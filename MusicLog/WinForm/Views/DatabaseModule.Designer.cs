namespace MusicLog
{
    partial class DatabaseModule
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TrackSelectedButton = new System.Windows.Forms.Button();
            this.TrackListView = new System.Windows.Forms.ListView();
            this.TrackContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.trackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AlbumListView = new System.Windows.Forms.ListView();
            this.AlbumContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.markListenedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteHistoryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.untrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ArtistListView = new System.Windows.Forms.ListView();
            this.ArtistContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.retrieveMissingAlbumsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteArtistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseInstanceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.TrackContextMenuStrip.SuspendLayout();
            this.AlbumContextMenuStrip.SuspendLayout();
            this.ArtistContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.databaseInstanceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.TrackSelectedButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.TrackListView, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.AlbumListView, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ArtistListView, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1492, 749);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // TrackSelectedButton
            // 
            this.TrackSelectedButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrackSelectedButton.Location = new System.Drawing.Point(301, 706);
            this.TrackSelectedButton.Name = "TrackSelectedButton";
            this.TrackSelectedButton.Size = new System.Drawing.Size(590, 40);
            this.TrackSelectedButton.TabIndex = 3;
            this.TrackSelectedButton.Text = "Track Selected ";
            this.TrackSelectedButton.UseVisualStyleBackColor = true;
            this.TrackSelectedButton.Click += new System.EventHandler(this.TrackSelectedButton_Click);
            // 
            // TrackListView
            // 
            this.TrackListView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.TrackListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TrackListView.ContextMenuStrip = this.TrackContextMenuStrip;
            this.TrackListView.FullRowSelect = true;
            this.TrackListView.Location = new System.Drawing.Point(897, 3);
            this.TrackListView.Name = "TrackListView";
            this.TrackListView.Size = new System.Drawing.Size(592, 697);
            this.TrackListView.TabIndex = 2;
            this.TrackListView.UseCompatibleStateImageBehavior = false;
            this.TrackListView.View = System.Windows.Forms.View.Details;
            this.TrackListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PreventRightClickOnHeader_ListView_MouseClick);
            // 
            // TrackContextMenuStrip
            // 
            this.TrackContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.TrackContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trackToolStripMenuItem,
            this.deleteHistoryToolStripMenuItem});
            this.TrackContextMenuStrip.Name = "contextMenuStrip3";
            this.TrackContextMenuStrip.Size = new System.Drawing.Size(211, 80);
            this.TrackContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.PreventRightClick_ContextMenuStrip_Opening);
            // 
            // trackToolStripMenuItem
            // 
            this.trackToolStripMenuItem.Name = "trackToolStripMenuItem";
            this.trackToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.trackToolStripMenuItem.Text = "Mark Listened";
            this.trackToolStripMenuItem.Click += new System.EventHandler(this.MarkTrackListened_ToolStripMenuItem_Click);
            // 
            // deleteHistoryToolStripMenuItem
            // 
            this.deleteHistoryToolStripMenuItem.Name = "deleteHistoryToolStripMenuItem";
            this.deleteHistoryToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.deleteHistoryToolStripMenuItem.Text = "Remove History";
            this.deleteHistoryToolStripMenuItem.Click += new System.EventHandler(this.DeleteTrack_ToolStripMenuItem_Click);
            // 
            // AlbumListView
            // 
            this.AlbumListView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.AlbumListView.ContextMenuStrip = this.AlbumContextMenuStrip;
            this.AlbumListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AlbumListView.HideSelection = false;
            this.AlbumListView.Location = new System.Drawing.Point(301, 3);
            this.AlbumListView.Name = "AlbumListView";
            this.AlbumListView.Size = new System.Drawing.Size(590, 697);
            this.AlbumListView.TabIndex = 4;
            this.AlbumListView.UseCompatibleStateImageBehavior = false;
            this.AlbumListView.View = System.Windows.Forms.View.Details;
            this.AlbumListView.SelectedIndexChanged += new System.EventHandler(this.AlbumListView_SelectedIndexChanged);
            this.AlbumListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PreventRightClickOnHeader_ListView_MouseClick);
            // 
            // AlbumContextMenuStrip
            // 
            this.AlbumContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.AlbumContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markListenedToolStripMenuItem,
            this.deleteHistoryToolStripMenuItem1,
            this.untrackToolStripMenuItem});
            this.AlbumContextMenuStrip.Name = "contextMenuStrip2";
            this.AlbumContextMenuStrip.Size = new System.Drawing.Size(184, 76);
            this.AlbumContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.PreventRightClick_ContextMenuStrip_Opening);
            // 
            // markListenedToolStripMenuItem
            // 
            this.markListenedToolStripMenuItem.Name = "markListenedToolStripMenuItem";
            this.markListenedToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.markListenedToolStripMenuItem.Text = "Mark Listened";
            this.markListenedToolStripMenuItem.Click += new System.EventHandler(this.MarkListened_ToolStripMenuItem_Click);
            // 
            // deleteHistoryToolStripMenuItem1
            // 
            this.deleteHistoryToolStripMenuItem1.Name = "deleteHistoryToolStripMenuItem1";
            this.deleteHistoryToolStripMenuItem1.Size = new System.Drawing.Size(183, 24);
            this.deleteHistoryToolStripMenuItem1.Text = "Remove History";
            this.deleteHistoryToolStripMenuItem1.Click += new System.EventHandler(this.RemoveAlbumHistory_ToolStripMenuItem_Click);
            // 
            // untrackToolStripMenuItem
            // 
            this.untrackToolStripMenuItem.Name = "untrackToolStripMenuItem";
            this.untrackToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.untrackToolStripMenuItem.Text = "Delete Album";
            this.untrackToolStripMenuItem.Click += new System.EventHandler(this.DeleteAlbum_ToolStripMenuItem_Click);
            // 
            // ArtistListView
            // 
            this.ArtistListView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.ArtistListView.ContextMenuStrip = this.ArtistContextMenuStrip;
            this.ArtistListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArtistListView.HideSelection = false;
            this.ArtistListView.Location = new System.Drawing.Point(3, 3);
            this.ArtistListView.Name = "ArtistListView";
            this.ArtistListView.Size = new System.Drawing.Size(292, 697);
            this.ArtistListView.TabIndex = 5;
            this.ArtistListView.UseCompatibleStateImageBehavior = false;
            this.ArtistListView.View = System.Windows.Forms.View.Details;
            this.ArtistListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ArtistListView_ColumnWidthChanging);
            this.ArtistListView.SelectedIndexChanged += new System.EventHandler(this.ArtistListView_SelectedIndexChanged);
            this.ArtistListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PreventRightClickOnHeader_ListView_MouseClick);
            // 
            // ArtistContextMenuStrip
            // 
            this.ArtistContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ArtistContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.retrieveMissingAlbumsToolStripMenuItem,
            this.deleteArtistToolStripMenuItem});
            this.ArtistContextMenuStrip.Name = "contextMenuStrip1";
            this.ArtistContextMenuStrip.Size = new System.Drawing.Size(241, 52);
            this.ArtistContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.PreventRightClick_ContextMenuStrip_Opening);
            // 
            // retrieveMissingAlbumsToolStripMenuItem
            // 
            this.retrieveMissingAlbumsToolStripMenuItem.Name = "retrieveMissingAlbumsToolStripMenuItem";
            this.retrieveMissingAlbumsToolStripMenuItem.Size = new System.Drawing.Size(240, 24);
            this.retrieveMissingAlbumsToolStripMenuItem.Text = "Retrieve Missing Albums";
            this.retrieveMissingAlbumsToolStripMenuItem.Click += new System.EventHandler(this.RetrieveMissingAlbums_ToolStripMenuItem_Click);
            // 
            // deleteArtistToolStripMenuItem
            // 
            this.deleteArtistToolStripMenuItem.Name = "deleteArtistToolStripMenuItem";
            this.deleteArtistToolStripMenuItem.Size = new System.Drawing.Size(240, 24);
            this.deleteArtistToolStripMenuItem.Text = "Delete Artist";
            this.deleteArtistToolStripMenuItem.Click += new System.EventHandler(this.DeleteArtist_ToolStripMenuItem_Click);
            // 
            // DatabaseModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DatabaseModule";
            this.Size = new System.Drawing.Size(1492, 749);
            this.Load += new System.EventHandler(this.DatabaseModule_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.TrackContextMenuStrip.ResumeLayout(false);
            this.AlbumContextMenuStrip.ResumeLayout(false);
            this.ArtistContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.databaseInstanceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource databaseInstanceBindingSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView TrackListView;
        private System.Windows.Forms.Button TrackSelectedButton;
        private System.Windows.Forms.ContextMenuStrip ArtistContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteArtistToolStripMenuItem;
        private System.Windows.Forms.ListView AlbumListView;
        private System.Windows.Forms.ContextMenuStrip AlbumContextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip TrackContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem trackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markListenedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem untrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteHistoryToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem retrieveMissingAlbumsToolStripMenuItem;
        private System.Windows.Forms.ListView ArtistListView;
    }
}
