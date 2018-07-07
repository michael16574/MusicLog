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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.untrackAllAlbumsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteArtistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView2 = new System.Windows.Forms.ListView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.trackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseInstanceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.databaseInstanceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.Window;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(292, 697);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.listBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.listView1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.listView2, 1, 0);
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
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.untrackAllAlbumsToolStripMenuItem,
            this.deleteArtistToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(205, 52);
            // 
            // untrackAllAlbumsToolStripMenuItem
            // 
            this.untrackAllAlbumsToolStripMenuItem.Name = "untrackAllAlbumsToolStripMenuItem";
            this.untrackAllAlbumsToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.untrackAllAlbumsToolStripMenuItem.Text = "Untrack All Albums";
            // 
            // deleteArtistToolStripMenuItem
            // 
            this.deleteArtistToolStripMenuItem.Name = "deleteArtistToolStripMenuItem";
            this.deleteArtistToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.deleteArtistToolStripMenuItem.Text = "Delete Artist";
            // 
            // listView2
            // 
            this.listView2.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(301, 3);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(590, 697);
            this.listView2.TabIndex = 4;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trackToolStripMenuItem,
            this.deleteHistoryToolStripMenuItem});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(174, 52);
            this.contextMenuStrip3.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip3_Opening);
            // 
            // trackToolStripMenuItem
            // 
            this.trackToolStripMenuItem.Name = "trackToolStripMenuItem";
            this.trackToolStripMenuItem.Size = new System.Drawing.Size(173, 24);
            this.trackToolStripMenuItem.Text = "Mark Listened";
            this.trackToolStripMenuItem.Click += new System.EventHandler(this.trackToolStripMenuItem_Click);
            // 
            // deleteHistoryToolStripMenuItem
            // 
            this.deleteHistoryToolStripMenuItem.Name = "deleteHistoryToolStripMenuItem";
            this.deleteHistoryToolStripMenuItem.Size = new System.Drawing.Size(173, 24);
            this.deleteHistoryToolStripMenuItem.Text = "Delete History";
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
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.databaseInstanceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource databaseInstanceBindingSource;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem untrackAllAlbumsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteArtistToolStripMenuItem;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem trackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteHistoryToolStripMenuItem;
    }
}
