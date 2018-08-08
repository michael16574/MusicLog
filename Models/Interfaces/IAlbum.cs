using System;

namespace MusicLog
{
    public interface IAlbum : IMusicObject
    {
        Guid ArtistID { get; set; }
        bool Tracked { get; set; }
    }
}
