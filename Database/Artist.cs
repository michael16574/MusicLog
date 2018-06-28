using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Artist
    {
        public Artist()
        {
            Albums = new List<Album>();
        }
        public string Name;
        public string Id;
        public List<Album> Albums;
    }
}
