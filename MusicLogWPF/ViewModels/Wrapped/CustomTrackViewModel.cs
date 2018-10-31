using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLog;

namespace MusicLogWPF
{
    public class CustomTrackViewModel : ITrackViewModel, INotifyPropertyChanged, IComparable
    {
        private ITrack _track;

        public ITrack Track
        {
            get { return _track; }
        }

        public CustomTrackViewModel(CustomTrack track)
        {
            _track = track;
            _visible = true;
        }

        public string Name
        {
            get { return Track.Name; }
            set
            {
                Track.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string ArtistID
        {
            get { return Track.ArtistID; }
            set
            {
                Track.ArtistID = value;
                RaisePropertyChanged("ArtistID");
            }
        }

        public string AlbumID
        {
            get { return Track.AlbumID; }
            set
            {
                Track.AlbumID = value;
                RaisePropertyChanged("AlbumID");
            }
        }

        public string ID
        {
            get { return Track.ID; }
            set
            {
                Track.ID = value;
                RaisePropertyChanged("TrackID");
            }
        }

        public int TrackNo
        {
            get { return Track.TrackNo; }
            set
            {
                Track.TrackNo = value;
                RaisePropertyChanged("TrackNo");
            }
        }

        public int LastListenedUnix
        {
            get { return Track.LastListenedUnix; }
            set
            {
                Track.LastListenedUnix = value;
                RaisePropertyChanged("LastListenedUnix");
            }
        }

        private bool _visible;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                RaisePropertyChanged("Visible");
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

        public int CompareTo(object obj)
        {
            var comparee = (CustomTrackViewModel)obj;
            return this.ID.CompareTo(comparee.ID);
        }
    }
}
