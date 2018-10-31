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
    public class PlaylistTabViewModel : INotifyPropertyChanged
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

        public event DoubleClickedEventHandler AlbumDoubleClicked;
        public delegate void DoubleClickedEventHandler(object sender, DoubleClickedEventArgs e);

        public ICommand AddPlaylistCommand { get; set; }
        public ICommand AddPlaylistToSpotifyCommand { get; set; }
        public ICommand ChangePlaylistNameCommand { get; set; }
        public ICommand DeletePlaylistCommand { get; set; }
        public ICommand DeleteAlbumFromPlaylistCommand { get; set; }
        public ICommand GoToDatabaseEntryCommand { get; set; }

        public PlaylistTabViewModel(MusicLogClient musicLog)
        {
            _musicLog = musicLog;
            GetPlaylists();
            LoadCommands();
        }

        private ObservableCollection<PlaylistObject> _playlists;
        public ObservableCollection<PlaylistObject> Playlists
        {
            get { return _playlists; }
            set
            {
                _playlists = value;
                RaisePropertyChanged("Playlists");
            }
        }

        private PlaylistObject _selectedPlaylist;
        public PlaylistObject SelectedPlaylist
        {
            get { return _selectedPlaylist; }
            set
            {
                _selectedPlaylist = value;
                RaisePropertyChanged("SelectedPlaylist");

                GetSelectedPlaylistAlbums();
                SetProgressForActiveAlbums();
            }
        }

        private ObservableCollection<IAlbumViewModel> _activeAlbums;
        public ObservableCollection<IAlbumViewModel> ActiveAlbums
        {
            get { return _activeAlbums; }
            set
            {
                _activeAlbums = value;
                RaisePropertyChanged("ActiveAlbums");
            }
        }

        private IAlbumViewModel _selectedAlbum;
        public IAlbumViewModel SelectedAlbum
        {
            get { return _selectedAlbum; }
            set
            {
                _selectedAlbum = value;
                RaisePropertyChanged("SelectedAlbum");
            }
        }


        private void LoadCommands()
        {
            AddPlaylistCommand = new CustomCommand(AddPlaylist, CanAddPlaylist);
            AddPlaylistToSpotifyCommand = new CustomCommand(AddPlaylistToSpotify, CanAddPlaylistToSpotify);
            ChangePlaylistNameCommand = new CustomCommand(ChangePlaylistName, CanChangePlaylistName);
            DeletePlaylistCommand = new CustomCommand(DeletePlaylist, CanDeletePlaylist);
            DeleteAlbumFromPlaylistCommand = new CustomCommand(DeleteAlbumFromPlaylist, CanDeleteAlbumFromPlaylist);
            GoToDatabaseEntryCommand = new CustomCommand(GoToDatabaseEntry, CanGoToDatabaseEntry);
        }

        private void AddPlaylist(object obj)
        {
            var dialog = new TextBoxWindow();
            var playlist = new PlaylistObject();
            if (dialog.ShowDialog() == true)
            {
                playlist.Name = dialog.ResponseText;
                if (String.IsNullOrWhiteSpace(playlist.Name))
                {
                    return;
                }
            }

            _musicLog.AddPlaylist(playlist);
            Playlists.Add(playlist);
        }
        private bool CanAddPlaylist(object obj)
        {
            return true;
        }

        private void AddPlaylistToSpotify(object obj)
        {
            _musicLog.AddPlaylistToSpotify(SelectedPlaylist);
        }

        private bool CanAddPlaylistToSpotify(object obj)
        {
            if (SelectedPlaylist != null)
            {
                return true;
            }
            return false;
        }


        private void ChangePlaylistName(object obj)
        {
            var activePlaylist = SelectedPlaylist;
            var dialog = new TextBoxWindow();
            if (dialog.ShowDialog() == true)
            {
                activePlaylist.Name = dialog.ResponseText;
            }

            int index = Playlists.IndexOf(activePlaylist);
            SelectedPlaylist = null;
            Playlists.Remove(activePlaylist);
            Playlists.Insert(index, activePlaylist);
            SelectedPlaylist = activePlaylist;
        }

        private bool CanChangePlaylistName(object obj)
        {
            if (SelectedPlaylist != null)
            {
                return true;
            }
            return false;
        }

        private void DeletePlaylist(object obj)
        {
            var activePlaylist = SelectedPlaylist;
            _musicLog.RemovePlaylist(activePlaylist);
            SelectedPlaylist = null;
            Playlists.Remove(activePlaylist);
        }

        private bool CanDeletePlaylist(object obj)
        {
            if (SelectedPlaylist != null)
            {
                return true;
            }
            return false;
        }

        private void DeleteAlbumFromPlaylist(object obj)
        {
            var activeAlbum = SelectedAlbum;
            SelectedPlaylist.Albums.Remove(activeAlbum.ID);
            SelectedAlbum = null;
            ActiveAlbums.Remove(activeAlbum);
        }

        private bool CanDeleteAlbumFromPlaylist(object obj)
        {
            if (SelectedAlbum != null)
            {
                return true;
            }
            return false;
        }

        private void GoToDatabaseEntry(object obj)
        {
            var args = new DoubleClickedEventArgs(SelectedAlbum.ArtistName, SelectedAlbum.Name);
            AlbumDoubleClicked(this, args);
        }

        private bool CanGoToDatabaseEntry(object obj)
        {
            if (SelectedAlbum != null)
            {
                return true;
            }
            return false;
        }


        private void GetPlaylists()
        {
            Playlists = _musicLog.GetPlaylists().ToObservableCollection();
        }

        private void GetSelectedPlaylistAlbums()
        {
            if (SelectedPlaylist == null || SelectedPlaylist.Albums.Count == 0)
            {
                SelectedPlaylist = null;
                return;
            }


            var albumQuery = _musicLog.GetMusicObjects(SelectedPlaylist.Albums)
                                      .ConvertAll(o => (IAlbum)o);

            ActiveAlbums = new ObservableCollection<IAlbumViewModel>();
            foreach (var album in albumQuery)
            {
                var newAlbumViewModel = MusicObjectProvider.MakeAlbumViewModel(album);
                newAlbumViewModel.ArtistName = _musicLog.GetMusicObject(newAlbumViewModel.ArtistID).Name;
                ActiveAlbums.Add(newAlbumViewModel);
            }

        }

        private void SetProgressForActiveAlbums()
        {
            if (SelectedPlaylist == null || SelectedPlaylist.Albums.Count == 0)
            {
                return;
            }

            foreach (var album in ActiveAlbums)
            {
                List<ITrack> tracks = _musicLog.GetTracks(album.Album);
                int listenedCountTrack = tracks.Where(t => t.LastListenedUnix != 0).Count();
                int totalCountTrack = tracks.Count();
                album.SetProgress(listenedCountTrack, totalCountTrack);
            }
        }
 
        public void SetDoubleClickedEvent(Action<object, DoubleClickedEventArgs> action)
        {
            AlbumDoubleClicked += new DoubleClickedEventHandler(action);
        }



    }
}
