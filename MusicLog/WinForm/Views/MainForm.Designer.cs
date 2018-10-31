namespace MusicLog
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Database = new System.Windows.Forms.Button();
            this.Input = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.InputPanel = new System.Windows.Forms.Panel();
            this.DatabasePanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.InputPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.Database, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Input, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.button4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.InputPanel, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1157, 745);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // Database
            // 
            this.Database.Location = new System.Drawing.Point(3, 56);
            this.Database.Name = "Database";
            this.Database.Size = new System.Drawing.Size(193, 46);
            this.Database.TabIndex = 2;
            this.Database.Text = "Database";
            this.Database.UseVisualStyleBackColor = true;
            this.Database.Click += new System.EventHandler(this.button2_Click);
            // 
            // Input
            // 
            this.Input.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Input.Location = new System.Drawing.Point(3, 3);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(194, 47);
            this.Input.TabIndex = 3;
            this.Input.Text = "Input";
            this.Input.UseVisualStyleBackColor = true;
            this.Input.Click += new System.EventHandler(this.Input_Click);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.Location = new System.Drawing.Point(3, 162);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(194, 47);
            this.button3.TabIndex = 4;
            this.button3.Text = "Retrieve Last.FM";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button4.Location = new System.Drawing.Point(3, 215);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(194, 47);
            this.button4.TabIndex = 5;
            this.button4.Text = "Settings";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // InputPanel
            // 
            this.InputPanel.Controls.Add(this.DatabasePanel);
            this.InputPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputPanel.Location = new System.Drawing.Point(203, 3);
            this.InputPanel.Name = "InputPanel";
            this.tableLayoutPanel1.SetRowSpan(this.InputPanel, 6);
            this.InputPanel.Size = new System.Drawing.Size(951, 739);
            this.InputPanel.TabIndex = 7;
            this.InputPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.InputPanel_Paint);
            // 
            // DatabasePanel
            // 
            this.DatabasePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DatabasePanel.Location = new System.Drawing.Point(0, 0);
            this.DatabasePanel.Name = "DatabasePanel";
            this.DatabasePanel.Size = new System.Drawing.Size(951, 739);
            this.DatabasePanel.TabIndex = 8;
            this.DatabasePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DatabasePanel_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1157, 745);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Text = "MusicLog";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.InputPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button Database;
        private System.Windows.Forms.Button Input;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel InputPanel;
        private System.Windows.Forms.Panel DatabasePanel;
    }
}