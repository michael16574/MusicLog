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
        private MusicLogClient _musicLog;

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

        public void UpdateMusicLog(MusicLogClient musicLog)
        {
            _musicLog = musicLog;
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
            ArtistListView.Items.Clear();

            if (ArtistListView.Columns.Count != 1)
            {
                ArtistListView.Columns.Clear();
                ArtistListView.Columns.Add("Artist", -2, HorizontalAlignment.Left);
            }

            // Adding artists (EXTRACT)
            foreach (var artist in _musicLog.GetArtists())
            {
                var newItm = new ListViewItem(artist.Name);
                newItm.Tag = artist;
                ArtistListView.Items.Add(newItm);
            }
        }

        private void ArtistListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Setting up listview
            AlbumListView.Items.Clear();

            if (AlbumListView.Columns.Count != 2)
            {
                AlbumListView.Columns.Clear();
                AlbumListView.Columns.Add("Album", 300, HorizontalAlignment.Left);
                AlbumListView.Columns.Add("Tracked", -2, HorizontalAlignment.Center);
            }

            // Retrieving album information
            ListViewItem lstViewItem = ArtistListView.FocusedItem;
            var selectedArtist = lstViewItem.Tag as Artist;

            string tickMark = "\u2713";
            foreach (var album in _musicLog.GetAlbums(selectedArtist))
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
                AlbumListView.Items.Add(newItm);
            }
        }

        private void AlbumListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Setting up listview
            TrackListView.Items.Clear();

            if (TrackListView.Columns.Count != 3)
            {
                TrackListView.Columns.Clear();
                TrackListView.Columns.Add("Track", 50, HorizontalAlignment.Left);
                TrackListView.Columns.Add("Name", 250, HorizontalAlignment.Left);
                TrackListView.Columns.Add("Time", -2, HorizontalAlignment.Right);
            }

            // Retrieving track information
            ListViewItem lstViewItem = AlbumListView.FocusedItem;
            var selectedAlbum = lstViewItem.Tag as Album;

            foreach (var track in _musicLog.GetTracks(selectedAlbum))
            {
                string time = GetTimeFromUnix(track.LastListenedUnix);

                var newItm = new ListViewItem(track.TrackNo.ToString());
                newItm.SubItems.Add(new ListViewItem.ListViewSubItem(newItm, track.Name));
                newItm.SubItems.Add(new ListViewItem.ListViewSubItem(newItm, time));
                newItm.Tag = track;
                TrackListView.Items.Add(newItm);
            }
        }



        private void TrackSelectedButton_Click(object sender, EventArgs e)
        {
            var selectedAlbums = new List<Album>();
            selectedAlbums.Add((Album)AlbumListView.FocusedItem.Tag);
            foreach (Album album in selectedAlbums)
            {
                album.Tracked = true;
            }          

            // Adding tickmark to newly added album in list
            string tickMark = "\u2713";
            AlbumListView.Items[AlbumListView.FocusedItem.Index].SubItems[1].Text = tickMark;
        }

        

        private void MarkTrackListened_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Updating track UTS in database
            var selectedTrack = (Track)TrackListView.SelectedItems[0].Tag;
            _musicLog.UpdateHistory(DateTime.UtcNow, selectedTrack);

            // Updating listview
            AlbumListView_SelectedIndexChanged(this, EventArgs.Empty);

        }      

        private void MarkListened_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Updating track UTS in database
            var selectedAlbum = (Album)AlbumListView.SelectedItems[0].Tag;
            _musicLog.UpdateHistory(DateTime.UtcNow, selectedAlbum);

            // Updating listview
            AlbumListView_SelectedIndexChanged(this, EventArgs.Empty);
        }
        
        private void DeleteTrack_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Updating track UTS in database
            var selectedTrack = (Track)TrackListView.SelectedItems[0].Tag;
            _musicLog.UpdateHistory(0, selectedTrack);

            // Updating listview
            AlbumListView_SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void RemoveAlbumHistory_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Updating track UTS in database
            var selectedAlbum = (Album)AlbumListView.SelectedItems[0].Tag;
            _musicLog.UpdateHistory(0, selectedAlbum);

            // Updating listview
            AlbumListView_SelectedIndexChanged(this, EventArgs.Empty);
        }         

        private void DeleteAlbum_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Removing selected album
            var selectedAlbum = (Album)AlbumListView.SelectedItems[0].Tag;
            _musicLog.RemoveAlbum(selectedAlbum);

            // Updating listview
            ArtistListView_SelectedIndexChanged(this, EventArgs.Empty);
            TrackListView.Items.Clear();
        }

        private void DeleteArtist_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Removing selected artist
            var selectedArtist = (Artist)ArtistListView.SelectedItems[0].Tag;
            _musicLog.RemoveArtist(selectedArtist);

            // Updating listview
            PopulateArtistList();
            AlbumListView.Items.Clear();
            TrackListView.Items.Clear();
        }



        private void ArtistListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {          
            e.NewWidth = ArtistListView.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void PreventRightClickOnHeader_ListView_MouseClick(object sender, MouseEventArgs e)
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

        private void PreventRightClick_ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            // Prevents right click on white space
            var contextMenu = (ContextMenuStrip)sender;
            var listView = (ListView)contextMenu.SourceControl;
            if (listView.SelectedItems.Count <= 0)
            {
                e.Cancel = true;
            }            
        }

        private void RetrieveMissingAlbums_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Adding missing albums
            var selectedArtist = (Artist)ArtistListView.SelectedItems[0].Tag;
            var retrievedAlbums = _musicLog.GetSpotifyAlbums(selectedArtist);

            _musicLog.AddAlbums(retrievedAlbums, selectedArtist);
            foreach (Album album in retrievedAlbums)
            {
                _musicLog.AddTracks(_musicLog.GetSpotifyTracks(album), album);
            }

            // Updating listview
            ArtistListView_SelectedIndexChanged(this, EventArgs.Empty);
            TrackListView.Items.Clear();
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
