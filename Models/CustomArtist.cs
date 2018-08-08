using System;

namespace MusicLog
{
    public class CustomArtist : IArtist
    {
        public string Name { get; set; }
        public Guid ID { get; set; }

        public CustomArtist()
        {
            ID = Guid.NewGuid();
        }
    }
}
