using System;
using System.Collections.Generic;

namespace MusicLog
{
    public interface ITrack : IMusicObject
    {
        string ArtistID { get; set; }
        string AlbumID { get; set; }
        int TrackNo { get; set; }
        int LastListenedUnix { get; set; }

        ITrack GetMatchingTrackFrom(List<ITrack> tracks);
    }
}
