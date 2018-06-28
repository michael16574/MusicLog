using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Album
    {
        public Album()
        {
            Tracks = new List<Track>();
        }
        public string Name;
        public string Id;
        public List<Track> Tracks;
    }
}
