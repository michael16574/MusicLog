using System;
using System.Collections.Generic;

namespace MusicLog
{
    public class CustomAlbum : IAlbum
    {
        public string Name { get; set; }
        public string ID { get; set; }

        public string ArtistID { get; set; }
        public bool Tracked { get; set; }

        public CustomAlbum()
        {
            ArtistID = String.Empty;
            ID = "custom:" + Guid.NewGuid().ToString();
            Tracked = false;
        }

        public IAlbum GetMatchingAlbumFrom(List<IAlbum> albums)
        {
            IAlbum query = null;
            var customAlbums = albums.FindAll(a => a is CustomAlbum);
            foreach (CustomAlbum album in customAlbums)
            {
                if (album.Name == Name)
                {
                    query = album;
                }
            }
            return query;
        }
        

    }
}
