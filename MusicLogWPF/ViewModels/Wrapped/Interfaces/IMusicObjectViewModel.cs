using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLogWPF
{
    public interface IMusicObjectViewModel
    {
        string Name { get; set; }
        string ID { get; set; }
    }
}
