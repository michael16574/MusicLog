using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLog;

namespace MusicLogWPF
{
    public class SpotifyAlbumViewModel : ISpotifyMusicObjectViewModel, IAlbumViewModel, INotifyPropertyChanged
    {
        private IAlbum _album;
        private string _albumProgress;
        private string _artistName;
        


        public IAlbum Album
        {
            get { return _album; }
        }

        public SpotifyAlbumViewModel(SpotifyAlbum album)
        {
            _album = album;
        }

        public string Name
        {
            get { return Album.Name; }
            set
            {
                Album.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string SpotifyID
        {
            get { return ((SpotifyAlbum)Album).SpotifyID; }
            set
            {
                ((SpotifyAlbum)Album).SpotifyID = value;
                RaisePropertyChanged("SpotifyID");
            }
        }

        public string ArtistID
        {
            get { return Album.ArtistID; }
            set
            {
                Album.ArtistID = value;
                RaisePropertyChanged("ArtistID");
            }
        }

        public string ID
        {
            get { return Album.ID; }
            set
            {
                Album.ID = value;
                RaisePropertyChanged("AlbumID");
            }
        }

        public bool Tracked
        {
            get { return Album.Tracked; }
            set
            {
                Album.Tracked = value;
                RaisePropertyChanged("Tracked");
            }
        }

        public string AlbumProgress
        {
            get { return _albumProgress; }
            set
            {
                _albumProgress = value;
                RaisePropertyChanged("AlbumProgress");
            }
        }

        public string ArtistName
        {
            get { return _artistName; }
            set
            {
                _artistName = value;
                RaisePropertyChanged("ArtistName");
            }
        }
            

        public void SetProgress(int listenedTrackCount, int totalTrackCount)
        {
            AlbumProgress = listenedTrackCount.ToString() + "/" + totalTrackCount.ToString();
        }

        public void SetArtist(string artistName)
        {
            ArtistName = artistName;
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
