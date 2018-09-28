using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLog
{
    public class PlaylistObject
    {
        public string Name { get; set; }
        public List<string> Albums { get; set; }
        public string ID { get; set; }

        public PlaylistObject()
        {
            Albums = new List<string>();
            ID = "playlist:" + Guid.NewGuid();           
        }

        public void Add(string id)
        {
            Albums.Add(id);
        }
        public void Remove(string id)
        {
            foreach (string albumID in Albums)
            {
                if (id == albumID)
                { Albums.Remove(id); }
            }
        }
        public void Wipe()
        {
            Albums.Clear();
        }

    }
}
