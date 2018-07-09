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
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.trackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView2 = new System.Windows.Forms.ListView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.markListenedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteHistoryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.untrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView3 = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.retrieveMissingAlbumsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteArtistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseInstanceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.listView1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.listView2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.listView3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1492, 749);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(301, 706);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(590, 40);
            this.button1.TabIndex = 3;
            this.button1.Text = "Track Selected ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.ContextMenuStrip = this.contextMenuStrip3;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(897, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(592, 697);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView3_MouseClick);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trackToolStripMenuItem,
            this.deleteHistoryToolStripMenuItem});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(184, 52);
            this.contextMenuStrip3.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // trackToolStripMenuItem
            // 
            this.trackToolStripMenuItem.Name = "trackToolStripMenuItem";
            this.trackToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.trackToolStripMenuItem.Text = "Mark Listened";
            this.trackToolStripMenuItem.Click += new System.EventHandler(this.trackToolStripMenuItem_Click);
            // 
            // deleteHistoryToolStripMenuItem
            // 
            this.deleteHistoryToolStripMenuItem.Name = "deleteHistoryToolStripMenuItem";
            this.deleteHistoryToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.deleteHistoryToolStripMenuItem.Text = "Remove History";
            this.deleteHistoryToolStripMenuItem.Click += new System.EventHandler(this.deleteHistoryToolStripMenuItem_Click);
            // 
            // listView2
            // 
            this.listView2.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listView2.ContextMenuStrip = this.contextMenuStrip2;
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(301, 3);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(590, 697);
            this.listView2.TabIndex = 4;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
            this.listView2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView3_MouseClick);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markListenedToolStripMenuItem,
            this.deleteHistoryToolStripMenuItem1,
            this.untrackToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(184, 76);
            this.contextMenuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // markListenedToolStripMenuItem
            // 
            this.markListenedToolStripMenuItem.Name = "markListenedToolStripMenuItem";
            this.markListenedToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.markListenedToolStripMenuItem.Text = "Mark Listened";
            this.markListenedToolStripMenuItem.Click += new System.EventHandler(this.markListenedToolStripMenuItem_Click);
            // 
            // deleteHistoryToolStripMenuItem1
            // 
            this.deleteHistoryToolStripMenuItem1.Name = "deleteHistoryToolStripMenuItem1";
            this.deleteHistoryToolStripMenuItem1.Size = new System.Drawing.Size(230, 24);
            this.deleteHistoryToolStripMenuItem1.Text = "Remove History";
            this.deleteHistoryToolStripMenuItem1.Click += new System.EventHandler(this.deleteHistoryToolStripMenuItem1_Click);
            // 
            // untrackToolStripMenuItem
            // 
            this.untrackToolStripMenuItem.Name = "untrackToolStripMenuItem";
            this.untrackToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.untrackToolStripMenuItem.Text = "Delete Album";
            this.untrackToolStripMenuItem.Click += new System.EventHandler(this.untrackToolStripMenuItem_Click);
            // 
            // listView3
            // 
            this.listView3.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listView3.ContextMenuStrip = this.contextMenuStrip1;
            this.listView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView3.HideSelection = false;
            this.listView3.Location = new System.Drawing.Point(3, 3);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(292, 697);
            this.listView3.TabIndex = 5;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            this.listView3.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listView3_ColumnWidthChanging);
            this.listView3.SelectedIndexChanged += new System.EventHandler(this.listView3_SelectedIndexChanged);
            this.listView3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView3_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.retrieveMissingAlbumsToolStripMenuItem,
            this.deleteArtistToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(241, 80);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // retrieveMissingAlbumsToolStripMenuItem
            // 
            this.retrieveMissingAlbumsToolStripMenuItem.Name = "retrieveMissingAlbumsToolStripMenuItem";
            this.retrieveMissingAlbumsToolStripMenuItem.Size = new System.Drawing.Size(240, 24);
            this.retrieveMissingAlbumsToolStripMenuItem.Text = "Retrieve Missing Albums";
            this.retrieveMissingAlbumsToolStripMenuItem.Click += new System.EventHandler(this.retrieveMissingAlbumsToolStripMenuItem_Click);
            // 
            // deleteArtistToolStripMenuItem
            // 
            this.deleteArtistToolStripMenuItem.Name = "deleteArtistToolStripMenuItem";
            this.deleteArtistToolStripMenuItem.Size = new System.Drawing.Size(240, 24);
            this.deleteArtistToolStripMenuItem.Text = "Delete Artist";
            this.deleteArtistToolStripMenuItem.Click += new System.EventHandler(this.deleteArtistToolStripMenuItem_Click);
            // 
            // databaseInstanceBindingSource
            // 
            this.databaseInstanceBindingSource.DataSource = typeof(MusicLog.Database.DatabaseInstance);
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
            this.contextMenuStrip3.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.databaseInstanceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource databaseInstanceBindingSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteArtistToolStripMenuItem;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem trackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markListenedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem untrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteHistoryToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem retrieveMissingAlbumsToolStripMenuItem;
        private System.Windows.Forms.ListView listView3;
    }
}
