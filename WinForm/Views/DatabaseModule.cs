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
            ArtistListView.Items.Clear();

            if (ArtistListView.Columns.Count != 1)
            {
                ArtistListView.Columns.Clear();
                ArtistListView.Columns.Add("Artist", -2, HorizontalAlignment.Left);
            }

            // Adding artists (EXTRACT)
            foreach (var artist in _database.Artists)
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
            var selectedAlbum = lstViewItem.Tag as Database.Album;

            foreach (var track in selectedAlbum.Tracks)
            {
                string time = GetTimeFromUnix(track.LastListenedUTS);

                var newItm = new ListViewItem(track.TrackNo.ToString());
                newItm.SubItems.Add(new ListViewItem.ListViewSubItem(newItm, track.Name));
                newItm.SubItems.Add(new ListViewItem.ListViewSubItem(newItm, time));
                newItm.Tag = track;
                TrackListView.Items.Add(newItm);
            }
        }



        private void TrackSelectedButton_Click(object sender, EventArgs e)
        {
            var selectedAlbums = new List<Database.Album>();
            selectedAlbums.Add((Database.Album)AlbumListView.FocusedItem.Tag);
            _database.TrackAlbums(selectedAlbums);

            // Adding tickmark to newly added album in list
            string tickMark = "\u2713";
            AlbumListView.Items[AlbumListView.FocusedItem.Index].SubItems[1].Text = tickMark;
        }

        

        private void MarkTrackListened_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Updating track UTS in database
            var selectedTrack = (Database.Track)TrackListView.SelectedItems[0].Tag;
            selectedTrack.UpdateHistory(DateTime.UtcNow);

            // Updating listview
            AlbumListView_SelectedIndexChanged(this, EventArgs.Empty);

        }      

        private void MarkListened_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Updating track UTS in database
            var selectedAlbum = (Database.Album)AlbumListView.SelectedItems[0].Tag;
            selectedAlbum.UpdateHistory(DateTime.UtcNow);

            // Updating listview
            AlbumListView_SelectedIndexChanged(this, EventArgs.Empty);
        }
        
        private void DeleteTrack_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Updating track UTS in database
            var selectedTrack = (Database.Track)TrackListView.SelectedItems[0].Tag;
            selectedTrack.UpdateHistory(0);

            // Updating listview
            AlbumListView_SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void RemoveAlbumHistory_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Updating track UTS in database
            var selectedAlbum = (Database.Album)AlbumListView.SelectedItems[0].Tag;
            selectedAlbum.UpdateHistory(0);

            // Updating listview
            AlbumListView_SelectedIndexChanged(this, EventArgs.Empty);
        }         

        private void DeleteAlbum_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Removing selected album
            var selectedArtist = (Database.Artist)ArtistListView.SelectedItems[0].Tag;
            var selectedAlbum = (Database.Album)AlbumListView.SelectedItems[0].Tag;
            selectedArtist.Albums.Remove(selectedAlbum);

            // Updating listview
            ArtistListView_SelectedIndexChanged(this, EventArgs.Empty);
            TrackListView.Items.Clear();
        }

        private void DeleteArtist_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Removing selected artist
            var selectedArtist = (Database.Artist)ArtistListView.SelectedItems[0].Tag;
            _database.RemoveArtist(selectedArtist);

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
            var selectedArtist = (Database.Artist)ArtistListView.SelectedItems[0].Tag;
            var retrievedAlbums = WebApi.Spotify.SpotifyApi.GetAlbums(selectedArtist, _spotifyAuth);
            foreach (Database.Album album in retrievedAlbums)
            {
                album.Tracks = WebApi.Spotify.SpotifyApi.GetTracks(album, _spotifyAuth);
            }
            _database.AddAlbums(retrievedAlbums, selectedArtist);

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
