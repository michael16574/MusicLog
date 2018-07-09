using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLog.Database
{
    public class Album
    {
        public Album()
        {
            Tracks = new List<Track>();
            Tracked = false;
        }
        public Album(string name, string id)
        {
            Name = name;
            Id = id;
            Tracked = false;
        }
        public override string ToString()
        {
            return Name;
        }

        public void UpdateHistory(int uts)
        {
            foreach (Track track in Tracks)
            {
                track.UpdateHistory(uts);
            }           
        }

        public void UpdateHistory(DateTime time)
        {
            var dateTimeOffset = new DateTimeOffset(time);
            int uts = (int)dateTimeOffset.ToUnixTimeSeconds();

            foreach (Track track in Tracks)
            {
                track.UpdateHistory(uts);
            }
        }

        public string Name;
        public string Id;
        public bool Tracked;
        public List<Track> Tracks;
    }
}
