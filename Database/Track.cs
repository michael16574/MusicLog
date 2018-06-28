using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Track
    {
        public Track()
        {
            ListenedState = false;
        }
        public Track(string name)
        {
            Name = name;
            ListenedState = false;
        }
        public string Name;
        public int LastListenedUTS;
        public bool ListenedState;
    }
}
