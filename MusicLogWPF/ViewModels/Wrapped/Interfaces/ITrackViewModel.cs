using System;
using MusicLog;

namespace MusicLogWPF
{
    public interface ITrackViewModel : IMusicObjectViewModel
    {
        ITrack Track { get; }
        string ArtistID { get; set; }
        string AlbumID { get; set; }
        int TrackNo { get; set; }
        int LastListenedUnix { get; set; }
        bool Visible { get; set; }
    }
}
