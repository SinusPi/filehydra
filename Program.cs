using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using FileHydra.NetworkEnums;
using System.Text.RegularExpressions;

namespace FileHydra {
	static class Program {
		public static bool netscan_in_progress;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			hbase = HBase.CreateExample();

			visibleNetworkDrives = new List<NetworkDrive>();
			visibleLocalDrives = new List<LocalDrive>();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}

		public static List<NetworkDrive> visibleNetworkDrives { get; set; }
		public static List<LocalDrive> visibleLocalDrives { get; set; }

		public static bool drivescan_in_progress { get; set; }

		public static HBase hbase;
		internal static string[] additional_netshares = { @"\\KOSZYK\C", @"\\KOSZYK\Sigejt Backup", @"\\KOSZYK\DUPA" };
		private static string hbase_filename;

		[Obsolete]
		public static void RebuildMasterStructure() {
			hbase.Render();
		}

		internal static void BaseSave() {
			if (hbase_filename == null) {
				BaseSaveAs();
				return;
			}
			hbase.Save(hbase_filename);
		}

		private static void BaseSaveAs() {
			SaveFileDialog sfd = new SaveFileDialog();
			if (hbase_filename != null) sfd.InitialDirectory = hbase_filename;
			DialogResult dr = sfd.ShowDialog();
			if (dr == DialogResult.OK) {
				hbase_filename = sfd.FileName;
				BaseSave();
				BaseOnSaved(true);
			}
		}

		internal static void BaseLoad(string filename = null) {
			if (filename == null) {
				OpenFileDialog ofd = new OpenFileDialog();
				DialogResult dr = ofd.ShowDialog();
				if (dr == DialogResult.OK) {
					filename = ofd.FileName;
				}
			}
			if (filename != null) {
				hbase = HBase.Load(filename);
				BaseOnLoaded(true);
			}
		}

		public static void UpdateTitle() {
			//MainForm.ActiveForm.Text = hbase_filename + (hbase.IsDirty() ? " *" : "");
		}
		internal static void BaseOnLoaded(bool success) {
			UpdateTitle();
		}
		internal static void BaseOnSaved(bool success) {
			UpdateTitle();
		}

		internal static string MakeSanePath(string v) {
			bool netshare = false;
			if (v.StartsWith(@"\\")) netshare = true;
			while (v.Contains(@"\\")) v = v.Replace(@"\\", @"\");
			if (netshare) v = @"\" + v;
			return v;
		}
	}
}



