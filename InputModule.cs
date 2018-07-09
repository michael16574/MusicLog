using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MusicLog.Spotify;
using SpotifyAPI;

namespace MusicLog
{
    public partial class InputModule : UserControl
    {
        private static InputModule _instance;
        private SpotifyAPI.Web.SpotifyWebAPI _spotifyAuth;
        private Database.DatabaseInstance _database;

        public static InputModule Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new InputModule();
                }
                return _instance;
            }
        }



        public InputModule()
        {
            InitializeComponent();
        }

        public void UpdateDatabase(Database.DatabaseInstance database)
        {
            _database = database;
        }

        public void UpdateSpotifyAuth(SpotifyAPI.Web.SpotifyWebAPI auth)
        {
            _spotifyAuth = auth;
        }

        private void InputModule_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void search_Click(object sender, EventArgs e)
        {
            
            string query = textBox1.Text;
            _spotifyAuth = SpotifyUtilities.GetAuthObj();

            // Populating list of artists
            List<Database.Artist> artists = SpotifyUtilities.GetArtists(query, _spotifyAuth);
            foreach (Database.Artist artist in artists)
            {
                artist.Albums = SpotifyUtilities.GetAlbums(artist, _spotifyAuth);
                if (artist.Albums.Count == 0)
                {
                    continue;
                }

                TreeNode treeNode = new TreeNode(artist.Name);
                treeNode.Tag = artist.Id;

                treeView1.Nodes.Add(treeNode);
            }
            
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var checkedArtists = new List<Database.Artist>();

            // Adds any checked albums to database
            foreach(TreeNode node in treeView1.Nodes)
            {
                if (node.Checked)
                {
                    var newArtist = new Database.Artist();
                    newArtist.Name = node.Text;
                    newArtist.Id = (string)node.Tag;
                    checkedArtists.Add(newArtist);
                }
            }
            
            foreach(Database.Artist artist in checkedArtists)
            {
                artist.Albums = SpotifyUtilities.GetAlbums(artist, _spotifyAuth);
            }

            var allAlbums = checkedArtists.SelectMany(a => a.Albums).ToList();
            foreach(Database.Album album in allAlbums)
            {
                album.Tracks = SpotifyUtilities.GetTracks(album, _spotifyAuth);
            }

            _database.AddArtists(checkedArtists);
            _database.SerializeDatabase("database.xml"); 
        }

        
    }
}
