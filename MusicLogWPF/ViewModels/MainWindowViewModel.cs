using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using MusicLog;

namespace MusicLogWPF
{
    public class MainWindowViewModel : INotifyPropertyChanged
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

        private TabViewModels _tabVM;
        public TabViewModels TabVM
        {
            get { return _tabVM; }
            set
            {
                _tabVM = value;
                RaisePropertyChanged("TabVM");
            }
        }

        public MainWindowViewModel()
        {
            string rootPath = "C:\\Programs\\MusicLog\\UserData\\";
            _musicLog = new MusicLogClient(new UserSettings(rootPath + "database.xml", rootPath + "playlist.xml", rootPath + "credentials.xml"));
            RefreshTab(0);
        }

        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                _selectedTabIndex = value;
                RaisePropertyChanged("SelectedTabIndex");
                RefreshTab(_selectedTabIndex);
            }
        }

        

        private void RefreshTab(int tabIndex)
        {
            TabVM = new TabViewModels();
            switch (tabIndex)
            {
                case 0:
                    TabVM.ArtistEntryTabVM = new ArtistEntryTabViewModel(_musicLog);
                    break;
                case 1:
                    TabVM.DatabaseTabVM = new DatabaseTabViewModel(_musicLog);
                    break;
                case 2:
                    TabVM.HistoryTabVM = new HistoryTabViewModel(_musicLog);
                    break;
                case 3:
                    TabVM.PlaylistTabVM = new PlaylistTabViewModel(_musicLog);
                    var goToDatabaseAlbum = new Action<object, DoubleClickedEventArgs>(GoToDatabaseAlbum);
                    TabVM.PlaylistTabVM.SetDoubleClickedEvent(goToDatabaseAlbum);
                    break;
                case 4:
                    TabVM.TrackLookupTabVM = new TrackLookupTabViewModel(_musicLog);
                    break;
            }
        }

        private void GoToDatabaseAlbum(object sender, DoubleClickedEventArgs e)
        {
            SelectedTabIndex = 1;
            var artist = TabVM.DatabaseTabVM.ActiveArtists.Where(a => a.Name == e.ArtistName).FirstOrDefault();
            TabVM.DatabaseTabVM.SelectedArtist = artist;
            var album = TabVM.DatabaseTabVM.ActiveAlbums.Where(a => a.Name == e.AlbumName).FirstOrDefault();
            TabVM.DatabaseTabVM.SelectedAlbum = album;
        }

        public void SaveMusicLog()
        {
            _musicLog.Save();
        }


       

        public class TabViewModels : INotifyPropertyChanged
        {
            private ArtistEntryTabViewModel _artistEntryTabVM;
            private DatabaseTabViewModel _databaseTabVM;
            private HistoryTabViewModel _historyTabVM;
            private PlaylistTabViewModel _playlistTabVM;
            private TrackLookupTabViewModel _trackLookupTabVM;

            public TabViewModels() { }

            public ArtistEntryTabViewModel ArtistEntryTabVM
            {
                get { return _artistEntryTabVM; }
                set
                {
                    _artistEntryTabVM = value;
                    RaisePropertyChanged("ArtistEntryTabVM");
                }
            }
            public DatabaseTabViewModel DatabaseTabVM
            {
                get { return _databaseTabVM; }
                set
                {
                    _databaseTabVM = value;
                    RaisePropertyChanged("DatabaseTabVM");
                }
            }
            public HistoryTabViewModel HistoryTabVM
            {
                get { return _historyTabVM; }
                set
                {
                    _historyTabVM = value;
                    RaisePropertyChanged("HistoryTabVM");
                }
            }
            public PlaylistTabViewModel PlaylistTabVM
            {
                get { return _playlistTabVM; }
                set
                {
                    _playlistTabVM = value;
                    RaisePropertyChanged("PlaylistTabVM");
                }
            }
            public TrackLookupTabViewModel TrackLookupTabVM
            {
                get { return _trackLookupTabVM; }
                set
                {
                    _trackLookupTabVM = value;
                    RaisePropertyChanged("TrackLookupTabVM");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            private void RaisePropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
    }
}
