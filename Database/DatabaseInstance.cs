using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MusicLog
{
    /// <summary>
    /// Contains database and methods pertaining to modification of such.
    /// </summary>
    public class DatabaseInstance
    {
        private XmlHandler _xmlHandler;
        private MusicObjectTable _database;


        public DatabaseInstance()
        {
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


        public List<Artist> GetArtists()
        {
            return _database.Artists;
        }

        public void AddArtist(Artist artist)
        {
            _database.Artists.Add(artist);
        }
        public void AddArtists(List<Artist> artists)
        {
            _database.Artists.AddRange(artists);
        }
        
        public void RemoveArtist(string name, string spotifyID)
        {
            Artist artist = FindArtist(new Artist(name, spotifyID));
            if (artist != null)
            {
                _database.Artists.Remove(artist);
            }
        }
        public void RemoveArtist(Artist artist)
        {
            if (_database.Artists.Contains(artist))
            {
                _database.Artists.Remove(artist);
            }
            else
            {
                // In instance where artist is not part of database
                RemoveArtist(artist.Name, artist.SpotifyID);
            }
        }
        public void RemoveArtists(List<Artist> artists)
        {
            foreach(var artist in artists)
            {
                RemoveArtist(artist);
            }
        }


        public List<Album> GetAlbums()
        {
            return _database.Albums;
        }

        public void AddAlbum(Album album)
        {
            _database.Albums.Add(album);
        }
        public void AddAlbums(List<Album> albums)
        {
            _database.Albums.AddRange(albums);
        }

        public void RemoveAlbum(string name, string spotifyID)
        {
            Album album = FindAlbum(new Album(name, spotifyID));
            if (album != null)
            {
                _database.Albums.Remove(album);
            }
        }
        public void RemoveAlbum(Album album)
        {
            if (_database.Albums.Contains(album))
            {
                _database.Albums.Remove(album);
            }
            else
            {
                // In instance where album is not part of database
                RemoveAlbum(album.Name, album.SpotifyID);
            }
        }
        public void RemoveAlbums(List<Album> albums)
        {
            foreach (var album in albums)
            {
                RemoveAlbum(album);
            }
        }

        public void TrackAlbums(List<Album> albums)
        {
            foreach(var album in albums)
            {
                album.Tracked = true;
            }
        }
        public void UntrackAlbums(List<Album> albums)
        {
            foreach(var album in albums)
            {
                album.Tracked = false;
            }
        }


        public List<Track> GetTracks()
        {
            return _database.Tracks;
        }
       
        public void AddTrack(Track track)
        {
            _database.Tracks.Add(track);
        }
        public void AddTracks(List<Track> tracks)
        {
            _database.Tracks.AddRange(tracks);
        }

        public void RemoveTrack(string name, string spotifyID)
        {
            Track track = FindTrack(new Track(name, spotifyID));
            if (track != null)
            {
                _database.Tracks.Remove(track);
            }
        }
        public void RemoveTrack(Track track)
        {
            if (_database.Tracks.Contains(track))
            {
                _database.Tracks.Remove(track);
            }
            else
            {
                // In instance where track is not part of database
                RemoveAlbum(track.Name, track.SpotifyID);
            }
        }
        public void RemoveTracks(List<Track> tracks)
        {
            foreach (var track in tracks)
            {
                RemoveTrack(track);
            }
        }

        

        public Artist FindArtist(Artist artist)
        {
            Artist activeArtist = _database.Artists.FirstOrDefault(a => a.Name == artist.Name && a.SpotifyID == artist.SpotifyID);
            return activeArtist;
        }
        public Artist FindArtist(Guid artistID)
        {
            Artist activeArtist = _database.Artists.FirstOrDefault(a => a.ArtistID == artistID);
            return activeArtist;
        }

        public Album FindAlbum(Album album)
        {
            Album activeAlbum = _database.Albums.FirstOrDefault(a => a.Name == album.Name && a.SpotifyID == album.SpotifyID);
            return activeAlbum;
        }
        public Album FindAlbum(Guid albumID)
        {
            Album activeAlbum = _database.Albums.FirstOrDefault(a => a.AlbumID == albumID);
            return activeAlbum;
        }
        public List<Album> FindAlbums(Artist artist)
        {
            // Finds all albums linked to artist through ArtistID
            List<Album> albums = _database.Albums.Where(a => a.ArtistID == artist.ArtistID).ToList();
            return albums;
        }

        public Track FindTrack(Track track)
        {
            Track activeTrack = _database.Tracks.FirstOrDefault(t => t.Name == track.Name && t.SpotifyID == track.SpotifyID);
            return activeTrack;
        }
        public Track FindTrack(Guid trackID)
        {
            Track activeTrack = _database.Tracks.FirstOrDefault(t => t.TrackID == trackID);
            return activeTrack;
        }
        public List<Track> FindTracks(Artist artist)
        {
            // Finds all tracks linked to artist through ArtistID
            List<Track> tracks = _database.Tracks.Where(t => t.ArtistID == artist.ArtistID).ToList();
            return tracks;
        }
        public List<Track> FindTracks(Album album)
        {
            List<Track> tracks = _database.Tracks.Where(t => t.AlbumID == album.AlbumID).ToList();
            return tracks;
        }

    }

   
    
}
