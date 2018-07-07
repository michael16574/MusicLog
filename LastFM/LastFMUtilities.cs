using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLog.LastFM
{
    public static class LastFMUtilities
    {
        public static List<LastFMResponse_ArtistTracks.Track> GetUserTracks(string user, string artistName)
        {
            LastFMResponse_ArtistTracks userTracks = LastFMAPIClient.SearchUserTracks(user, artistName).Result;
            
            var tracks = new List<LastFMResponse_ArtistTracks.Track>();
            foreach (LastFMResponse_ArtistTracks.Track singleTrack in userTracks.artisttracks.track)
            {
                // Filling in potentially missing album name
                if (String.IsNullOrEmpty(singleTrack.album.text))
                {
                    LastFMResponse_AlbumGetInfo albumInfo = LastFMAPIClient.AlbumGetInfo(singleTrack.album).Result;
                    singleTrack.album.text = albumInfo.rootObject.album.name;
                }
                
                tracks.Add(singleTrack);               
            }
            return tracks;
        }

    }
}
