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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void CreateSampleDatabase()
        {
            Database.DatabaseInstance activeDatabase = new Database.DatabaseInstance();

            var artists = new List<Database.Artist>();
            for (int i = 0; i < 100; i++)
            {
                var newArtist = new Database.Artist();
                newArtist.Name = "Sample_Artist #" + i.ToString();
                newArtist.SpotifyID = i.ToString();

                var albums = new List<Database.Album>();
                for (int j = 0; j < 5; j++)
                {
                    var newAlbum = new Database.Album();
                    newAlbum.Name = "Sample_Album #" + j.ToString();
                    newAlbum.SpotifyID = j.ToString();

                    var songs = new List<Database.Track>();
                    for (int k = 0; k < 15; k++)
                    {
                        var NewTrack = new Database.Track("Sample_Track #" + k);
                        songs.Add(NewTrack);
                    }
                    newAlbum.Tracks.AddRange(songs);

                    albums.Add(newAlbum);
                }
                newArtist.Albums.AddRange(albums);

                artists.Add(newArtist);
            }
            activeDatabase.AddArtists(artists);




            activeDatabase.Save("database.xml");

            activeDatabase.Load("database.xml");
        }
    }

}
