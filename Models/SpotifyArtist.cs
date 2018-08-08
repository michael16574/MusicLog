using System;

namespace MusicLog
{
    public class SpotifyArtist : ISpotifyMusicObject, IArtist
    {
        public string Name { get; set; }
        public string SpotifyID { get; set; }
        public Guid ID { get; set; }

        public SpotifyArtist()
        {
            ID = Guid.NewGuid();
        }
    }
}
