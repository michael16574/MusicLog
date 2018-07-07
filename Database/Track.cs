using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLog.Database
{
    public class Track
    {
        public Track()
        {
            
        }

        public Track(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }

        public string Name;
        public int TrackNo;
        public int LastListenedUTS;
    }
}
