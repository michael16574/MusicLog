using System.Collections.Generic;

namespace MusicLog
{
    public interface IArtist : IMusicObject
    {
        IArtist GetMatchingArtistFrom(List<IArtist> artists);
    }
}
