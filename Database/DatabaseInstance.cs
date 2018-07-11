using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MusicLog.Database
{
    /// <summary>
    /// Contains database and methods pertaining to modification of such.
    /// </summary>
    public class DatabaseInstance
    {
        public List<Artist> Artists;
        private XmlHandler _xmlHandler;
        private MusicObjectTable _database;
        

        public DatabaseInstance()
        {
            Artists = new List<Artist>();
            this._xmlHandler = new XmlHandler();
            this._database = new MusicObjectTable();            
        }
        public DatabaseInstance(string location)
        {
            this._xmlHandler = new XmlHandler();
            this._database = _xmlHandler.Deserialize(location);
        }

        public void Save(string fileName)
        {
            _xmlHandler.Serialize(_database, fileName);
        }
        
        public void Load(string filePath)
        {
            this._database = _xmlHandler.Deserialize(filePath);
        }

        public void AddArtist(string name, string spotifyID)
        {
            Artist NewArtist = new Artist(name, );
            NewArtist.Name = name;
            NewArtist.SpotifyID = spotifyID;
            NewArtist.ArtistID = Guid.NewGuid();

            // Checking duplicity
            var foundArtist = FindArtistNode(NewArtist.Name, NewArtist.SpotifyID, Artists);
            if (foundArtist != null)
            {
                return;
            }
            Artists.Add(NewArtist);           
        }
        public void AddArtists(List<Artist> artists)
        {
            List<Artist> uniqueArtists = new List<Artist>();

            // Checking duplicity
            foreach (var artist in artists)
            {
                var foundArtist = FindArtistNode(artist.Name, artist.SpotifyID, Artists);
                if (foundArtist != null)
                {
                    continue;
                }
                uniqueArtists.Add(artist);
            }
            Artists.AddRange(uniqueArtists);           
        }


        public void RemoveArtist(Artist artist)
        {
            if (Artists.Contains(artist))
            {
                Artists.Remove(artist);
            }
            else
            {
                RemoveArtist(artist.Name, artist.SpotifyID);
            }
        }
        public void RemoveArtist(string name, string id)
        {
            Artist activeArtist = FindArtistNode(name, id, Artists);

            if (activeArtist != null)
            {
                Artists.Remove(activeArtist);
            }
        }

        public void AddAlbum(string albumName, string albumId, string artistName, string artistId)
        {
            Album NewAlbum = new Album();
            NewAlbum.Name = albumName;
            NewAlbum.SpotifyID = albumId;

            Artist activeArtist = FindArtistNode(artistName, artistId, Artists);

            if (activeArtist != null)
            {
                if (!activeArtist.Existance(NewAlbum))
                {
                    activeArtist.Albums.Add(NewAlbum);
                }             
            }          
        }
        public void AddAlbums(List<Album> albums, string artistName, string artistId)
        {
            Artist activeArtist = FindArtistNode(artistName, artistId, Artists);

            if (activeArtist != null)
            {
                foreach(Album album in albums)
                {
                    if (!activeArtist.Existance(album))
                    {
                        activeArtist.Albums.Add(album);
                    }
                }
            }
        }
        public void AddAlbums(List<Album> albums, Artist artist)
        {
            if (artist != null)
            {
                foreach (Album album in albums)
                {
                    if (!artist.Existance(album))
                    {
                        artist.Albums.Add(album);
                    }
                }
            }
        }

        public void RemoveAlbum(string albumName, string albumId, string artistName, string artistId)
        {
            AlbumNode activeAlbum = FindAlbumNode(albumName, albumId, artistName, artistId, Artists);

            if (activeAlbum.albumNode != null)
            {
                activeAlbum.artistNode.Albums.Remove(activeAlbum.albumNode);
            }
        }

        public void TrackAlbums(List<Album> albums)
        {
            foreach(var album in albums)
            {
                album.Tracked = true;
            }
        }

        public void AddTracks(List<Track> tracks, string albumName, string albumId, string artistName, string artistId)
        {
            AlbumNode activeAlbum = FindAlbumNode(albumName, albumId, artistName, artistId, Artists);            

            activeAlbum.albumNode.Tracks.AddRange(tracks);               
        }
        public void RemoveTrack(string trackName, string albumName, string albumId, string artistName, string artistId)
        {
            TrackNode activeTrack = FindTrackNode(trackName, albumName, albumId, artistName, artistId, Artists);

            if (activeTrack.trackNode != null)
            {
                activeTrack.albumNode.Tracks.Remove(activeTrack.trackNode);
            }
        }

        public void ModifyDate(Track track, int uts)
        {
            track.LastListenedUTS = uts;
        }


        

        private static Artist FindArtistNode(string name, string id, List<Artist> artists)
        {
            Artist activeArtist = null;
            try
            {
                activeArtist = artists.First(a => a.Name == name && a.SpotifyID == id);
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
            return activeArtist;
        }

        private static AlbumNode FindAlbumNode(string albumName, string albumId, string artistName, string artistId, List<Artist> artists)
        {
            AlbumNode albumStruct = new AlbumNode();
            albumStruct.artistNode = artists.First(a => a.Name == artistName && a.SpotifyID == artistId);
            albumStruct.albumNode = albumStruct.artistNode.Albums.First(a => a.Name == albumName && a.SpotifyID == albumId);
            return albumStruct;
        }

        private static TrackNode FindTrackNode(string trackName, string albumName, string albumId, string artistName, string artistId, List<Artist> artists)
        {
            TrackNode trackStruct = new TrackNode();
            trackStruct.artistNode = artists.First(a => a.Name == artistName && a.SpotifyID == artistId);
            trackStruct.albumNode = trackStruct.artistNode.Albums.First(a => a.Name == albumName && a.SpotifyID == albumId);
            trackStruct.trackNode = trackStruct.albumNode.Tracks.First(t => t.Name == trackName);
            return trackStruct;
        }

  

        public struct AlbumNode
        {
            public Artist artistNode;
            public Album albumNode;
        }

        public struct TrackNode
        {
            public Artist artistNode;
            public Album albumNode;
            public Track trackNode;
        }
    }

   
    
}
