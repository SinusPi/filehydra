using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Security;
using FileHydra.NetworkEnums;
using ServerEnumerator;

namespace FileHydra
{
    public partial class MainForm : Form
    {
        string status = "";

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            backgroundWorker1_scanfiles.RunWorkerAsync();
        }

        private void backgroundWorker1_scanfiles_DoWork(object sender, DoWorkEventArgs e)
        {
			/*
            DriveInfo[] drives = System.IO.DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                ScanDrive(drive);
            }
			*/
        }

        private void ScanDrive(DriveInfo drive)
        {
            /*
            List<Volume> knownd = KnownVolumes.FindFromDrive(drive);
            if (knownd.Count==0) return; // only scan known drives.
            DirectoryInfo rootdir = drive.RootDirectory;
            knownd.rootdir = new Dir("$");
            ScanDirectory(rootdir,knownd.rootdir);
             */ 
        }

        private void ScanDirectory(DirectoryInfo startdir,SnapDir knowndir)
        {
            if (startdir.Attributes.HasFlag(System.IO.FileAttributes.ReparsePoint)) return;

            status = startdir.Name;
            foreach (var dir in startdir.EnumerateDirectories())
            {
                try
                {
                    //var new_knowndir = new VDir(dir);
                    //knowndir.dirs.Add(new_knowndir);
                    //ScanDirectory(dir,new_knowndir);
                }
                catch (SecurityException e) { }
                catch (UnauthorizedAccessException e) { }
                try
                {
                    ScanFiles(dir,knowndir);
                }
                catch (SecurityException e) { }
                catch (UnauthorizedAccessException e) { }
            }
        }

        private void ScanFiles(DirectoryInfo startdir,SnapDir knowndir)
        {
            if (startdir.Attributes.HasFlag(System.IO.FileAttributes.ReparsePoint)) return;
            foreach (var filesi in startdir.EnumerateFiles())
            {
                knowndir.AddFile(filesi);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.textBox1.Text = status;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void StartRescanLocalDrives()
        {
			if (!backgroundWorker2_drivescanner.IsBusy) {
				backgroundWorker2_drivescanner.RunWorkerAsync();
				backgroundWorker2_drivescanner.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker2_drivescanner_RunWorkerCompleted);
			} else {
				MessageBox.Show("Rescanning locals already.");
			}
        }

        void drive_Click(object sender, EventArgs e)
        {
            var drive = ((ToolStripButton)sender).Tag as DriveInfo;
            if (drive==null) return;
			FolderBrowserDialog dialog = new FolderBrowserDialog
			{
				SelectedPath = drive.RootDirectory.FullName,
				ShowNewFolderButton = false
			};
			dialog.ShowDialog();
			//Program.hbase.volumes.Add(new Volume(drive.VolumeLabel, drive new RecognitionVolumeLabel dialog.SelectedPath));
			MessageBox.Show("TODO: guess recognition.");
            
            StartRescanLocalDrives();
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_DEVICECHANGE=0x219;
            switch (m.Msg)
            {
                case WM_DEVICECHANGE:
                    // The WParam value identifies what is occurring.
                    // n = (int)m.WParam;
                    StartRescanLocalDrives();
                    break;
            }
            base.WndProc(ref m);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            StartRescanLocalDrives();
        }

        public void StartRescanNetworkDrives() {
            listBox_Netdrives.Items.Clear();
            listBox_Netdrives.Items.Add("Scanning...");
            backgroundWorker2_netscanner.RunWorkerAsync();
            backgroundWorker2_netscanner.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker2_netscanner_RunWorkerCompleted);
        }

        private void loadDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Program.BaseLoad();
        }

        private void saveDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Program.BaseSave();
		}

		private void MainForm_Load(object sender, EventArgs e)
        {
            StartRescanLocalDrives();
            StartRescanNetworkDrives();
        }

        private void backgroundWorker2_netscanner_DoWork(object sender, DoWorkEventArgs ev)
        {
            Program.netscan_in_progress = true;

			/*
				var nb = new NetworkBrowser();
				var doms = nb.GetNetworkDomains();
				//doms.Add("WORKGROUP");
				foreach (var domain in doms)
				{
					var comps = nb.GetNetworkComputers(domain);
					foreach (var computer in comps)
					{
						var sc = new ShareCollection();
						ShareCollection shares = ShareCollection.GetShares("\\\\" + computer); // no domain used here. computers need to be unique!?!?
						foreach (Share share in shares) if (share.IsFileSystem && share.ShareType == ShareType.Disk)
							{
								var drive = new NetworkDrive(share);
								Program.visibleNetworkDrives.Add(drive); //new Drive "\\\\" + computer + "\\" + share.NetName + " (" + share.Remark + ")"
								backgroundWorker2_netscanner.ReportProgress(50);
							}
					}
				}
			*/

			// approach #2: ServerEnum
			ServerEnum shares = new ServerEnum(ResourceScope.RESOURCE_GLOBALNET,
												ResourceType.RESOURCETYPE_DISK,
												ResourceUsage.RESOURCEUSAGE_ALL,
												ResourceDisplayType.RESOURCEDISPLAYTYPE_SHARE);
			List<Share> foundshares = new List<Share>();
			foreach (string servershare in Program.additional_netshares) {
				string[] split = servershare.Split(new char[1] { '\\' });
				try {
					Share share = ShareCollection.GetNetShareInfo(split[2], split[3]);
					add_unique_share(foundshares, share);
				}
				catch (Exception e) {
				}
			}

			foreach (string servershare in shares) {
				string[] split = servershare.Split(new char[1] { '\\' });
				try {
					Share share = ShareCollection.GetNetShareInfo(split[2], split[3]);
					add_unique_share(foundshares, share);
				}
				catch (Exception e) {
				}
			}

			lock (Program.visibleNetworkDrives) {
				Program.visibleNetworkDrives.Clear();

				foreach (Share share in foundshares) {
					var drive = new NetworkDrive(share);
					Program.visibleNetworkDrives.Add(drive); //new Drive "\\\\" + computer + "\\" + share.NetName + " (" + share.Remark + ")"
				}

				Program.netscan_in_progress = false;
			}
        }

		private void add_unique_share(List<Share> shares, Share share) {
			foreach (Share sh in shares) {
				if (sh.NetName == share.NetName) return;
			}
			shares.Add(share);
		}

        private void backgroundWorker2_netscanner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            listBox_Netdrives.Items.Clear();
            foreach (var drive in Program.visibleNetworkDrives) {
                listBox_Netdrives.Items.Add(drive);
            }
            StartRecheckVolumes();
        }

        private void backgroundWorker2_drivescanner_DoWork(object sender, DoWorkEventArgs e) {
            Program.drivescan_in_progress = true;
			lock (Program.visibleLocalDrives) {
				Program.visibleLocalDrives.Clear();

				foreach (var drive in System.IO.DriveInfo.GetDrives().Where(drive => drive.IsReady)) {
					Program.visibleLocalDrives.Add(new LocalDrive(drive));
				}
			}
            Program.drivescan_in_progress = false;
        }
        private void backgroundWorker2_drivescanner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            listBox_Localdrives.Items.Clear();
            foreach (var drive in Program.visibleLocalDrives) {
                listBox_Localdrives.Items.Add(drive);
            }
            StartRecheckVolumes();
        }


        private void button_Localdrives_update_Click(object sender, EventArgs e) {
            StartRescanLocalDrives();
        }

        private void button_Netdrives_update_Click(object sender, EventArgs e) {
            StartRescanNetworkDrives();
        }

        private void unlabelToolStripMenuItem_Click(object sender, EventArgs e) {
            var listbox = contextMenuStrip_drive.SourceControl as ListBox;
            var drive = listbox != null ? listbox.SelectedItem as Drive : null;
            if (drive == null) return;
            //drive.label = "";
        }

        private void labelToolStripMenuItem_DropDownOpening(object sender, EventArgs e) {
            var listbox = contextMenuStrip_drive.SourceControl as ListBox;
            var drive = listbox!=null ? listbox.SelectedItem as Drive : null;
            if (drive==null) return;
            //toolStripTextBox1.Text = drive.label;
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == 13) {
                var listbox = contextMenuStrip_drive.SourceControl as ListBox;
                var drive = listbox != null ? listbox.SelectedItem as Drive : null;
                if (drive == null) return;
                //drive.label = toolStripTextBox1.Text;
            }
        }

        private void button_Volumes_update_Click(object sender, EventArgs e) {
            Volumes.is_dirty = true;
            StartRecheckVolumes();
        }

        private void StartRecheckVolumes() {
            foreach (var volume in Program.hbase.volumes)
                ThreadPool.QueueUserWorkItem(new WaitCallback(Volume.ReFind), volume);
        }

        private void timer_volumedisplay_Tick(object sender, EventArgs e) {
            if (Volumes.is_dirty) {
                Volumes.is_dirty = false;
                RefreshVolumesList();
            }
        }

        private void RefreshVolumesList() {
            listBox_Volumes.Items.Clear();
            foreach (var vol in Program.hbase.volumes) {
                listBox_Volumes.Items.Add(vol);
            }
        }

        private void timer_volumerecheck_Tick(object sender, EventArgs e) {
            Volumes.is_dirty = true;
            StartRecheckVolumes();
        }

		private void timer_ui_fx_Tick(object sender, EventArgs e) {
			if (backgroundWorker2_netscanner.IsBusy) {
				listBox_Netdrives.Items.Clear();
				if (System.DateTime.Now.Millisecond < 500) listBox_Netdrives.Items.Add("Scanning...");
			}
		}

		private void button_refreshMasterDir_Click(object sender, EventArgs e) {
			Program.hbase.Render();
			HDir old_selected_dir = masterdir_treeView1.SelectedNode?.Tag as HDir;
			TreeNode old_selected_node = null;

			this.SuspendLayout();
			masterdir_treeView1.Nodes.Clear();

			void fill (TreeNode node,HDir dir) {
				node.Tag = dir;
				foreach (HDir d in dir.dirs) {
					TreeNode n = new TreeNode(d.name);
					n.Tag = d;
					if (d == old_selected_dir) old_selected_node = n;
					node.Nodes.Add(n);
					fill(n,d);
				}
			}
			TreeNode rootnode = new TreeNode(@"\");
			masterdir_treeView1.Nodes.Add(rootnode);
			fill(rootnode, Program.hbase.root);
			masterdir_treeView1.SelectedNode = (old_selected_node is null) ? rootnode : old_selected_node;
			UpdateMasterFileBox((old_selected_dir is null) ? Program.hbase.root : old_selected_dir);
			this.ResumeLayout();
		}

		private void masterdir_treeView1_AfterSelect(object sender, TreeViewEventArgs e) {
			this.UpdateMasterFileBox(e.Node.Tag as HDir);
		}

		private void UpdateMasterFileBox(HDir dir) {
			this.SuspendLayout();
			masterdir_listView1.Items.Clear();
			foreach (HFile f in dir.files) {
				masterdir_listView1.Items.Add(f.name).Tag = f;
			}
			this.ResumeLayout();
		}

		private void toolStripMenuItem_Scan_Click(object sender, EventArgs e) {
			var ts = sender as ToolStripMenuItem;
			var cms = ts?.GetCurrentParent() as ContextMenuStrip;

			var lb = cms?.SourceControl as ListBox;

			var v = lb?.SelectedItem as Volume;

			v?.DoSnapshot();

			//
		}

	}
}
