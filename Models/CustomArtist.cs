using System;
using System.Collections.Generic;

namespace MusicLog
{
    public class CustomArtist : IArtist
    {
        public string Name { get; set; }
        public string ID { get; set; }

        public CustomArtist()
        {
            ID = "custom:" + Guid.NewGuid().ToString();
        }

        public IArtist GetMatchingArtistFrom(List<IArtist> artists)
        {
            IArtist query = null;
            var customArtists = artists.FindAll(a => a is CustomArtist);
            foreach (CustomArtist artist in customArtists)
            {
                if (artist.Name == Name)
                {
                    query = artist;
                }
            }
            return query;
        }
    }
}
