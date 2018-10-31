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
    public class ArtistEntryTabViewModel : INotifyPropertyChanged
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

        public ICommand SearchSpotifyArtistsCommand { get; set; }
        public ICommand AddSpotifyArtistsToDatabaseCommand { get; set; }
        
        private ObservableCollection<SpotifyArtist> _spotifyArtists;
        public ObservableCollection<SpotifyArtist> SpotifyArtists
        {
            get
            {
                return _spotifyArtists;
            }
            set
            {
                _spotifyArtists = value;
                RaisePropertyChanged("SpotifyArtists");
            }
        }

        private SpotifyArtist _selectedSpotifyArtist;
        public SpotifyArtist SelectedSpotifyArtist
        {
            get
            {
                return _selectedSpotifyArtist;
            }
            set
            {
                _selectedSpotifyArtist = value;
                RaisePropertyChanged("SelectedSpotifyArtist");
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

        public ArtistEntryTabViewModel(MusicLogClient musicLog)
        {
            _musicLog = musicLog;
            LoadCommands();
        }

        private void LoadCommands()
        {
            SearchSpotifyArtistsCommand = new CustomCommand(SearchSpotifyArtists, CanSearchSpotifyArtists);
            AddSpotifyArtistsToDatabaseCommand = new CustomCommand(AddSpotifyArtistsToDatabase, CanAddSpotifyArtistsToDatabase);
        }

        private void SearchSpotifyArtists(object obj)
        {
            List<SpotifyArtist> artists = _musicLog.GetSpotifyArtists(_spotifyQuery);
            List<SpotifyArtist> artistWithAlbums = new List<SpotifyArtist>();

            foreach (SpotifyArtist artist in artists)
            {
                List<SpotifyAlbum> albums = _musicLog.GetSpotifyAlbums(artist);
                if (albums.Count != 0)
                {
                    artistWithAlbums.Add(artist);
                }
            }

            SpotifyArtists = artistWithAlbums.ToObservableCollection();
        }

        private bool CanSearchSpotifyArtists(object obj)
        {
            if (String.IsNullOrEmpty(_spotifyQuery))
            {
                return false;
            }
            return true;
        }

        private void AddSpotifyArtistsToDatabase(object obj)
        {
            List<IAlbum> albumQuery = _musicLog.GetSpotifyAlbums(_selectedSpotifyArtist)
                                               .Cast<IAlbum>()
                                               .ToList();
            _musicLog.AddAlbums(albumQuery, _selectedSpotifyArtist);


            foreach (SpotifyAlbum album in albumQuery)
            {
                List<ITrack> trackQuery = _musicLog.GetSpotifyTracks(album)
                                                         .Cast<ITrack>()
                                                         .ToList(); ;
                _musicLog.AddTracks(trackQuery, album);

            }
        }

        private bool CanAddSpotifyArtistsToDatabase(object obj)
        {
            if (_selectedSpotifyArtist != null)
            {
                return true;
            }
            return false;
        }
    }
}
