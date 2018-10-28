namespace FileHydra
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.masterdir_treeView1 = new System.Windows.Forms.TreeView();
			this.masterdir_listView1 = new System.Windows.Forms.ListView();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.backgroundWorker1_scanfiles = new System.ComponentModel.BackgroundWorker();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.saveDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.listBox_Localdrives = new System.Windows.Forms.ListBox();
			this.contextMenuStrip_drive = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.labelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
			this.unlabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.backgroundWorker2_netscanner = new System.ComponentModel.BackgroundWorker();
			this.backgroundWorker2_drivescanner = new System.ComponentModel.BackgroundWorker();
			this.button_Localdrives_update = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button_Netdrives_update = new System.Windows.Forms.Button();
			this.listBox_Netdrives = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.button_Volumes_update = new System.Windows.Forms.Button();
			this.listBox_Volumes = new System.Windows.Forms.ListBox();
			this.contextMenuStripVolume = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem_Scan = new System.Windows.Forms.ToolStripMenuItem();
			this.backgroundWorker_volumechecker = new System.ComponentModel.BackgroundWorker();
			this.timer_volumedisplay = new System.Windows.Forms.Timer(this.components);
			this.timer_volumerecheck = new System.Windows.Forms.Timer(this.components);
			this.timer_ui_fx = new System.Windows.Forms.Timer(this.components);
			this.button_refreshMasterDir = new System.Windows.Forms.Button();
			this.toolStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.contextMenuStrip_drive.SuspendLayout();
			this.contextMenuStripVolume.SuspendLayout();
			this.SuspendLayout();
			// 
			// masterdir_treeView1
			// 
			this.masterdir_treeView1.Location = new System.Drawing.Point(13, 40);
			this.masterdir_treeView1.Name = "masterdir_treeView1";
			this.masterdir_treeView1.Size = new System.Drawing.Size(121, 210);
			this.masterdir_treeView1.TabIndex = 0;
			this.masterdir_treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.masterdir_treeView1_AfterSelect);
			// 
			// masterdir_listView1
			// 
			this.masterdir_listView1.Location = new System.Drawing.Point(140, 40);
			this.masterdir_listView1.Name = "masterdir_listView1";
			this.masterdir_listView1.Size = new System.Drawing.Size(360, 209);
			this.masterdir_listView1.TabIndex = 1;
			this.masterdir_listView1.UseCompatibleStateImageBehavior = false;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(506, 266);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(290, 44);
			this.button1.TabIndex = 2;
			this.button1.Text = "scan files";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(506, 191);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(290, 58);
			this.textBox1.TabIndex = 3;
			// 
			// backgroundWorker1_scanfiles
			// 
			this.backgroundWorker1_scanfiles.WorkerReportsProgress = true;
			this.backgroundWorker1_scanfiles.WorkerSupportsCancellation = true;
			this.backgroundWorker1_scanfiles.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_scanfiles_DoWork);
			this.backgroundWorker1_scanfiles.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripButton1,
            this.toolStripSeparator1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 311);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1059, 25);
			this.toolStrip1.TabIndex = 4;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(42, 22);
			this.toolStripLabel1.Text = "Drives:";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(48, 22);
			this.toolStripButton1.Text = "Rescan";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1059, 24);
			this.menuStrip1.TabIndex = 6;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveDatabaseToolStripMenuItem,
            this.loadDatabaseToolStripMenuItem});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
			this.toolStripMenuItem1.Text = "&File";
			// 
			// saveDatabaseToolStripMenuItem
			// 
			this.saveDatabaseToolStripMenuItem.Name = "saveDatabaseToolStripMenuItem";
			this.saveDatabaseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveDatabaseToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.saveDatabaseToolStripMenuItem.Text = "&Save database";
			this.saveDatabaseToolStripMenuItem.Click += new System.EventHandler(this.saveDatabaseToolStripMenuItem_Click);
			// 
			// loadDatabaseToolStripMenuItem
			// 
			this.loadDatabaseToolStripMenuItem.Name = "loadDatabaseToolStripMenuItem";
			this.loadDatabaseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
			this.loadDatabaseToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.loadDatabaseToolStripMenuItem.Text = "&Load database";
			this.loadDatabaseToolStripMenuItem.Click += new System.EventHandler(this.loadDatabaseToolStripMenuItem_Click);
			// 
			// listBox_Localdrives
			// 
			this.listBox_Localdrives.ContextMenuStrip = this.contextMenuStrip_drive;
			this.listBox_Localdrives.FormattingEnabled = true;
			this.listBox_Localdrives.Location = new System.Drawing.Point(506, 66);
			this.listBox_Localdrives.Name = "listBox_Localdrives";
			this.listBox_Localdrives.Size = new System.Drawing.Size(154, 69);
			this.listBox_Localdrives.TabIndex = 7;
			// 
			// contextMenuStrip_drive
			// 
			this.contextMenuStrip_drive.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelToolStripMenuItem,
            this.unlabelToolStripMenuItem});
			this.contextMenuStrip_drive.Name = "contextMenuStrip_drive";
			this.contextMenuStrip_drive.ShowImageMargin = false;
			this.contextMenuStrip_drive.Size = new System.Drawing.Size(90, 48);
			// 
			// labelToolStripMenuItem
			// 
			this.labelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1});
			this.labelToolStripMenuItem.Name = "labelToolStripMenuItem";
			this.labelToolStripMenuItem.Size = new System.Drawing.Size(89, 22);
			this.labelToolStripMenuItem.Text = "&Label";
			this.labelToolStripMenuItem.DropDownOpening += new System.EventHandler(this.labelToolStripMenuItem_DropDownOpening);
			// 
			// toolStripTextBox1
			// 
			this.toolStripTextBox1.Name = "toolStripTextBox1";
			this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
			this.toolStripTextBox1.Text = "blaa";
			this.toolStripTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBox1_KeyPress);
			// 
			// unlabelToolStripMenuItem
			// 
			this.unlabelToolStripMenuItem.Name = "unlabelToolStripMenuItem";
			this.unlabelToolStripMenuItem.Size = new System.Drawing.Size(89, 22);
			this.unlabelToolStripMenuItem.Text = "&Unlabel";
			this.unlabelToolStripMenuItem.Click += new System.EventHandler(this.unlabelToolStripMenuItem_Click);
			// 
			// backgroundWorker2_netscanner
			// 
			this.backgroundWorker2_netscanner.WorkerReportsProgress = true;
			this.backgroundWorker2_netscanner.WorkerSupportsCancellation = true;
			this.backgroundWorker2_netscanner.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_netscanner_DoWork);
			// 
			// backgroundWorker2_drivescanner
			// 
			this.backgroundWorker2_drivescanner.WorkerReportsProgress = true;
			this.backgroundWorker2_drivescanner.WorkerSupportsCancellation = true;
			this.backgroundWorker2_drivescanner.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_drivescanner_DoWork);
			// 
			// button_Localdrives_update
			// 
			this.button_Localdrives_update.Location = new System.Drawing.Point(506, 142);
			this.button_Localdrives_update.Name = "button_Localdrives_update";
			this.button_Localdrives_update.Size = new System.Drawing.Size(75, 23);
			this.button_Localdrives_update.TabIndex = 8;
			this.button_Localdrives_update.Text = "Update";
			this.button_Localdrives_update.UseVisualStyleBackColor = true;
			this.button_Localdrives_update.Click += new System.EventHandler(this.button_Localdrives_update_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(507, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Local drives:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(684, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Network drives:";
			// 
			// button_Netdrives_update
			// 
			this.button_Netdrives_update.Location = new System.Drawing.Point(683, 142);
			this.button_Netdrives_update.Name = "button_Netdrives_update";
			this.button_Netdrives_update.Size = new System.Drawing.Size(75, 23);
			this.button_Netdrives_update.TabIndex = 11;
			this.button_Netdrives_update.Text = "Update";
			this.button_Netdrives_update.UseVisualStyleBackColor = true;
			this.button_Netdrives_update.Click += new System.EventHandler(this.button_Netdrives_update_Click);
			// 
			// listBox_Netdrives
			// 
			this.listBox_Netdrives.ContextMenuStrip = this.contextMenuStrip_drive;
			this.listBox_Netdrives.FormattingEnabled = true;
			this.listBox_Netdrives.Location = new System.Drawing.Point(683, 66);
			this.listBox_Netdrives.Name = "listBox_Netdrives";
			this.listBox_Netdrives.Size = new System.Drawing.Size(154, 69);
			this.listBox_Netdrives.TabIndex = 10;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(854, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(50, 13);
			this.label3.TabIndex = 16;
			this.label3.Text = "Volumes:";
			// 
			// button_Volumes_update
			// 
			this.button_Volumes_update.Location = new System.Drawing.Point(853, 142);
			this.button_Volumes_update.Name = "button_Volumes_update";
			this.button_Volumes_update.Size = new System.Drawing.Size(75, 23);
			this.button_Volumes_update.TabIndex = 15;
			this.button_Volumes_update.Text = "Update";
			this.button_Volumes_update.UseVisualStyleBackColor = true;
			this.button_Volumes_update.Click += new System.EventHandler(this.button_Volumes_update_Click);
			// 
			// listBox_Volumes
			// 
			this.listBox_Volumes.ContextMenuStrip = this.contextMenuStripVolume;
			this.listBox_Volumes.FormattingEnabled = true;
			this.listBox_Volumes.Location = new System.Drawing.Point(853, 66);
			this.listBox_Volumes.Name = "listBox_Volumes";
			this.listBox_Volumes.Size = new System.Drawing.Size(154, 69);
			this.listBox_Volumes.TabIndex = 14;
			// 
			// contextMenuStripVolume
			// 
			this.contextMenuStripVolume.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Scan});
			this.contextMenuStripVolume.Name = "contextMenuStrip_drive";
			this.contextMenuStripVolume.ShowImageMargin = false;
			this.contextMenuStripVolume.Size = new System.Drawing.Size(75, 26);
			// 
			// toolStripMenuItem_Scan
			// 
			this.toolStripMenuItem_Scan.Name = "toolStripMenuItem_Scan";
			this.toolStripMenuItem_Scan.Size = new System.Drawing.Size(74, 22);
			this.toolStripMenuItem_Scan.Text = "&Scan";
			this.toolStripMenuItem_Scan.Click += new System.EventHandler(this.toolStripMenuItem_Scan_Click);
			// 
			// timer_volumedisplay
			// 
			this.timer_volumedisplay.Enabled = true;
			this.timer_volumedisplay.Tick += new System.EventHandler(this.timer_volumedisplay_Tick);
			// 
			// timer_volumerecheck
			// 
			this.timer_volumerecheck.Enabled = true;
			this.timer_volumerecheck.Interval = 10000;
			this.timer_volumerecheck.Tick += new System.EventHandler(this.timer_volumerecheck_Tick);
			// 
			// timer_ui_fx
			// 
			this.timer_ui_fx.Enabled = true;
			this.timer_ui_fx.Tick += new System.EventHandler(this.timer_ui_fx_Tick);
			// 
			// button_refreshMasterDir
			// 
			this.button_refreshMasterDir.Location = new System.Drawing.Point(13, 256);
			this.button_refreshMasterDir.Name = "button_refreshMasterDir";
			this.button_refreshMasterDir.Size = new System.Drawing.Size(121, 23);
			this.button_refreshMasterDir.TabIndex = 17;
			this.button_refreshMasterDir.Text = "Update";
			this.button_refreshMasterDir.UseVisualStyleBackColor = true;
			this.button_refreshMasterDir.Click += new System.EventHandler(this.button_refreshMasterDir_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1059, 336);
			this.Controls.Add(this.button_refreshMasterDir);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.button_Volumes_update);
			this.Controls.Add(this.listBox_Volumes);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button_Netdrives_update);
			this.Controls.Add(this.listBox_Netdrives);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button_Localdrives_update);
			this.Controls.Add(this.listBox_Localdrives);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.masterdir_listView1);
			this.Controls.Add(this.masterdir_treeView1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.contextMenuStrip_drive.ResumeLayout(false);
			this.contextMenuStripVolume.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView masterdir_treeView1;
        private System.Windows.Forms.ListView masterdir_listView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1_scanfiles;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDatabaseToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox_Localdrives;
        private System.ComponentModel.BackgroundWorker backgroundWorker2_netscanner;
        private System.ComponentModel.BackgroundWorker backgroundWorker2_drivescanner;
        private System.Windows.Forms.Button button_Localdrives_update;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_Netdrives_update;
        private System.Windows.Forms.ListBox listBox_Netdrives;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_drive;
        private System.Windows.Forms.ToolStripMenuItem labelToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem unlabelToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_Volumes_update;
        private System.Windows.Forms.ListBox listBox_Volumes;
        private System.ComponentModel.BackgroundWorker backgroundWorker_volumechecker;
        private System.Windows.Forms.Timer timer_volumedisplay;
        private System.Windows.Forms.Timer timer_volumerecheck;
		private System.Windows.Forms.Timer timer_ui_fx;
		private System.Windows.Forms.Button button_refreshMasterDir;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripVolume;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Scan;
	}
}

