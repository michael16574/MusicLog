using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLogWPF
{
    public class DoubleClickedEventArgs : EventArgs
    {
        private string _artistName;
        private string _albumName;

        public DoubleClickedEventArgs(string artistName, string albumName)
        {
            _artistName = artistName;
            _albumName = albumName;
        }

        public string ArtistName
        {
            get { return _artistName; }
        }
        public string AlbumName
        {
            get { return _albumName; }
        }
    }
}
