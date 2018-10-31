using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLog;

namespace MusicLogWPF
{
    public static class MusicObjectProvider
    {
        public static ObservableCollection<IArtistViewModel> MakeObservableArtists(MusicLogClient musicLog)
        {
            var artists = new ObservableCollection<IArtistViewModel>();
            foreach (IArtist vanillaArtist in musicLog.GetArtists())
            {
                artists.Add(MakeArtistViewModel(vanillaArtist));
            }
            return artists;
        }

        public static ObservableCollection<IAlbumViewModel> MakeObservableAlbums(IArtistViewModel artist, MusicLogClient musicLog)
        {
            var albums = new ObservableCollection<IAlbumViewModel>();
            foreach (IAlbum vanillaAlbum in musicLog.GetAlbums(artist.Artist))
            {
                albums.Add(MakeAlbumViewModel(vanillaAlbum));
            }
            return albums;
        }

        public static ObservableCollection<ITrackViewModel> MakeObservableTracks(IAlbumViewModel album, MusicLogClient musicLog)
        {
            var tracks = new ObservableCollection<ITrackViewModel>();
            foreach (ITrack vanillaTrack in musicLog.GetTracks(album.Album))
            {
                tracks.Add(MakeTrackViewModel(vanillaTrack));
            }
            return tracks;
        }

        public static ObservableCollection<ITrackViewModel> MakeObservableTracks(IEnumerable<ITrack> list)
        {
            var tracks = new ObservableCollection<ITrackViewModel>();
            foreach (var vanillaTrack in list)
            {
                tracks.Add(MakeTrackViewModel(vanillaTrack));
            }
            return tracks;
        }

        public static IArtistViewModel MakeArtistViewModel(IArtist artist)
        {
            switch (artist)
            {
                case SpotifyArtist s:
                    return new SpotifyArtistViewModel(s);
                case CustomArtist c:
                    return new CustomArtistViewModel(c);
            }
            return null;
        }

        public static IAlbumViewModel MakeAlbumViewModel(IAlbum album)
        {
            switch (album)
            {
                case SpotifyAlbum s:
                    return new SpotifyAlbumViewModel(s);
                case CustomAlbum c:
                    return new CustomAlbumViewModel(c);
            }
            return null;
        }

        public static ITrackViewModel MakeTrackViewModel(ITrack track)
        {
            switch (track)
            {
                case SpotifyTrack s:
                    return new SpotifyTrackViewModel(s);
                case CustomTrack c:
                    return new CustomTrackViewModel(c);
            }
            return null;
        }

    }
}
