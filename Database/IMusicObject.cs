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
        Guid ID { get; set; }
    }

    public interface ISpotifyMusicObject : IMusicObject
    {
        string SpotifyID { get; set; }
    }

    public class Artist : ISpotifyMusicObject
    {
        public string Name { get; set; }
        public string SpotifyID { get; set; }
        public Guid ID { get; set; }

        public Artist()
        {
            ID = Guid.NewGuid();       
        }
        public Artist(string name, string spotifyID)
        {
            Name = name;
            SpotifyID = spotifyID;
            ID = Guid.NewGuid();
        }
    }

    public class Album : ISpotifyMusicObject
    {
        public string Name { get; set; }
        public string SpotifyID { get; set; }
        public Guid ID { get; set; }

        public Guid ArtistID { get; set; }
        public bool Tracked { get; set; }

        public Album()
        {
            ArtistID = Guid.Empty;
            ID = Guid.NewGuid();
            Tracked = false;
        }
        public Album(string name, string spotifyID)
        {
            Name = name;
            SpotifyID = spotifyID;
            ArtistID = Guid.Empty;
            ID = Guid.NewGuid();
            Tracked = false;
        }
        public Album(string name, string spotifyID, Guid artistID)
        {
            Name = name;
            SpotifyID = spotifyID;
            ArtistID = artistID;
            ID = Guid.NewGuid();
            Tracked = false;
        }
    }

    public class Track : ISpotifyMusicObject
    {
        public string Name { get; set; }
        public string SpotifyID { get; set; }
        public Guid ID { get; set; }

        public Guid ArtistID { get; set; }
        public Guid AlbumID { get; set; }
        public int TrackNo { get; set; }
        public int LastListenedUnix { get; set; }

        public Track()
        {
            ArtistID = Guid.Empty;
            AlbumID = Guid.Empty;
            ID = Guid.NewGuid();
        }
        public Track(string name, string spotifyID)
        {
            Name = name;
            SpotifyID = spotifyID;
            ArtistID = Guid.Empty;
            AlbumID = Guid.Empty;
            ID = Guid.NewGuid();
        }
        public Track(string name, string spotifyID, Guid albumID)
        {
            Name = name;
            SpotifyID = spotifyID;
            ArtistID = Guid.Empty;
            AlbumID = albumID;
            ID = Guid.NewGuid();
        }
        public Track(string name, string spotifyID, Guid albumID, Guid artistID)
        {
            Name = name;
            SpotifyID = spotifyID;
            ArtistID = artistID;
            AlbumID = albumID;
            ID = Guid.NewGuid();
        }
    }
}
