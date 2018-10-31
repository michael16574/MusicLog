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
    public class DatabaseInstance
    {
        private XmlHandler _xmlHandler;
        private MusicObjectTable _database;


        public DatabaseInstance()
        {
            _xmlHandler = new XmlHandler();
            _database = new MusicObjectTable();
        }
        public DatabaseInstance(string filePath)
        {
            _xmlHandler = new XmlHandler();
            Load(filePath);
        }

        public void Save(string fileName)
        {
            _xmlHandler.Serialize(ref _database, fileName);
        }
        public void Load(string filePath)
        {
            _database = new MusicObjectTable();
            _xmlHandler.Deserialize(ref _database, filePath);
        }


        public List<IArtist> GetArtists()
        {
            return _database.Artists;
        }

        public void AddArtist(IArtist artist)
        {
            _database.Artists.Add(artist);
        }
        public void AddArtists(List<IArtist> artists)
        {
            _database.Artists.AddRange(artists);
        }
        
        public void RemoveArtist(IArtist artist)
        {
            if (_database.Artists.Contains(artist))
            {
                _database.Artists.Remove(artist);
            }
        }
        public void RemoveArtists(List<IArtist> artists)
        {
            foreach(var artist in artists)
            {
                RemoveArtist(artist);
            }
        }


        public List<IAlbum> GetAlbums()
        {
            return _database.Albums;
        }

        public void AddAlbum(IAlbum album)
        {
            _database.Albums.Add(album);
        }
        public void AddAlbums(List<IAlbum> albums)
        {
            _database.Albums.AddRange(albums);
        }

        public void RemoveAlbum(IAlbum album)
        {
            if (_database.Albums.Contains(album))
            {
                _database.Albums.Remove(album);
            }
        }
        public void RemoveAlbums(List<IAlbum> albums)
        {
            foreach (var album in albums)
            {
                RemoveAlbum(album);
            }
        }

        public void TrackAlbums(List<IAlbum> albums)
        {
            foreach(var album in albums)
            {
                album.Tracked = true;
            }
        }
        public void UntrackAlbums(List<IAlbum> albums)
        {
            foreach(var album in albums)
            {
                album.Tracked = false;
            }
        }


        public List<ITrack> GetTracks()
        {
            return _database.Tracks;
        }
       
        public void AddTrack(ITrack track)
        {
            _database.Tracks.Add(track);
        }
        public void AddTracks(List<ITrack> tracks)
        {
            _database.Tracks.AddRange(tracks);
        }

        public void RemoveTrack(ITrack track)
        {
            if (_database.Tracks.Contains(track))
            {
                _database.Tracks.Remove(track);
            }
        }
        public void RemoveTracks(List<ITrack> tracks)
        {
            foreach (var track in tracks)
            {
                RemoveTrack(track);
            }
        }


        public IArtist FindArtist(IArtist artist)
        {
            IArtist activeArtist = artist.GetMatchingArtistFrom(_database.Artists);        
            return activeArtist;
        }
        public IArtist FindArtist(string artistID)
        {
            IArtist activeArtist = _database.Artists.FirstOrDefault(a => a.ID == artistID);
            return activeArtist;
        }
        
        public IAlbum FindAlbum(IAlbum album)
        {
            IAlbum activeAlbum = album.GetMatchingAlbumFrom(_database.Albums);
            return activeAlbum;
        }
        public IAlbum FindAlbum(string albumID)
        {
            IAlbum activeAlbum = _database.Albums.FirstOrDefault(a => a.ID == albumID);
            return activeAlbum;
        }
        
        public List<IAlbum> FindAlbums(IArtist artist)
        {
            List<IAlbum> albums = _database.Albums.Where(a => a.ArtistID == artist.ID).ToList();
            return albums;
        }

        public ITrack FindTrack(ITrack track)
        {
            ITrack activeTrack = track.GetMatchingTrackFrom(_database.Tracks);
            return activeTrack;
        }
        public ITrack FindTrack(string trackID)
        {
            ITrack activeTrack = _database.Tracks.FirstOrDefault(t => t.ID == trackID);
            return activeTrack;
        }
        
        public List<ITrack> FindTracks(IArtist artist)
        {
            List<ITrack> tracks = _database.Tracks.Where(t => t.ArtistID == artist.ID).ToList();
            return tracks;
        }

        public List<ITrack> FindTracks(IAlbum album)
        {
            List<ITrack> tracks = _database.Tracks.Where(t => t.AlbumID == album.ID).ToList();
            return tracks;
        }

        public IMusicObject FindMusicObject(string musicObjectID)
        {
            var musicObjectList = new List<IMusicObject>();
            return musicObjectList.Union(_database.Artists)
                                  .Union(_database.Albums)
                                  .Union(_database.Tracks)
                                  .Where(m => m.ID == musicObjectID)
                                  .FirstOrDefault();
        }
        public List<IMusicObject> FindMusicObjects(List<string> musicObjectIDs)
        {
            var musicObjectList = new List<IMusicObject>();
            return musicObjectList.Union(_database.Artists)
                                  .Union(_database.Albums)
                                  .Union(_database.Tracks)
                                  .Where(m => musicObjectIDs.Contains(m.ID))
                                  .ToList();
        }

    }

   
    
}
