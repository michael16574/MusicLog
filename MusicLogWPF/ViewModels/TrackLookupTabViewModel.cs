using MusicLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        public ICommand SearchTrackCommand { get; set; }

        public TrackLookupTabViewModel(MusicLogClient musicLog)
        {
            _musicLog = musicLog;
            LoadCommands();

            GetArtists(new CustomArtist());
        }

        private ObservableCollection<TreeArtist> _artists;
        public ObservableCollection<TreeArtist> Artists
        { get => _artists; set => _artists = value; }

        private ObservableCollection<SpotifyTrackViewModel> _queryTracks;
        public ObservableCollection<SpotifyTrackViewModel> QueryTracks
        {
            get
            {
                return _queryTracks;
            }
            set
            {
                if (!string.Equals(this._spotifyQuery, value))
                {
                    _queryTracks = value;
                    RaisePropertyChanged("QueryTracks");
                }
            }
        }

        private string _spotifyQuery;
        public string SpotifyQuery
        {
            get
            {
                return _spotifyQuery;
            }
            set
            {
                if (!string.Equals(this._spotifyQuery, value))
                {
                    _spotifyQuery = value;
                    RaisePropertyChanged("SpotifyQuery");
                }
            }
        }

        private void LoadCommands()
        {
            SearchTrackCommand = new CustomCommand(SearchTrack, CanSearchTrack);
        }

        private void SearchTrack(object obj)
        {
            var tracks = _musicLog.GetSpotifyTracks(SpotifyQuery);
            var trackVms = new List<SpotifyTrackViewModel>();
            foreach (var track in tracks)
            {
                trackVms.Add(new SpotifyTrackViewModel(track));
            }
            QueryTracks = trackVms.ToObservableCollection();
            
        }
        private bool CanSearchTrack(object obj)
        {
            if (String.IsNullOrWhiteSpace(SpotifyQuery))
            {
                return false;
            }
            return true;
        }

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
        
        private void BindToTrack()
        {

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

