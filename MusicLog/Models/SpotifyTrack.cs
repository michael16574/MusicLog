using System;
using System.Collections.Generic;

namespace MusicLog
{
    public class SpotifyTrack : ISpotifyMusicObject, ITrack
    {
        public string Name { get; set; }
        public string SpotifyID { get; set; }
        public string ID { get; set; }

        public string ArtistID { get; set; }
        public string AlbumID { get; set; }
        public int TrackNo { get; set; }
        public int LastListenedUnix { get; set; }

        public SpotifyTrack()
        {
            ArtistID = String.Empty;
            AlbumID = String.Empty;
            ID = "spotify:" + Guid.NewGuid().ToString();
        }

        public ITrack GetMatchingTrackFrom(List<ITrack> tracks)
        {
            ITrack query = null;
            var spotifyTracks = tracks.FindAll(t => t is SpotifyTrack);
            foreach (SpotifyTrack track in tracks)
            {
                if (track.Name == Name &&
                    track.SpotifyID == SpotifyID)
                {
                    query = track;
                }
            }
            return query;
        }
    }
}
