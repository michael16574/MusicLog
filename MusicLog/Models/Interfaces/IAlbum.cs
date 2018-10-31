using System;
using System.Collections.Generic;

namespace MusicLog
{
    public interface IAlbum : IMusicObject
    {
        string ArtistID { get; set; }
        bool Tracked { get; set; }

        IAlbum GetMatchingAlbumFrom(List<IAlbum> album);
    }
}
