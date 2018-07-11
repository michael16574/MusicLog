using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLog.Database
{
    public class MusicObjectTable
    {
        public List<Artist> Artists;
        public List<Album> Albums;
        public List<Track> Tracks;

        public MusicObjectTable()
        {
            this.Artists = new List<Artist>();
            this.Albums = new List<Album>();
            this.Tracks = new List<Track>();
        }

        public MusicObjectTable(List<Artist> artists, List<Album> albums, List<Track> tracks)
        {
            this.Artists = artists;
            this.Albums = albums;
            this.Tracks = tracks;
        }
    }
}
