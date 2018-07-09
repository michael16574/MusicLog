using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicLog
{
    public partial class DatabaseModule : UserControl
    {
        private static DatabaseModule _instance;
        private SpotifyAPI.Web.SpotifyWebAPI _spotifyAuth;
        private Database.DatabaseInstance _database;

        public static DatabaseModule Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseModule();
                }
                return _instance;
            }
        }

        public void UpdateDatabase(Database.DatabaseInstance database)
        {
            _database = database;
        }

        public void UpdateSpotifyAuth(SpotifyAPI.Web.SpotifyWebAPI auth)
        {
            _spotifyAuth = auth;
        }


        public DatabaseModule()
        {
            InitializeComponent();
        }

        private void DatabaseModule_Load(object sender, EventArgs e)
        {

        }

        public void PopulateArtistList()
        {
            // Setting up listview
            listView3.Items.Clear();

            if (listView3.Columns.Count != 1)
            {
                listView3.Columns.Clear();
                listView3.Columns.Add("Artist", -2, HorizontalAlignment.Left);
            }

            // Adding artists
            foreach (var artist in _database.Artists)
            {
                var newItm = new ListViewItem(artist.Name);
                newItm.Tag = artist;
                listView3.Items.Add(newItm);
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Setting up listview
            listView1.Items.Clear();

            if (listView1.Columns.Count != 3)
            {
                listView1.Columns.Clear();
                listView1.Columns.Add("Track", 50, HorizontalAlignment.Left);
                listView1.Columns.Add("Name", 250, HorizontalAlignment.Left);
                listView1.Columns.Add("Time", -2, HorizontalAlignment.Right);
            }

            // Retrieving track information
            ListViewItem lstViewItem = listView2.FocusedItem;
            var selectedAlbum = lstViewItem.Tag as Database.Album;

            foreach (var track in selectedAlbum.Tracks)
            {
                string time = GetTimeFromUnix(track.LastListenedUTS);

                var newItm = new ListViewItem(track.TrackNo.ToString());
                newItm.SubItems.Add(new ListViewItem.ListViewSubItem(newItm, track.Name));
                newItm.SubItems.Add(new ListViewItem.ListViewSubItem(newItm, time));
                newItm.Tag = track;
                listView1.Items.Add(newItm);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var selectedAlbums = new List<Database.Album>();
            selectedAlbums.Add((Database.Album)listView2.FocusedItem.Tag);
            _database.TrackAlbums(selectedAlbums);

            // Adding tickmark to newly added album in list
            string tickMark = "\u2713";
            listView2.Items[listView2.FocusedItem.Index].SubItems[1].Text = tickMark;
        }

        private void trackAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void trackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Updating track UTS in database
            var selectedTrack = (Database.Track)listView1.SelectedItems[0].Tag;
            selectedTrack.UpdateHistory(DateTime.UtcNow);

            // Updating listview
            listView2_SelectedIndexChanged(this, EventArgs.Empty);

        }

        

        private string GetTimeFromUnix(int uts)
        {
            string time = "";
            if (uts != 0)
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(uts);
                time = dateTimeOffset.DateTime.ToLocalTime().ToString();
            }
            else
            {
                time = "N/A";
            }

            return time;
        }

        private void markListenedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Updating track UTS in database
            var selectedAlbum = (Database.Album)listView2.SelectedItems[0].Tag;
            selectedAlbum.UpdateHistory(DateTime.UtcNow);

            // Updating listview
            listView2_SelectedIndexChanged(this, EventArgs.Empty);
        }

        

        private void deleteHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Updating track UTS in database
            var selectedTrack = (Database.Track)listView1.SelectedItems[0].Tag;
            selectedTrack.UpdateHistory(0);

            // Updating listview
            listView2_SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void deleteHistoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Updating track UTS in database
            var selectedAlbum = (Database.Album)listView2.SelectedItems[0].Tag;
            selectedAlbum.UpdateHistory(0);

            // Updating listview
            listView2_SelectedIndexChanged(this, EventArgs.Empty);
        }   

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Setting up listview
            listView2.Items.Clear();

            if (listView2.Columns.Count != 2)
            {
                listView2.Columns.Clear();
                listView2.Columns.Add("Album", 300, HorizontalAlignment.Left);
                listView2.Columns.Add("Tracked", -2, HorizontalAlignment.Center);
            }

            // Retrieving album information
            ListViewItem lstViewItem = listView3.FocusedItem;
            var selectedArtist = lstViewItem.Tag as Database.Artist;

            string tickMark = "\u2713";
            foreach (var album in selectedArtist.Albums)
            {

                var newItm = new ListViewItem(album.Name);
                newItm.Tag = album;
                if (album.Tracked)
                {
                    newItm.SubItems.Add(new ListViewItem.ListViewSubItem(newItm, tickMark));
                }
                else
                {
                    newItm.SubItems.Add(new ListViewItem.ListViewSubItem(newItm, ""));
                }
                listView2.Items.Add(newItm);
            }
        }

        private void untrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Removing selected album
            var selectedArtist = (Database.Artist)listView3.SelectedItems[0].Tag;
            var selectedAlbum = (Database.Album)listView2.SelectedItems[0].Tag;
            selectedArtist.Albums.Remove(selectedAlbum);

            // Updating listview
            listView3_SelectedIndexChanged(this, EventArgs.Empty);
            listView1.Items.Clear();
        }

        private void deleteArtistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Removing selected artist
            var selectedArtist = (Database.Artist)listView3.SelectedItems[0].Tag;
            _database.RemoveArtist(selectedArtist);

            // Updating listview
            PopulateArtistList();
            listView2.Items.Clear();
            listView1.Items.Clear();
        }

        private void listView3_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {          
            e.NewWidth = listView3.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void listView3_MouseClick(object sender, MouseEventArgs e)
        {
            // Prevents right click on header
            ListView listView = sender as ListView;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ListViewItem item = listView.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    item.Selected = true;
                    listView.ContextMenuStrip.Show(listView, e.Location);
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // Prevents right click on white space
            var contextMenu = (ContextMenuStrip)sender;
            var listView = (ListView)contextMenu.SourceControl;
            if (listView.SelectedItems.Count <= 0)
            {
                e.Cancel = true;
            }
            
        }

        private void retrieveMissingAlbumsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Adding missing albums
            var selectedArtist = (Database.Artist)listView3.SelectedItems[0].Tag;
            var retrievedAlbums = Spotify.SpotifyUtilities.GetAlbums(selectedArtist, _spotifyAuth);
            foreach (Database.Album album in retrievedAlbums)
            {
                album.Tracks = Spotify.SpotifyUtilities.GetTracks(album, _spotifyAuth);
            }
            _database.AddAlbums(retrievedAlbums, selectedArtist);

            // Updating listview
            listView3_SelectedIndexChanged(this, EventArgs.Empty);
            listView1.Items.Clear();

        }
    }
}
