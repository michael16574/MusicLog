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
        public List<Guid> Albums { get; set; }
        public Guid ID { get; set; }

        public PlaylistObject()
        {
            Albums = new List<Guid>();
            ID = Guid.NewGuid();           
        }

        public PlaylistObject(string name)
        {
            Name = name;
            Albums = new List<Guid>();
            ID = Guid.NewGuid();
        }

        public PlaylistObject(string name, List<Guid> albums)
        {
            Name = name;
            Albums = albums;
            ID = Guid.NewGuid();
        }

        public void Add(Guid guid)
        {
            Albums.Add(guid);
        }
        public void Remove(Guid guid)
        {
            foreach (Guid id in Albums)
            {
                if (id == guid)
                { Albums.Remove(id); }
            }
        }
        public void Wipe()
        {
            Albums.Clear();
        }

    }
}
