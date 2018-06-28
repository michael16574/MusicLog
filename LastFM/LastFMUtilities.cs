using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastFM
{
    public static class LastFMUtilities
    {
        public static List<Database.Track> GetUserTracks(string user, string artistName)
        {
            LastFMAPIClient.LastFMResponse response = LastFMAPIClient.SearchUserTracks(user, artistName).Result;
            var tracks = new List<Database.Track>();
            foreach (LastFMAPIClient.Track singleTrack in response.artisttracks.track)
            {
                var newTrack = new Database.Track();

                newTrack.Name = singleTrack.name;
                newTrack.LastListenedUTS = Int32.Parse(singleTrack.date.uts);
                newTrack.ListenedState = true;
               
                tracks.Add(newTrack);               
            }
            return tracks;
        }

        
    }
}
