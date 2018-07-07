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


        public DatabaseModule()
        {
            InitializeComponent();
        }

        private void DatabaseModule_Load(object sender, EventArgs e)
        {

        }

        public void PopulateArtistList()
        {
            listBox1.DataSource = _database.Artists;
            listBox1.DisplayMember = "Name";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView2.Items.Clear();

            if (listView2.Columns.Count != 2)
            {
                listView2.Columns.Clear();
                listView2.Columns.Add("Album", 300, HorizontalAlignment.Left);
                listView2.Columns.Add("Tracked", -2, HorizontalAlignment.Center);
            }

            // Retrieving album information
            ListBox lstBox = (ListBox)sender;
            var selectedArtist = lstBox.SelectedItem as Database.Artist;

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

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            if (listView1.Columns.Count != 3)
            {
                listView1.Columns.Clear();
                listView1.Columns.Add("Track", 50, HorizontalAlignment.Left);
                listView1.Columns.Add("Name", 300, HorizontalAlignment.Left);
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
            var dateTimeOffset = new DateTimeOffset(DateTime.UtcNow);
            int uts = (int)dateTimeOffset.ToUnixTimeSeconds();
            selectedTrack.LastListenedUTS = uts;

            // Updating listview
            listView1.SelectedItems[0].SubItems[2].Text = GetTimeFromUnix(uts);

        }

        private void contextMenuStrip3_Opening(object sender, CancelEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                e.Cancel = true;
            }
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
    }
}
