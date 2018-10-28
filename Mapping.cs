using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileHydra {
	[System.Diagnostics.DebuggerDisplay("{ToDebugString()}")]
	[Serializable]
	/// <summary>
	/// <para>Represents a single mapping from a <see cref="Volume"/> (or a folder on one) to a folder in the master structure.</para>
	/// <para>May feature exclusions or special stuff sometime.</para>
	/// </summary>
	internal class Mapping {
		[NonSerialized] public Volume volume;
		public string path_on_volume;
		public string path_on_master;

		/* mappingstatus enum fiasco
		public class MappingStatus {
			public string name;
			public string description;
			public bool canRender;
			public bool canSnapshot;
			public int error;

			/// <summary>Available on a volume now, target folder is present. Can (refresh) snapshot or render.</summary>
			public static MappingStatus LIVE = new MappingStatus(name:"Live", canRender:true, canSnapshot:true, error:0, description: "Available on a volume now, target folder is present, and we have . Can refresh snapshot or render.");
			/// <summary>Unavailable but we have a snapshot. Can render.</summary>
			public static MappingStatus OFFLINE = new MappingStatus(name:"Offline", canRender: true, canSnapshot: false, error:0);
			/// <summary>The volume is present, we have a snapshot, but target folder is missing. Can render, but it's scary!</summary>
			public static MappingStatus GONE = new MappingStatus(name:"Gone", canRender: true, canSnapshot: false, error: 2);

			/// <summary>the volume is present, target folder is present, but we have no snapshot. </summary>
			public static MappingStatus CLEAN = new MappingStatus(canRender: false, canSnapshot: true, error: 0);

			/// <summary>The volume is completely unknown.</summary>
			public static MappingStatus VOLUME_UNKNOWN = new MappingStatus(canRender: false, canSnapshot: false, error: 1);

			/// <summary>The volume is completely unknown.</summary>
			public static MappingStatus UNKNOWN_STATE = new MappingStatus(canRender: false, canSnapshot: false, error: 0);

			public MappingStatus(string name,bool canRender,bool canSnapshot,int error,string description) {
				this.name = name;
				this.description = description;
				this.canRender = canRender;
				this.canSnapshot = canSnapshot;
				this.error = error;
			}

			/// <summary>The volume is present, but target folder is missing. No can do.</summary>
			UNKNOWN, /// <descr>initial state</descr>
		}
		*/

		[NonSerialized] public string last_error;

		internal Mapping(Volume volume, string on_volume, string on_master) {
			this.volume = volume;
			this.path_on_volume = on_volume;
			this.path_on_master = on_master;
		}

		/*
		public void UpdateStatus() {
			volume_online = false;
			source_present = false;
			last_error = "";

			volume = Program.hbase.volumes.FindByName(volume_name);
			if (volume == null) {
				last_error = "Volume " + volume_name + " is unknown.";
				return;
			}

			volume_online = volume.IsPresent;

			DirectoryInfo di = null;
			try {
				di = volume.GetLivePath();
				if (di!=null) source_present = true;
			} catch (Exception e) {
				source_present = false;
			}
		}

		public bool DoSnapshot() {
			UpdateStatus();
			if (!source_present) return false;

			return volume.DoSnapshot();
		}
		*/

		//[Obsolete]
		//internal bool RenderOntoMasterDir() {
		//	UpdateStatus();
		//	if (!have_snapshot) return false;
		//	HDir hdir = Program.hbase.root.GetPath(path_on_master,true);
		//	VDir vdir = volume.GetPath(volume_path,false);
		//	hdir.MapVDir(vdir,true);
		//	return true;
		//}

		public string ToDebugString() {
			return "{Mapping} " + volume.Label + ":\\" + path_on_volume + " -> " + path_on_master;
		}

		override public string ToString() {
			return volume.Label + ":\\" + path_on_volume + " -> " + path_on_master;
		}

		public string GetLiveRoot() {
			return Program.MakeSanePath(volume.GetCurrentPath()+path_on_volume);
		}

		public SnapDir GetSnapshotRoot() {
			return volume.root.GetPath(path_on_volume, true);
		}

		public HDir GetMasterRoot() {
			return Program.hbase.root.GetPath(path_on_master, true);
		}

		/// <summary>
		/// Perform a snapshot of this mapping's folder onto its parent <see cref="Volume"/>'s snapshot stage.
		/// </summary>
		internal void DoSnapshot()
		{
			var live_root = GetLiveRoot(); // D:\ or \\SERVER\SHARE\
			GetSnapshotRoot().FillWithSnapshot(new DirectoryInfo(live_root));
		}

		internal void Render() {
			HDir hd_map = GetMasterRoot();
			SnapDir sd = GetSnapshotRoot();
			sd.RenderTo(hd_map);
		}
	}

}
