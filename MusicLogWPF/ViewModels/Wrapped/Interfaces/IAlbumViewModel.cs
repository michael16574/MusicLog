using System;
using MusicLog;

namespace MusicLogWPF
{
    public interface IAlbumViewModel : IMusicObjectViewModel
    {
        IAlbum Album { get; }
        string ArtistID { get; set; }
        bool Tracked { get; set; }
        string AlbumProgress { get; set; }
        string ArtistName { get; set; }

        void SetProgress(int listenedCountTrack, int totalCountTrack);
    }
}
