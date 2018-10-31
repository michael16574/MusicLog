using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using MusicLog;

namespace MusicLogWPF
{
    public class HistoryTabViewModel : INotifyPropertyChanged
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

        public ICommand MakeVisibleCommand { get; set; }

        public HistoryTabViewModel(MusicLogClient musicLog)
        {
            _musicLog = musicLog;
            LoadCommands();
            HistoryTracks = new ObservableCollection<KeyValuePair<ITrackViewModel, TrackViewModelParents>>();
            _sortedMusicViewModels = new SortedMusicViewModels();
            GetTracksWithHistory();
            

        }

        private ObservableCollection<KeyValuePair<ITrackViewModel, TrackViewModelParents>> _historyTracks;
        public ObservableCollection<KeyValuePair<ITrackViewModel, TrackViewModelParents>> HistoryTracks
        {
            get { return _historyTracks; }
            set
            {
                _historyTracks = value;
                RaisePropertyChanged("HistoryTracks");
            }
        }

        private SortedMusicViewModels _sortedMusicViewModels;
        private List<ITrackViewModel> _lastFiltered;

        private DateTime? _dateFilter;
        public DateTime? DateFilter
        {
            get { return _dateFilter; }
            set
            {
                _dateFilter = value;
                RaisePropertyChanged("DateFilter");
                
                if( MakeVisibleCommand.CanExecute(new object()) )
                {
                    MakeVisible(new object());
                }
            }
        }

        private string _filterText;
        public string FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                RaisePropertyChanged("FilterText");
            }
        }

        

        private void GetTracksWithHistory()
        {
            var tracks = MusicObjectProvider.MakeObservableTracks(_musicLog.GetTracksWithHistory());
            MatchTracksWithParents(tracks);
        }

        private void MatchTracksWithParents(ObservableCollection<ITrackViewModel> tracks)
        {
            // Searches and matches tracks with respective albums and tracks to populate HistoryTracks
            // Also populates SortedMusicViewModels with retrieved tracks,albums,artists

            var tracksSorted = tracks.GroupBy(t => t.AlbumID).GroupBy(t => t.First().ArtistID);

            var keyValueList = new List<KeyValuePair<ITrackViewModel, TrackViewModelParents>>();
            foreach (var artistGroup in tracksSorted)
            {
                var artistMusicObject = _musicLog.GetMusicObject(artistGroup.First().First().ArtistID);
                IArtistViewModel activeArtist = MusicObjectProvider.MakeArtistViewModel((IArtist)artistMusicObject);
                _sortedMusicViewModels.Add(activeArtist);

                foreach (var albumGroup in artistGroup)
                {
                    var albumMusicObject = _musicLog.GetMusicObject(albumGroup.First().AlbumID);
                    IAlbumViewModel activeAlbum = MusicObjectProvider.MakeAlbumViewModel((IAlbum)albumMusicObject);
                    _sortedMusicViewModels.Add(activeAlbum);

                    foreach (var track in albumGroup)
                    {
                        _sortedMusicViewModels.Add(track);
                        track.Visible = true;
                        var key = track;
                        var value = new TrackViewModelParents(activeArtist, activeAlbum);
                        keyValueList.Add(new KeyValuePair<ITrackViewModel, TrackViewModelParents>(key, value));
                    }
                }
            }

            HistoryTracks = keyValueList.OrderByDescending(l => l.Key.LastListenedUnix).ToObservableCollection();
        }
        
        private void RefreshFilter()
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                foreach (var track in HistoryTracks)
                {
                    track.Key.Visible = true;
                }
                return;
            }

            foreach (var track in HistoryTracks)
            {
                track.Key.Visible = false;
            }
        }

        private void FilterNameSwitch()
        {   
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                return;
            }

            var list = _sortedMusicViewModels.FindSimilar(FilterText);

            foreach (var track in list)
            {
                track.Visible = true;              
            }

            _lastFiltered = list;         
        }

        private void FilterTimeSwitch(int unixTimeFrame)
        {
            if (DateFilter.HasValue)
            {
                if (_lastFiltered.Count != 0)
                {
                    foreach (var track in _lastFiltered)
                    {
                        long dateFilterUnix = new DateTimeOffset(_dateFilter.GetValueOrDefault()).ToUnixTimeSeconds();
                        if (track.LastListenedUnix <= dateFilterUnix || track.LastListenedUnix >= dateFilterUnix + unixTimeFrame)
                        {
                            track.Visible = false;
                        }                       
                    }
                }
                else
                {
                    foreach (var track in HistoryTracks)
                    {
                        long dateFilterUnix = new DateTimeOffset(_dateFilter.GetValueOrDefault()).ToUnixTimeSeconds();
                        if (track.Key.LastListenedUnix <= dateFilterUnix || track.Key.LastListenedUnix >= dateFilterUnix + unixTimeFrame)
                        {
                            track.Key.Visible = false;
                        }
                    }
                }
            }
        }

        private void LoadCommands()
        {
            MakeVisibleCommand = new CustomCommand(MakeVisible, CanMakeVisible);
        }

        private void MakeVisible(object obj)
        {
            _lastFiltered = new List<ITrackViewModel>();
            RefreshFilter();
            FilterNameSwitch();
            FilterTimeSwitch(86400);
        }
        private bool CanMakeVisible(object obj)
        {
            if (HistoryTracks == null)
            {
                return false;
            }
            return true;
        }
        

        public struct TrackViewModelParents
        {
            public IArtistViewModel Artist { get; }
            public IAlbumViewModel Album { get; }

            public TrackViewModelParents(IArtistViewModel artist, IAlbumViewModel album)
            {
                this.Artist = artist;
                this.Album = album;
            }
        }
    }
}
