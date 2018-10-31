using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MusicLog;

namespace MusicLogWPF
{
    public class PlaylistMenuItemViewModel
    {
        private ICommand _command;

        public PlaylistMenuItemViewModel() { }

        public string Header { get; set; }

        public ObservableCollection<PlaylistMenuItemViewModel> PlaylistMenuItems { get; set; }

        public PlaylistObject Playlist { get; set; }

        public ICommand Command
        {
            get
            {
                return _command;
            }
        }

        public void AssignCommand(ICommand command)
        {
            _command = command;
        }
    }

}
