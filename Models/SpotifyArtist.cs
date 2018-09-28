using System;
using System.Collections.Generic;

namespace MusicLog
{
    public class SpotifyArtist : ISpotifyMusicObject, IArtist
    {
        public string Name { get; set; }
        public string SpotifyID { get; set; }
        public string ID { get; set; }

        public SpotifyArtist()
        {
            ID = "spotify:" + Guid.NewGuid().ToString();
        }

        public IArtist GetMatchingArtistFrom(List<IArtist> artists)
        {
            IArtist query = null;
            var spotifyArtists = artists.FindAll(a => a is SpotifyArtist);
            foreach (SpotifyArtist artist in spotifyArtists)
            {
                if (artist.Name == Name &&
                    artist.SpotifyID == SpotifyID)
                {
                    query = artist;
                }
            }
            return query;
        }
    }
}
