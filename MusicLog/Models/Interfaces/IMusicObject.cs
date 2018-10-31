using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLog
{
    public interface IMusicObject
    {
        string Name { get; set; }
        string ID { get; set; }
    }
}
