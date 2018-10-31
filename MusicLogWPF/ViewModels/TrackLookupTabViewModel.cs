using MusicLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLogWPF
{
    public class TrackLookupTabViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private MusicLogClient _musicLog;

        public TrackLookupTabViewModel(MusicLogClient musicLog)
        {
            _musicLog = musicLog;
            GetArtists(new CustomArtist());
        }

        private ObservableCollection<TreeArtist> _artists;
        public ObservableCollection<TreeArtist> Artists
        { get => _artists; set => _artists = value; }


        private void GetArtists()
        {
            Artists = new ObservableCollection<TreeArtist>();
            var artists = _musicLog.GetArtists();
            foreach (var artist in artists)
            {
                var newTreeArtist = new TreeArtist(artist, _musicLog);
                Artists.Add(newTreeArtist);
            }
        }
        private void GetArtists<T>(T item) where T : IArtist
        {
            Artists = new ObservableCollection<TreeArtist>();
            var artists = _musicLog.GetArtists().Where(a => a is T);
            foreach (var artist in artists)
            {
                var newTreeArtist = new TreeArtist(artist, _musicLog);
                Artists.Add(newTreeArtist);
            }
        }      
 
    }

    public class TreeArtist
    {
        public string Name { get; set; }

        public ObservableCollection<TreeAlbum> TreeAlbums { get; set; }

        public TreeArtist(IArtist artist, MusicLogClient musicLog)
        {
            Name = artist.Name;
            TreeAlbums = new ObservableCollection<TreeAlbum>();
            PopulateAlbums(artist, musicLog);
        }

        private void PopulateAlbums(IArtist artist, MusicLogClient musicLog)
        {
            var albums = musicLog.GetAlbums(artist);
            foreach (var album in albums)
            {
                TreeAlbums.Add(new TreeAlbum(album, musicLog));
            }
        }
    }

    public class TreeAlbum
    {
        public string Name { get; set; }

        public ObservableCollection<TreeTrack> TreeTracks { get; set; }

        public TreeAlbum(IAlbum album, MusicLogClient musicLog)
        {
            Name = album.Name;
            TreeTracks = new ObservableCollection<TreeTrack>();
            PopulateTracks(album, musicLog);
        }

        private void PopulateTracks(IAlbum album, MusicLogClient musicLog)
        {
            var tracks = musicLog.GetTracks(album);
            foreach (var track in tracks)
            {
                TreeTracks.Add(new TreeTrack(track));
            }
        }
    }

    public class TreeTrack
    {
        public ITrack Track { get; set; }

        public TreeTrack(ITrack track)
        {
            Track = track;
        }
    }


}

