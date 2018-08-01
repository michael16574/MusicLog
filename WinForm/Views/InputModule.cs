using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpotifyAPI;

namespace MusicLog
{
    public partial class InputModule : UserControl
    {
        private static InputModule _instance;
        private MusicLogClient _musicLog;

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

        public void UpdateMusicLog(MusicLogClient musicLog)
        {
            _musicLog = musicLog;
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

            // Populating list of artists
            List<Artist> artists = _musicLog.GetSpotifyArtists(query);
            foreach (Artist artist in artists)
            {
                List<Album> albums = _musicLog.GetSpotifyAlbums(artist);
                if (albums.Count == 0)
                {
                    continue;
                }

                TreeNode treeNode = new TreeNode(artist.Name);
                treeNode.Tag = artist.SpotifyID;

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
            var checkedArtists = new List<Artist>();

            // Adds any checked albums to database
            foreach(TreeNode node in treeView1.Nodes)
            {
                if (node.Checked)
                {
                    var newArtist = new Artist();
                    newArtist.Name = node.Text;
                    newArtist.SpotifyID = (string)node.Tag;
                    checkedArtists.Add(newArtist);
                }
            }

            List<Album> allSpotifyAlbums = new List<Album>();
            foreach(Artist artist in checkedArtists)
            {
                var albumQuery = _musicLog.GetSpotifyAlbums(artist);
                _musicLog.AddAlbums(albumQuery, artist);
                allSpotifyAlbums.AddRange(albumQuery);
            }

            foreach(Album album in allSpotifyAlbums)
            {
                var trackQuery = _musicLog.GetSpotifyTracks(album);
                _musicLog.AddTracks(trackQuery, album);
            }

            _musicLog.Save();
        }

        
    }
}
