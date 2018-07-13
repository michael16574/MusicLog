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

            settings.Creds.SpotifyUser = "eriejar";
            settings.Creds.SpotifyID = "86a63babc6bd4c84a6d49bd42ceec7b7";
            settings.Creds.SpotifySecret = "1b7a620bd2504a79866398e4f2aadaff";
            settings.Creds.LastFMUser = "eriejar";
            settings.Creds.LastFMKey = "dc1c134531fc3eaa8ba716cc71fdcde9";

            var musicLogProgram = new MusicLogApi(settings);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(musicLogProgram));
        }
    }

}
