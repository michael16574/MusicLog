using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLog.LastFM
{
    public class LastFMResponse_ArtistTracks
    {
            public Artisttracks artisttracks { get; set; }

            public class Artisttracks
            {
                public Track[] track { get; set; }
                public Attr @attr { get; set; }
            }
            public class Attr
            {
                public string user { get; set; }
                public string artist { get; set; }
                public string page { get; set; }
                public string perPage { get; set; }
                public string totalPages { get; set; }
                public string total { get; set; }
            }
            public class Track
            {
                public Artist artist { get; set; }
                public string name { get; set; }
                public string streamable { get; set; }
                public string mbid { get; set; }
                public Album album { get; set; }
                public string url { get; set; }
                public Image[] image { get; set; }
                public Date date { get; set; }
            }
            public class Artist
            {
                [JsonProperty("#text")]
                public string text { get; set; }
                public string mbid { get; set; }
            }
            public class Album
            {
                [JsonProperty("#text")]
                public string text { get; set; }
                public string mbid { get; set; }
            }
            public class Date
            {
                public string uts { get; set; }
                [JsonProperty("#text")]
                public string text { get; set; }
            }
            public class Image
            {
                [JsonProperty("#text")]
                public string text { get; set; }
                public string size { get; set; }
            }
     }
        
}

