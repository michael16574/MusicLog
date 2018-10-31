using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLog;

namespace MusicLogWPF
{
    public class CustomArtistViewModel : IArtistViewModel, INotifyPropertyChanged
    {
        private IArtist _artist;

        public IArtist Artist
        {
            get { return _artist; }
        }

        public CustomArtistViewModel(CustomArtist artist)
        {
            _artist = artist;
        }

        public string Name
        {
            get { return Artist.Name; }
            set
            {
                Artist.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string ID
        {
            get { return Artist.ID; }
            set
            {
                Artist.ID = value;
                RaisePropertyChanged("ArtistID");
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
