using System;
using System.Collections.Generic;

namespace MusicLog
{
    public class CustomTrack : ITrack
    {
        public string Name { get; set; }
        public string ID { get; set; }

        public string ArtistID { get; set; }
        public string AlbumID { get; set; }
        public int TrackNo { get; set; }
        public int LastListenedUnix { get; set; }

        public CustomTrack()
        {
            ArtistID = string.Empty;
            AlbumID = string.Empty;
            ID = "custom:" + Guid.NewGuid();
        }

        public ITrack GetMatchingTrackFrom(List<ITrack> tracks)
        {
            ITrack query = null;
            var customTracks = tracks.FindAll(t => t is CustomTrack);
            foreach (CustomTrack track in customTracks)
            {
                if (track.Name == Name)
                {
                    query = track;
                }
            }
            return query;
        }
    }
}