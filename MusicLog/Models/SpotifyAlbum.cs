using System;
using System.Collections.Generic;

namespace MusicLog
{
    public class SpotifyAlbum : ISpotifyMusicObject, IAlbum
    {
        public string Name { get; set; }
        public string SpotifyID { get; set; }
        public string ID { get; set; }

        public string ArtistID { get; set; }
        public bool Tracked { get; set; }

        public SpotifyAlbum()
        {
            ArtistID = String.Empty;
            ID = "spotify:" + Guid.NewGuid().ToString();
            Tracked = false;
        }

        public IAlbum GetMatchingAlbumFrom(List<IAlbum> albums)
        {
            IAlbum query = null;
            var spotifyAlbums = albums.FindAll(a => a is SpotifyAlbum);
            foreach (SpotifyAlbum album in spotifyAlbums)
            {
                if (album.Name == Name &&
                    album.SpotifyID == SpotifyID)
                {
                    query = album;
                }
            }
            return query;
        }
    }
}
