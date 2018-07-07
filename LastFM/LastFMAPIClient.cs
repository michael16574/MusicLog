using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

namespace MusicLog.LastFM
{
    public static class LastFMAPIClient
    {
        public async static Task<LastFMResponse_ArtistTracks> SearchUserTracks(string user, string artistName)
        {
            string url = "http://ws.audioscrobbler.com/2.0/?method=user.getArtistTracks"
                            + "&user=" + user
                            + "&artist=" + artistName
                            + GetUserAPIKey()
                            + "&format=json";

            string result = await CallClient(url);
            if (CallClient(url) != null)
            {
                var rootResult = JsonConvert.DeserializeObject<LastFMResponse_ArtistTracks>(result);
                return rootResult;
            }
            else
            {
                return null;
            }
        }

        public async static Task<LastFMResponse_AlbumGetInfo> AlbumGetInfo(LastFMResponse_ArtistTracks.Album album)
        {
            string url = "http://ws.audioscrobbler.com/2.0/?method=album.getInfo"
                            + "&format = json";
            if (!string.IsNullOrEmpty(album.text)) 
            {
                url += "&album=" + album.text;
            }
            if (!string.IsNullOrEmpty(album.mbid))
            {
                url += "&mbid=" + album.mbid;
            }

            string result = await CallClient(url);
            if (CallClient(url) != null)
            {
                var rootResult = JsonConvert.DeserializeObject<LastFMResponse_AlbumGetInfo>(result);
                return rootResult;
            }
            else
            {
                return null;
            }
        }

        private async static Task<String> CallClient(string url)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }


        private static string GetUserAPIKey()
        {
            string apiKey = "&api_key=" + File.ReadLines("clientinfo.txt").Skip(2).Take(1).First();
            return apiKey;
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

        
    }

    
    
    
}
