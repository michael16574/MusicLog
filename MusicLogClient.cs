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

        public Artist GetParentArtist(Album album)
        {
            return _database.FindArtist(album.ArtistID);
        }
        public Artist GetParentArtist(Track track)
        {
            return _database.FindArtist(track.ArtistID);
        }
        public Album GetParentAlbum(Track track)
        {
            return _database.FindAlbum(track.AlbumID);
        }

        public IMusicObject GetMusicObject(Guid musicObjectID)
        {
            return _database.FindMusicObject(musicObjectID);
        }

        public List<Artist> GetArtists()
        {
            return _database.GetArtists();
        }

        public List<Album> GetAlbums()
        {
            return _database.GetAlbums();
        }
        public List<Album> GetAlbums(Artist artist)
        {
            return _database.FindAlbums(artist);
        }

        public List<Track> GetTracks()
        {
            return _database.GetTracks();
        }
        public List<Track> GetTracks(Artist artist)
        {
            return _database.FindTracks(artist);
        }
        public List<Track> GetTracks(Album album)
        {
            return _database.FindTracks(album);
        }

        public IEnumerable<Track> GetTracksWithHistory()
        {
            return _database.GetTracks().Where(t => t.LastListenedUnix != 0);
        }

        public void AddArtist(string name, string spotifyID)
        {
            Artist newArtist = new Artist(name, spotifyID);
            _database.AddArtist(newArtist);
            RemoveIfDuplicate(newArtist);
        }
        public void AddArtist(Artist artist)
        {
            _database.AddArtist(artist);
            RemoveIfDuplicate(artist);
        }
        public void AddArtists(List<Artist> artists)
        {
            var databaseArtists = _database.GetArtists();
            foreach (var artist in artists)
            {
                AddArtist(artist);
            }
        }

        public void RemoveArtist(string name, string spotifyID)
        {
            RemoveAlbums(_database.FindAlbums(new Artist(name, spotifyID)));
            _database.RemoveArtist(name, spotifyID);
        }
        public void RemoveArtist(Artist artist)
        {
            RemoveAlbums(_database.FindAlbums(artist));
            _database.RemoveArtist(artist);
        }
        public void RemoveArtists(List<Artist> artists)
        {
            foreach (var artist in artists)
            {
                RemoveAlbums(_database.FindAlbums(artist));
            }
            _database.RemoveArtists(artists);
        }


        public void AddAlbum(string albumName, string albumSpotifyID, Artist artist)
        {
            artist = ArtistInDBConfirmation(artist);
            Album newAlbum = new Album(albumName, albumSpotifyID, artist.ID);
            _database.AddAlbum(newAlbum);
            RemoveIfDuplicate(newAlbum);
        }
        public void AddAlbum(Album album, Artist artist)
        {
            artist = ArtistInDBConfirmation(artist);
            album.ArtistID = artist.ID;
            _database.AddAlbum(album);
            RemoveIfDuplicate(album);
        }
        public void AddAlbums(List<Album> albums, Artist artist)
        {
            artist = ArtistInDBConfirmation(artist);

            foreach (var album in albums)
            {
                album.ArtistID = artist.ID;
                _database.AddAlbum(album);
                RemoveIfDuplicate(album);
            }
        }

        public void RemoveAlbum(string name, string spotifyID)
        {
            RemoveTracks(_database.FindTracks(new Album(name, spotifyID)));
            _database.RemoveAlbum(name, spotifyID);

        }
        public void RemoveAlbum(Album album)
        {
            RemoveTracks(_database.FindTracks(album));
            _database.RemoveAlbum(album);
        }
        public void RemoveAlbums(List<Album> albums)
        {
            foreach (var album in albums)
            {
                RemoveTracks(_database.FindTracks(album));
            }
            _database.RemoveAlbums(albums);
        }


        public void AddTrack(string trackName, string trackSpotifyID, Album album)
        {
            // Will throw exception if album's ArtistID is empty
            album = AlbumInDBConfirmation(album);

            Track newTrack = new Track(trackName, trackSpotifyID, album.ID, album.ArtistID);
            _database.AddTrack(newTrack);
            RemoveIfDuplicate(newTrack);
        }
        public void AddTrack(Track track, Album album)
        {
            album = AlbumInDBConfirmation(album);

            track.ArtistID = album.ID;
            track.AlbumID = album.ID;
            _database.AddTrack(track);
            RemoveIfDuplicate(track);
        }
        public void AddTracks(List<Track> tracks, Album album)
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

        public void RemoveTrack(string name, string spotifyID)
        {
            _database.RemoveTrack(name, spotifyID);
        }
        public void RemoveTrack(Track track)
        {
            _database.RemoveTrack(track);
        }
        public void RemoveTracks(List<Track> tracks)
        {
            _database.RemoveTracks(tracks);
        }


        private Artist ArtistInDBConfirmation(Artist artist)
        {
            // Makes sure artist exist, if not then adds to database
            // Returns instance of artist in the database
            Artist foundArtist = _database.FindArtist(artist);
            if (foundArtist == null)
            {
                _database.AddArtist(artist);
                return artist;
            }
            return foundArtist;
        }
        private Album AlbumInDBConfirmation(Album album)
        {
            // Makes sure album exists, if not then adds to database
            // Returns instance of album in the database
            Album foundAlbum = _database.FindAlbum(album);

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


        private void RemoveIfDuplicate(Artist artist)
        {
            Artist foundArtist = _database.FindArtist(artist);
            if (foundArtist != artist)
            {
                _database.RemoveArtist(artist);
            }
        }
        private void RemoveIfDuplicate(Album album)
        {
            Album foundAlbum = _database.FindAlbum(album);
            if (foundAlbum != album)
            {
                _database.RemoveAlbum(album);
            }
        }
        private void RemoveIfDuplicate(Track track)
        {
            Track foundTrack = _database.FindTrack(track);
            if (foundTrack != track)
            {
                _database.RemoveTrack(track);
            }
        }


        public void UpdateSpotifyAuth(Credentials creds)
        {
            _spotifyAuth = new SpotifyWebAPI();
            _spotifyAuth = SpotifyApi.GetAuthObj(creds.SpotifyID, creds.SpotifySecret);
        }

        public List<Artist> GetSpotifyArtists(string query)
        {
            List<FullArtist> items = SpotifyApi.SearchArtists(query, _spotifyAuth);
            List<Artist> artistList = new List<Artist>();
            foreach (FullArtist artist in items)
            {
                Artist newArtist = new Artist
                {
                    Name = artist.Name,
                    SpotifyID = artist.Id
                };
                artistList.Add(newArtist);
            }
            return artistList;
        }
        public List<Album> GetSpotifyAlbums(Artist artistObj)
        {
            List<SimpleAlbum> SimpleAlbumList = SpotifyApi.SearchAlbums(artistObj, _spotifyAuth);
            List<Album> AlbumList = new List<Album>();
            foreach (SimpleAlbum album in SimpleAlbumList)
            {
                Album newAlbum = new Album
                {
                    Name = album.Name,
                    SpotifyID = album.Id
                };
                AlbumList.Add(newAlbum);

            }
            return DeleteDuplicateAlbums(AlbumList);
        }
        public List<Track> GetSpotifyTracks(Album albumObj)
        {
            List<SimpleTrack> SimpleTrackList = SpotifyApi.SearchTracks(albumObj, _spotifyAuth);
            List<Track> TrackList = new List<Track>();
            foreach (SimpleTrack track in SimpleTrackList)
            {
                Track NewTrack = new Track(track.Name, track.Id);
                NewTrack.TrackNo = track.TrackNumber;
                TrackList.Add(NewTrack);
            }
            return TrackList;
        }

        private List<Album> DeleteDuplicateAlbums(List<Album> albums)
        {
            if (albums.Count == 0)
            {
                return albums;
            }

            List<Album> albumRemovalList = new List<Album>();
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


        public void UpdateArtist(Artist artist)
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
                Album matchedAlbum = trackedAlbums.Find(a => a.Name == lastFMTrack.album.text);

                if (matchedAlbum == null)
                {
                    continue;
                }

                // Finding correct song to modify uts
                List<Track> songList = _database.FindTracks(matchedAlbum);
                foreach (Track dbTrack in songList)
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
        public void UpdateArtists(List<Artist> artists)
        {
            foreach (var artist in artists)
            {
                UpdateArtist(artist);
            }
        }
        public void UpdateAllArtists()
        {
            List<Artist> artists = _database.GetArtists();
            foreach (Artist artist in artists)
            {
                UpdateArtist(artist);
            }
        }

        public void RetrieveMissingAlbums(Artist artist)
        {
            List<Album> spotifyAlbumRetrieval = GetSpotifyAlbums(artist);
            AddAlbums(spotifyAlbumRetrieval, artist);
            foreach (Album album in spotifyAlbumRetrieval)
            {
                AddTracks(GetSpotifyTracks(album), album);
            }
        }

        public void UpdateHistory(int unix, Track track)
        {
            track.LastListenedUnix = unix;
        }
        public void UpdateHistory(DateTime time, Track track)
        {
            var dateTimeOffset = new DateTimeOffset(time);
            int uts = (int)dateTimeOffset.ToUnixTimeSeconds();
            UpdateHistory(uts, track);
        }
        public void UpdateHistory(int unix, Album album)
        {
            List<Track> tracks = _database.FindTracks(album);
            foreach (Track track in tracks)
            {
                UpdateHistory(unix, track);
            }
        }
        public void UpdateHistory(DateTime time, Album album)
        {
            var dateTimeOffset = new DateTimeOffset(time);
            int uts = (int)dateTimeOffset.ToUnixTimeSeconds();

            List<Track> tracks = _database.FindTracks(album);
            foreach (Track track in tracks)
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
                var album = (Album)GetMusicObject(guid);
                List<Track> trackList = GetTracks(album);
                foreach (var track in trackList)
                {
                    trackUris.Add(track.SpotifyID);
                }
            }

            FullPlaylist newPlaylist = SpotifyApi.CreatePlaylist(playlist.Name, _settings.Creds.SpotifyID, _spotifyAuth);
            SpotifyApi.AddPlaylistTracks(trackUris, newPlaylist, _spotifyAuth);
        }



    }
}
