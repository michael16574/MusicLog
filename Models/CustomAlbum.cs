using System;

namespace MusicLog
{
    public class CustomAlbum : IAlbum
    {
        public string Name { get; set; }
        public Guid ID { get; set; }

        public Guid ArtistID { get; set; }
        public bool Tracked { get; set; }

        public CustomAlbum()
        {
            ArtistID = Guid.Empty;
            ID = Guid.NewGuid();
            Tracked = false;
        }
        

    }
}
