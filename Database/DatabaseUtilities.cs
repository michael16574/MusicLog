using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MusicLog.Database
{
    /// <summary>
    /// Contains methods for Database operation that do not modify such.
    /// </summary>
    public static class DatabaseUtilities
    {
        public static Artist FindArtistNode(string name, string id, List<Artist> artists)
        {
            Artist activeArtist = null;
            try
            {
                activeArtist = artists.First(a => a.Name == name && a.Id == id);
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }          
            return activeArtist;                
        }

        public static AlbumNode FindAlbumNode(string albumName, string albumId, string artistName, string artistId, List<Artist> artists)
        {
            AlbumNode albumStruct = new AlbumNode();
            albumStruct.artistNode = artists.First(a => a.Name == artistName && a.Id == artistId);
            albumStruct.albumNode = albumStruct.artistNode.Albums.First(a => a.Name == albumName && a.Id == albumId);
            return albumStruct;
        }

        public static TrackNode FindTrackNode(string trackName, string albumName, string albumId, string artistName, string artistId, List<Artist> artists)
        {
            TrackNode trackStruct = new TrackNode();
            trackStruct.artistNode = artists.First(a => a.Name == artistName && a.Id == artistId);
            trackStruct.albumNode = trackStruct.artistNode.Albums.First(a => a.Name == albumName && a.Id == albumId);
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
