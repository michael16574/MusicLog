using System;

namespace MusicLog
{
    public class SpotifyAlbum : ISpotifyMusicObject, IAlbum
    {
        public string Name { get; set; }
        public string SpotifyID { get; set; }
        public Guid ID { get; set; }

        public Guid ArtistID { get; set; }
        public bool Tracked { get; set; }

        public SpotifyAlbum()
        {
            ArtistID = Guid.Empty;
            ID = Guid.NewGuid();
            Tracked = false;
        }
    }
}
