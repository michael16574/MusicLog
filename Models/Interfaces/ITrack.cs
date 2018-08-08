using System;

namespace MusicLog
{
    public interface ITrack : IMusicObject
    {
        Guid ArtistID { get; set; }
        Guid AlbumID { get; set; }
        int TrackNo { get; set; }
        int LastListenedUnix { get; set; }
    }
}
