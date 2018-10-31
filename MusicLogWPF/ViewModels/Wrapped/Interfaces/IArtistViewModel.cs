using MusicLog;

namespace MusicLogWPF
{
    public interface IArtistViewModel : IMusicObjectViewModel
    {
        IArtist Artist { get; }
    }
}
