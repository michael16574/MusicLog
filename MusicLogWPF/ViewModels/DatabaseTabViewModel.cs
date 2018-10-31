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
    public class DatabaseTabViewModel : INotifyPropertyChanged
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

        public DatabaseTabViewModel(MusicLogClient musicLog)
        {
            _musicLog = musicLog;
            LoadCommands();
            GetDatabaseArtists();
            GetPlaylists();
            PopulatePlaylistMenuItems();

        }

        public ICommand AddCustomArtistCommand { get; set; }
        public ICommand UpdateArtistCommand { get; set; }
        public ICommand UpdateAllArtistsCommand { get; set; }
        public ICommand RetrieveMissingAlbumsCommand { get; set; }
        public ICommand DeleteArtistCommand { get; set; }

        public ICommand AddCustomAlbumCommand { get; set; }
        public ICommand TrackAlbumCommand { get; set; }
        public ICommand UntrackAlbumCommand { get; set; }
        public ICommand MarkAlbumListenedCommand { get; set; }
        public ICommand RemoveAlbumHistoryCommand { get; set; }
        public ICommand DeleteAlbumCommand { get; set; }

        public ICommand MarkTrackListenedCommand { get; set; }
        public ICommand RemoveTrackHistoryCommand { get; set; }

        public ICommand AddAlbumToPlaylistCommand { get; set; }


        private ObservableCollection<IArtistViewModel> _activeArtists;
        public ObservableCollection<IArtistViewModel> ActiveArtists
        {
            get
            {
                return _activeArtists;
            }
            set
            {
                _activeArtists = value;
                RaisePropertyChanged("ActiveArtists");
            }
        }

        private IArtistViewModel _selectedArtist;
        public IArtistViewModel SelectedArtist
        {
            get
            {
                return _selectedArtist;
            }
            set
            {
                _selectedArtist = value;
                RaisePropertyChanged("SelectedArtist");
                GetDatabaseAlbums();
                ActiveTracks = null;
            }
        }

        private ObservableCollection<IAlbumViewModel> _activeAlbums;
        public ObservableCollection<IAlbumViewModel> ActiveAlbums
        {
            get
            {
                return _activeAlbums;
            }
            set
            {
                _activeAlbums = value;
                RaisePropertyChanged("ActiveAlbums");

            }
        }

        private IAlbumViewModel _selectedAlbum;
        public IAlbumViewModel SelectedAlbum
        {
            get
            {
                return _selectedAlbum;
            }
            set
            {
                _selectedAlbum = value;
                RaisePropertyChanged("SelectedAlbum");
                GetDatabaseTracks();
            }
        }

        private ObservableCollection<ITrackViewModel> _activeTracks;
        public ObservableCollection<ITrackViewModel> ActiveTracks
        {
            get
            {
                return _activeTracks;
            }
            set
            {
                _activeTracks = value;
                RaisePropertyChanged("ActiveTracks");
            }
        }

        private ITrackViewModel _selectedTrack;
        public ITrackViewModel SelectedTrack
        {
            get { return _selectedTrack; }
            set
            {
                _selectedTrack = value;
                RaisePropertyChanged("SelectedTrack");
            }
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

        private ObservableCollection<PlaylistMenuItemViewModel> _playlistMenuItems;
        public ObservableCollection<PlaylistMenuItemViewModel> PlaylistMenuItems
        {
            get { return _playlistMenuItems; }
            set
            {
                _playlistMenuItems = value;
                RaisePropertyChanged("PlaylistMenuItems");
            }
        }

        private PlaylistObject _activePlaylist;
        public PlaylistObject ActivePlaylist
        {
            get { return _activePlaylist; }
            set
            {
                _activePlaylist = value;
                RaisePropertyChanged("ActivePlaylist");
            }
        }


        private void LoadCommands()
        {
            AddCustomArtistCommand = new CustomCommand(AddCustomArtist, CanAddCustomArtist);
            UpdateArtistCommand = new CustomCommand(UpdateArtist, CanUpdateArtist);
            UpdateAllArtistsCommand = new CustomCommand(UpdateAllArtists, CanUpdateAllArtists);
            RetrieveMissingAlbumsCommand = new CustomCommand(RetrieveMissingAlbums, CanRetrieveMissingAlbums);
            DeleteArtistCommand = new CustomCommand(DeleteArtist, CanDeleteArtist);

            AddCustomAlbumCommand = new CustomCommand(AddCustomAlbum, CanAddCustomAlbum);
            TrackAlbumCommand = new CustomCommand(TrackAlbum, CanTrackAlbum);
            UntrackAlbumCommand = new CustomCommand(UntrackAlbum, CanUntrackAlbum);
            MarkAlbumListenedCommand = new CustomCommand(MarkAlbumListened, CanMarkAlbumListened);
            RemoveAlbumHistoryCommand = new CustomCommand(RemoveAlbumHistory, CanRemoveAlbumHistory);
            DeleteAlbumCommand = new CustomCommand(DeleteAlbum, CanDeleteAlbum);

            MarkTrackListenedCommand = new CustomCommand(MarkTrackListened, CanMarkTrackListened);
            RemoveTrackHistoryCommand = new CustomCommand(RemoveTrackHistory, CanRemoveTrackHistory);

            AddAlbumToPlaylistCommand = new CustomCommand(AddAlbumToPlaylist, CanAddAlbumToPlaylist);
        }

        private void AddCustomArtist(object obj)
        {
            var dialog = new TextBoxWindow();
            if (dialog.ShowDialog() == true)
            {
                var artist = new CustomArtist()
                {
                    Name = dialog.ResponseText
                };

                _musicLog.AddArtist(artist);
                ActiveArtists.Add(new CustomArtistViewModel(artist));
            }
        }

        private bool CanAddCustomArtist(object obj)
        {
            return true;
        }

        private void UpdateArtist(object obj)
        {
            var prevSelected = new PreviouslySelectedMusicObjects(SelectedArtist, SelectedAlbum, SelectedTrack);
            _musicLog.UpdateArtist(SelectedArtist.Artist);
            Refresh(prevSelected);
        }
        private bool CanUpdateArtist(object obj)
        {
            if (SelectedArtist != null)
            { return true; }
            return false;
        }

        private void UpdateAllArtists(object obj)
        {
            var prevSelected = new PreviouslySelectedMusicObjects(SelectedArtist, SelectedAlbum, SelectedTrack);
            _musicLog.UpdateAllArtists();
            Refresh(prevSelected);
        }
        private bool CanUpdateAllArtists(object obj)
        {
            if (SelectedArtist != null)
            { return true; }
            return false;
        }

        private void RetrieveMissingAlbums(object obj)
        {
            var prevSelected = new PreviouslySelectedMusicObjects(SelectedArtist, SelectedAlbum, SelectedTrack);
            _musicLog.RetrieveMissingAlbums((SpotifyArtist)SelectedArtist.Artist);
            Refresh(prevSelected);
        }
        private bool CanRetrieveMissingAlbums(object obj)
        {
            if (SelectedArtist != null)
            {
                return true;
            }
            return false;
        }

        private void DeleteArtist(object obj)
        {
            var prevSelected = new PreviouslySelectedMusicObjects(SelectedArtist, SelectedAlbum, SelectedTrack);
            _musicLog.RemoveArtist(SelectedArtist.Artist);
            Refresh(prevSelected);
        }
        private bool CanDeleteArtist(object obj)
        {
            if (SelectedArtist != null)
            {
                return true;
            }
            return false;
        }

        private void AddCustomAlbum(object obj)
        {
            var dialog = new TextBoxWindow();
            if (dialog.ShowDialog() == true)
            {
                var album = new CustomAlbum()
                {
                    Name = dialog.ResponseText
                };

                _musicLog.AddAlbum(album, SelectedArtist.Artist);
                ActiveAlbums.Add(new CustomAlbumViewModel(album));
            }
        }

        private bool CanAddCustomAlbum(object obj)
        {
            if (SelectedArtist != null)
            {
                return true;
            }
            return false;
        }

        private void TrackAlbum(object obj)
        {
            SelectedAlbum.Tracked = true;
        }
        private bool CanTrackAlbum(object obj)
        {
            if (SelectedAlbum != null)
            {
                return true;
            }
            return false;
        }

        private void UntrackAlbum(object obj)
        {
            SelectedAlbum.Tracked = false;
        }
        private bool CanUntrackAlbum(object obj)
        {
            if (SelectedAlbum != null)
            {
                return true;
            }
            return false;
        }

        private void MarkAlbumListened(object obj)
        {
            foreach (var track in ActiveTracks)
            {
                track.LastListenedUnix = DateTimeToUnix(DateTime.UtcNow);
            }
        }
        private bool CanMarkAlbumListened(object obj)
        {
            if (ActiveTracks == null ||
                ActiveTracks.Count == 0 ||
                SelectedAlbum == null ||
                SelectedAlbum.ID != ActiveTracks.First().AlbumID)
            {
                return false;
            }
            return true;
        }

        private void RemoveAlbumHistory(object obj)
        {
            foreach (var track in ActiveTracks)
            {
                track.LastListenedUnix = 0;
            }
        }
        private bool CanRemoveAlbumHistory(object obj)
        {
            if (ActiveTracks == null ||
                ActiveTracks.Count == 0 ||
                SelectedAlbum == null ||
                SelectedAlbum.ID != ActiveTracks.First().AlbumID)
            {
                return false;
            }
            return true;
        }

        private void DeleteAlbum(object obj)
        {
            var prevSelected = new PreviouslySelectedMusicObjects(SelectedArtist, SelectedAlbum, SelectedTrack);
            _musicLog.RemoveAlbum(SelectedAlbum.Album);
            Refresh(prevSelected);
        }
        private bool CanDeleteAlbum(object obj)
        {
            if (SelectedAlbum != null)
            {
                return true;
            }
            return false;
        }

        private void MarkTrackListened(object obj)
        {
            SelectedTrack.LastListenedUnix = DateTimeToUnix(DateTime.UtcNow);
        }
        private bool CanMarkTrackListened(object obj)
        {
            if (SelectedTrack != null)
            {
                return true;
            }
            return false;
        }

        private void RemoveTrackHistory(object obj)
        {
            SelectedTrack.LastListenedUnix = 0;
        }
        private bool CanRemoveTrackHistory(object obj)
        {
            if (SelectedTrack != null)
            {
                return true;
            }
            return false;
        }

        private void AddAlbumToPlaylist(object obj)
        {
            ((PlaylistObject)obj).Add(SelectedAlbum.ID);
        }

        private bool CanAddAlbumToPlaylist(object obj)
        {
            if (SelectedAlbum != null &&
                obj.GetType() == typeof(PlaylistObject))
            {
                return true;
            }
            return false;
        }




        private void GetDatabaseArtists()
        {
            ActiveArtists = MusicObjectProvider.MakeObservableArtists(_musicLog);
            ActiveArtists.OrderByDescending(a => a.Name);
        }
        private void GetDatabaseAlbums()
        {
            if (SelectedArtist != null)
            {
                ActiveAlbums = MusicObjectProvider.MakeObservableAlbums(SelectedArtist, _musicLog);
                ActiveAlbums.OrderByDescending(a => a.Name);
            }

        }
        private void GetDatabaseTracks()
        {
            if (SelectedAlbum != null)
            {
                ActiveTracks = MusicObjectProvider.MakeObservableTracks(SelectedAlbum, _musicLog);
                ActiveTracks.OrderByDescending(a => a.TrackNo);
            }
        }

        private int DateTimeToUnix(DateTime time)
        {
            var dateTimeOffset = new DateTimeOffset(time);
            return (int)dateTimeOffset.ToUnixTimeSeconds();
        }

        private void Refresh(PreviouslySelectedMusicObjects prev)
        {
            // Manual refreshing, required when _musicLog database modifications are made
            // Will take in saved previously selected entries and attempt to reselect
            ActiveTracks = null;
            ActiveAlbums = null;
            ActiveArtists = null;
            GetDatabaseArtists();

            var artistIDQuery = ActiveArtists.Where(a => a.ID == prev.Artist.ID);
            if (prev.Artist == null || !artistIDQuery.Any()) { return; }
            SelectedArtist = prev.Artist;
            GetDatabaseAlbums();

            var albumIDQuery = ActiveAlbums.Where(a => a.ID == prev.Album.ID);
            if (prev.Album == null || !albumIDQuery.Any()) { return; }
            SelectedAlbum = prev.Album;
            GetDatabaseTracks();

            var trackIDQuery = ActiveTracks.Where(t => t.ID == prev.Track.ID);
            if (prev.Track == null || !trackIDQuery.Any()) { return; }
            SelectedTrack = prev.Track;
        }

        private void GetPlaylists()
        {
            Playlists = _musicLog.GetPlaylists().ToObservableCollection();
        }

        private void PopulatePlaylistMenuItems()
        {
            PlaylistMenuItems = new ObservableCollection<PlaylistMenuItemViewModel>();
            foreach (var playlist in Playlists)
            {
                var playlistMenu = new PlaylistMenuItemViewModel();
                playlistMenu.Header = playlist.Name;
                playlistMenu.Playlist = playlist;
                playlistMenu.AssignCommand(AddAlbumToPlaylistCommand);
                PlaylistMenuItems.Add(playlistMenu);
            }
        }



        struct PreviouslySelectedMusicObjects
        {
            public IArtistViewModel Artist { get; }
            public IAlbumViewModel Album { get; }
            public ITrackViewModel Track { get; }

            public PreviouslySelectedMusicObjects(IArtistViewModel artist, IAlbumViewModel album, ITrackViewModel track)
            {
                this.Artist = artist;
                this.Album = album;
                this.Track = track;
            }

        }

    }
}
