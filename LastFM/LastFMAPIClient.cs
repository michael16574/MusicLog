using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

namespace LastFM
{
    public class LastFMAPIClient
    {
        public static async Task<LastFMResponse> SearchUserTracks(string user, string artistName)
        {
            string apiUrl = "http://ws.audioscrobbler.com/2.0/?method=user.getArtistTracks";
            string param1 = "&user=" + user;
            string param2 = "&artist=" + artistName;
            string apiKey = "&api_key=" + File.ReadLines("clientinfo.txt").Skip(2).Take(1).First();
            string apiFormat = "&format=json";

            using (var client = new HttpClient())
            {
                
                string url = apiUrl + param1 + param2 + apiKey + apiFormat;

                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var rootResult = JsonConvert.DeserializeObject<LastFMResponse>(result);
                    return rootResult;
                }
                else
                {
                    return null;
                }
            }
        }

        private static string ConvertSpaceToAddSign(string input)
        {
            input = input.TrimEnd();
            string[] words = input.Split(' ');
            string output = string.Empty;
            foreach (string word in words)
            {
                output += word;
                output += "+";
            }
            output = output.TrimEnd('+');
            return output;

        }
        private static readonly HttpClient client = new HttpClient();

        public class LastFMResponse
        {
            public Artisttracks artisttracks { get; set; }
        }
        public class Artisttracks
        {
            public Track[] track { get; set; }
            public Attr attr { get; set; }
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
            public string text { get; set; }
            public string mbid { get; set; }
        }
        public class Album
        {
            public string text { get; set; }
            public string mbid { get; set; }
        }
        public class Date
        {
            public string uts { get; set; }
            public string text { get; set; }
        }
        public class Image
        {
            public string text { get; set; }
            public string size { get; set; }
        }
    }

    
    
    
}
