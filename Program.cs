using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicLog
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var settings = new UserSettings();

            var musicLogProgram = new MusicLogApi(settings);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(musicLogProgram));
        }
    }

}
