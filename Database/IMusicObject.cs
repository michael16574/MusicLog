using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLog
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

        public Artist()
        {
            ArtistID = Guid.NewGuid();       
        }
        public Artist(string name, string spotifyID)
        {
            Name = name;
            SpotifyID = spotifyID;
            ArtistID = Guid.NewGuid();
        }
    }

    public class Album : IMusicObject
    {
        public string Name { get; set; }
        public string SpotifyID { get; set; }
        public Guid ArtistID { get; set; }

        public Guid AlbumID;        
        public bool Tracked;

        public Album()
        {
            ArtistID = Guid.Empty;
            AlbumID = Guid.NewGuid();
            Tracked = false;
        }
        public Album(string name, string id)
        {
            Name = name;
            SpotifyID = id;
            ArtistID = Guid.Empty;
            AlbumID = Guid.NewGuid();
            Tracked = false;
        }
        public Album(string name, string id, Guid artistID)
        {
            Name = name;
            SpotifyID = id;
            ArtistID = artistID;
            AlbumID = Guid.NewGuid();
            Tracked = false;
        }

        
    }

    public class Track : IMusicObject
    {
        public string Name { get; set; }
        public string SpotifyID { get; set; }
        public Guid ArtistID { get; set; }

        public Guid AlbumID;
        public Guid TrackID;

        public int TrackNo;
        public int LastListenedUTS;

        public Track()
        {
            ArtistID = Guid.Empty;
            AlbumID = Guid.Empty;
            TrackID = Guid.NewGuid();
        }
        public Track(string name, string spotifyID)
        {
            Name = name;
            SpotifyID = spotifyID;
            ArtistID = Guid.Empty;
            AlbumID = Guid.Empty;
            TrackID = Guid.NewGuid();
        }
        public Track(string name, string spotifyID, Guid albumID)
        {
            Name = name;
            SpotifyID = spotifyID;
            ArtistID = Guid.Empty;
            AlbumID = albumID;
            TrackID = Guid.NewGuid();
        }
        public Track(string name, string spotifyID, Guid albumID, Guid artistID)
        {
            Name = name;
            SpotifyID = spotifyID;
            ArtistID = artistID;
            AlbumID = albumID;
            TrackID = Guid.NewGuid();
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
