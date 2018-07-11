using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLog.Database
{
    public interface IMusicObject
    {
        string Name { get; set; }
        string SpotifyID { get; set; }
        Guid ArtistID { get; set; }
    }

    public class Artist : IMusicObject
    {
        public string Name { get; set; }
        public string SpotifyID { get; set; }
        public Guid ArtistID { get; set; }

        public List<Album> Albums;

        public Artist()
        {
            ArtistID = Guid.NewGuid();
            Albums = new List<Album>();          
        }
        public Artist(string name, string id, List<Album> albums)
        {
            Name = name;
            SpotifyID = id;
            ArtistID = Guid.NewGuid();
            Albums = albums;
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Existance(Album album)
        {
            foreach (Album dbAlbum in Albums)
            {
                if (album.Name == dbAlbum.Name && album.SpotifyID == dbAlbum.SpotifyID)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class Album : IMusicObject
    {
        public string Name { get; set; }
        public string SpotifyID { get; set; }
        public Guid ArtistID { get; set; }

        public Guid AlbumID;        
        public bool Tracked;
        public List<Track> Tracks;

        public Album()
        {
            AlbumID = Guid.NewGuid();
            Tracks = new List<Track>();
            Tracked = false;
        }
        public Album(string name, string id)
        {
            Name = name;
            SpotifyID = id;
            AlbumID = Guid.NewGuid();
            Tracked = false;
        }

        public override string ToString()
        {
            return Name;
        }

        public void UpdateHistory(int uts)
        {
            foreach (Track track in Tracks)
            {
                track.UpdateHistory(uts);
            }
        }

        public void UpdateHistory(DateTime time)
        {
            var dateTimeOffset = new DateTimeOffset(time);
            int uts = (int)dateTimeOffset.ToUnixTimeSeconds();

            foreach (Track track in Tracks)
            {
                track.UpdateHistory(uts);
            }
        }        
    }

    public class Track : IMusicObject
    {
        public string Name { get; set; }
        public string SpotifyID { get; set; }
        public Guid ArtistID { get; set; }

        public Guid AlbumID;
        public int TrackNo;
        public int LastListenedUTS;

        public Track() {}
        public Track(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public void UpdateHistory(int uts)
        {
            LastListenedUTS = uts;
        }

        public void UpdateHistory(DateTime time)
        {
            var dateTimeOffset = new DateTimeOffset(time);
            LastListenedUTS = (int)dateTimeOffset.ToUnixTimeSeconds();
        }

    }
}
