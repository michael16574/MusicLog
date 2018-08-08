using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;

namespace MusicLog
{
    public class MusicLogClient
    {
        private static readonly Lazy<MusicLogClient> _instance = new Lazy<MusicLogClient>(() => new MusicLogClient(new UserSettings()));

        private DatabaseInstance _database;
        private PlaylistInstance _playlist;
        private SpotifyWebAPI _spotifyAuth;
        private UserSettings _settings;

        public static MusicLogClient Instance
        {
            get { return _instance.Value; }
        }

        public MusicLogClient(UserSettings settings)
        {
            _settings = settings;

            if (File.Exists(settings.DatabasePath))
            { _database = new DatabaseInstance(settings.DatabasePath); }
            else
            { _database = new DatabaseInstance(); }

            if (File.Exists(settings.PlaylistPath))
            { _playlist = new PlaylistInstance(settings.PlaylistPath); }
            else
            { _playlist = new PlaylistInstance(); }

            UpdateSpotifyAuth(_settings.Creds);
        }

        public void Save()
        {
            _database.Save(_settings.DatabasePath);
            _playlist.Save(_settings.PlaylistPath);
        }

        public IArtist GetParentArtist(IAlbum album)
        {
            return _database.FindArtist(album.ArtistID);
        }
        public IArtist GetParentArtist(ITrack track)
        {
            return _database.FindArtist(track.ArtistID);
        }
        public IAlbum GetParentAlbum(ITrack track)
        {
            return _database.FindAlbum(track.AlbumID);
        }

        public IMusicObject GetMusicObject(Guid musicObjectID)
        {
            return _database.FindMusicObject(musicObjectID);
        }

        public List<IMusicObject> GetMusicObjects(List<Guid> musicObjectIDs)
        {
            return _database.FindMusicObjects(musicObjectIDs);
        }

        public List<IArtist> GetArtists()
        {
            return _database.GetArtists();
        }

        public List<IAlbum> GetAlbums()
        {
            return _database.GetAlbums();
        }
        public List<IAlbum> GetAlbums(IArtist artist)
        {
            return _database.FindAlbums(artist);
        }

        public List<ITrack> GetTracks()
        {
            return _database.GetTracks();
        }
        public List<ITrack> GetTracks(IArtist artist)
        {
            return _database.FindTracks(artist);
        }
        public List<ITrack> GetTracks(IAlbum album)
        {
            return _database.FindTracks(album);
        }

        public IEnumerable<ITrack> GetTracksWithHistory()
        {
            return _database.GetTracks().Where(t => t.LastListenedUnix != 0);
        }

        public void AddArtist(IArtist artist)
        {
            _database.AddArtist(artist);
            RemoveIfDuplicate(artist);
        }
        public void AddArtists(List<IArtist> artists)
        {
            var databaseArtists = _database.GetArtists();
            foreach (var artist in artists)
            {
                AddArtist(artist);
            }
        }
        


        public void RemoveArtist(IArtist artist)
        {
            RemoveAlbums(_database.FindAlbums(artist));
            _database.RemoveArtist(artist);
        }
        public void RemoveArtists(List<IArtist> artists)
        {
            foreach (var artist in artists)
            {
                RemoveAlbums(_database.FindAlbums(artist));
            }
            _database.RemoveArtists(artists);
        }


        public void AddAlbum(IAlbum album, IArtist artist)
        {
            artist = ArtistInDBConfirmation(artist);
            album.ArtistID = artist.ID;
            _database.AddAlbum(album);
            RemoveIfDuplicate(album);
        }
        public void AddAlbums(List<IAlbum> albums, IArtist artist)
        {
            artist = ArtistInDBConfirmation(artist);

            foreach (var album in albums)
            {
                album.ArtistID = artist.ID;
                _database.AddAlbum(album);
                RemoveIfDuplicate(album);
            }
        }


        public void RemoveAlbum(IAlbum album)
        {
            RemoveTracks(_database.FindTracks(album));
            _database.RemoveAlbum(album);
        }
        public void RemoveAlbums(List<IAlbum> albums)
        {
            foreach (var album in albums)
            {
                RemoveTracks(_database.FindTracks(album));
            }
            _database.RemoveAlbums(albums);
        }


        public void AddTrack(ITrack track, IAlbum album)
        {
            album = AlbumInDBConfirmation(album);

            track.ArtistID = album.ID;
            track.AlbumID = album.ID;
            _database.AddTrack(track);
            RemoveIfDuplicate(track);
        }
        public void AddTracks(List<ITrack> tracks, IAlbum album)
        {
            album = AlbumInDBConfirmation(album);

            foreach (var track in tracks)
            {
                track.ArtistID = album.ArtistID;
                track.AlbumID = album.ID;
                _database.AddTrack(track);
                RemoveIfDuplicate(track);
            }
        }

        
        public void RemoveTrack(ITrack track)
        {
            _database.RemoveTrack(track);
        }
        public void RemoveTracks(List<ITrack> tracks)
        {
            _database.RemoveTracks(tracks);
        }

        private IArtist ArtistInDBConfirmation(IArtist artist)
        {
            // Makes sure artist exist, if not then adds to database
            // Returns instance of artist in the database
            IArtist foundArtist = _database.FindArtist(artist);
            if (foundArtist == null)
            {
                _database.AddArtist(artist);
                return artist;
            }
            return foundArtist;
        }
        private IArtist ArtistInDBConfirmation(SpotifyArtist artist)
        {
            // Makes sure artist exist, if not then adds to database
            // Returns instance of artist in the database
            SpotifyArtist foundArtist = (SpotifyArtist)_database.FindArtist(artist);
            if (foundArtist == null)
            {
                _database.AddArtist(artist);
                return artist;
            }
            return foundArtist;
        }
        private IAlbum AlbumInDBConfirmation(IAlbum album)
        {
            // Makes sure album exists, if not then adds to database
            // Returns instance of album in the database
            IAlbum foundAlbum = _database.FindAlbum(album);

            if (foundAlbum == null)
            {
                if (album.ArtistID == Guid.Empty)
                {
                    throw new System.ArgumentException("Album is missing artist parent");
                }
                _database.AddAlbum(album);
                return album;
            }
            return foundAlbum;
        }
        private IAlbum AlbumInDBConfirmation(SpotifyAlbum album)
        {
            // Makes sure album exists, if not then adds to database
            // Returns instance of album in the database
            SpotifyAlbum foundAlbum = (SpotifyAlbum)_database.FindAlbum(album);

            if (foundAlbum == null)
            {
                if (album.ArtistID == Guid.Empty)
                {
                    throw new System.ArgumentException("Album is missing artist parent");
                }
                _database.AddAlbum(album);
                return album;
            }
            return foundAlbum;
        }

        private void RemoveIfDuplicate(IArtist artist)
        {
            IArtist foundArtist = _database.FindArtist(artist);
            if (foundArtist != artist)
            {
                _database.RemoveArtist(artist);
            }
        }
        private void RemoveIfDuplicate(IAlbum album)
        {
            IAlbum foundAlbum = _database.FindAlbum(album);
            if (foundAlbum != album)
            {
                _database.RemoveAlbum(album);
            }
        }
        private void RemoveIfDuplicate(ITrack track)
        {
            ITrack foundTrack = _database.FindTrack(track);
            if (foundTrack != track)
            {
                _database.RemoveTrack(track);
            }
        }


        public void UpdateSpotifyAuth(Credentials creds)
        {
            _spotifyAuth = SpotifyApi.GetAuthObjByImplicitGrant(creds.SpotifyID).Result;
        }

        public List<SpotifyArtist> GetSpotifyArtists(string query)
        {
            List<FullArtist> items = SpotifyApi.SearchArtists(query, _spotifyAuth);
            List<SpotifyArtist> artistList = new List<SpotifyArtist>();
            foreach (FullArtist artist in items)
            {
                SpotifyArtist newArtist = new SpotifyArtist
                {
                    Name = artist.Name,
                    SpotifyID = artist.Id
                };
                artistList.Add(newArtist);
            }
            return artistList;
        }
        public List<SpotifyAlbum> GetSpotifyAlbums(SpotifyArtist artistObj)
        {
            List<SimpleAlbum> SimpleAlbumList = SpotifyApi.SearchAlbums(artistObj, _spotifyAuth);
            List<SpotifyAlbum> AlbumList = new List<SpotifyAlbum>();
            foreach (SimpleAlbum album in SimpleAlbumList)
            {
                SpotifyAlbum newAlbum = new SpotifyAlbum
                {
                    Name = album.Name,
                    SpotifyID = album.Id
                };
                AlbumList.Add(newAlbum);

            }
            return GetUniqueSpotifyAlbums(AlbumList);
        }
        public List<SpotifyTrack> GetSpotifyTracks(SpotifyAlbum albumObj)
        {
            List<SimpleTrack> SimpleTrackList = SpotifyApi.SearchTracks(albumObj, _spotifyAuth);
            List<SpotifyTrack> TrackList = new List<SpotifyTrack>();
            foreach (SimpleTrack track in SimpleTrackList)
            {
                SpotifyTrack NewTrack = new SpotifyTrack
                {
                    Name = track.Name,
                    SpotifyID = track.Id
                };
                NewTrack.TrackNo = track.TrackNumber;
                TrackList.Add(NewTrack);
            }
            return TrackList;
        }

        private List<SpotifyAlbum> GetUniqueSpotifyAlbums(List<SpotifyAlbum> albums)
        {
            if (albums.Count == 0)
            {
                return albums;
            }

            List<SpotifyAlbum> albumRemovalList = new List<SpotifyAlbum>();
            List<int> albumTrackCount = new List<int>();

            albumTrackCount.Add(_database.FindTracks(albums[0]).Count);
            for (int i = 1; i < albums.Count(); i++)
            {
                albumTrackCount.Add(_database.FindTracks(albums[i]).Count);
                for (int j = 0; j < i; j++)
                {
                    if (albums[i].Name == albums[j].Name)
                    {
                        if (albumTrackCount[i] >= albumTrackCount[j])
                        {
                            albumRemovalList.Add(albums[j]);
                        }
                        else
                        {
                            albumRemovalList.Add(albums[i]);
                        }
                    }
                }
            }

            foreach (var album in albumRemovalList)
            {
                albums.Remove(album);
            }

            return albums;
        }


        public void UpdateArtist(IArtist artist)
        {
            // Getting tracked albums from artist
            var trackedAlbums = _database.FindAlbums(artist)
                                         .Where(a => a.Tracked == true)
                                         .ToList();
            if (trackedAlbums.Count == 0)
            {
                return;
            }

            // Getting track listen history from lastFM
            List<LastFMResponse_ArtistTracks.Track> tracksNonUnique = LastFMApi.GetUserTracks(artist.Name, _settings.Creds.LastFMUser, _settings.Creds.LastFMKey);
            var tracks = tracksNonUnique.GroupBy(t => new { t.name, t.album.text })
                                        .Select(t => t.First())
                                        .ToList();

            foreach (var lastFMTrack in tracks)
            {
                // Matching correct album
                IAlbum matchedAlbum = trackedAlbums.Find(a => a.Name == lastFMTrack.album.text);

                if (matchedAlbum == null)
                {
                    continue;
                }

                // Finding correct song to modify uts
                List<ITrack> songList = _database.FindTracks(matchedAlbum);
                foreach (ITrack dbTrack in songList)
                {
                    if (lastFMTrack.name.ToLower() != dbTrack.Name.ToLower())
                    {
                        continue;
                    }
                    else
                    {
                        dbTrack.LastListenedUnix = Int32.Parse(lastFMTrack.date.uts);
                    }
                }
            }
        }
        public void UpdateArtists(List<IArtist> artists)
        {
            foreach (var artist in artists)
            {
                UpdateArtist(artist);
            }
        }
        public void UpdateAllArtists()
        {
            List<IArtist> artists = _database.GetArtists();
            foreach (IArtist artist in artists)
            {
                UpdateArtist(artist);
            }
        }

        public void RetrieveMissingAlbums(SpotifyArtist artist)
        {
            List<SpotifyAlbum> spotifyAlbumRetrieval = GetSpotifyAlbums(artist);
            AddAlbums(spotifyAlbumRetrieval.Cast<IAlbum>().ToList(), artist);
            foreach (var album in spotifyAlbumRetrieval)
            {
                AddTracks(GetSpotifyTracks(album).Cast<ITrack>().ToList(), album);
            }
        }

        public void UpdateHistory(int unix, ITrack track)
        {
            track.LastListenedUnix = unix;
        }
        public void UpdateHistory(DateTime time, ITrack track)
        {
            var dateTimeOffset = new DateTimeOffset(time);
            int uts = (int)dateTimeOffset.ToUnixTimeSeconds();
            UpdateHistory(uts, track);
        }
        public void UpdateHistory(int unix, IAlbum album)
        {
            List<ITrack> tracks = _database.FindTracks(album);
            foreach (ITrack track in tracks)
            {
                UpdateHistory(unix, track);
            }
        }
        public void UpdateHistory(DateTime time, IAlbum album)
        {
            var dateTimeOffset = new DateTimeOffset(time);
            int uts = (int)dateTimeOffset.ToUnixTimeSeconds();

            List<ITrack> tracks = _database.FindTracks(album);
            foreach (ITrack track in tracks)
            {
                UpdateHistory(uts, track);
            }
        }

        public void AddPlaylist(PlaylistObject playlist)
        {
            _playlist.Add(playlist);
        }

        public void RemovePlaylist(PlaylistObject playlist)
        {
            _playlist.Remove(playlist);
        }

        public List<PlaylistObject> GetPlaylists()
        {
            return _playlist.GetPlaylists();
        }

        public List<PlaylistObject> GetPlaylists(string playlistName)
        {
            return _playlist.GetPlaylists(playlistName);
        }

        public void AddPlaylistToSpotify(PlaylistObject playlist)
        {
            var trackUris = new List<string>();

            foreach (var guid in playlist.Albums)
            {
                IAlbum album = (IAlbum)GetMusicObject(guid);
                if (album is SpotifyAlbum)
                {
                    List<SpotifyTrack> trackList = GetTracks(album).Cast<SpotifyTrack>().ToList();
                    foreach (var track in trackList)
                    {
                        trackUris.Add(track.SpotifyID);
                    }
                }               
            }

            FullPlaylist newPlaylist = SpotifyApi.CreatePlaylist(playlist.Name, _settings.Creds.SpotifyUser, _spotifyAuth);
            SpotifyApi.AddPlaylistTracks(trackUris, newPlaylist, _spotifyAuth);
        }



    }
}
