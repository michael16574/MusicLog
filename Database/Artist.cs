using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLog.Database
{
    public class Artist
    {
        public Artist()
        {
            Albums = new List<Album>();
        }
        public Artist(string name, string id, List<Album> albums)
        {
            Name = name;
            Id = id;
            Albums = albums;
        }

        public override string ToString()
        {
            return Name;
        }
        public string Name;
        public string Id;
        public List<Album> Albums;
    }
}
