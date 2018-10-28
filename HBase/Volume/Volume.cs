using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace FileHydra {
    [Serializable]
	/// <summary>
	/// <para>Volume represents a single logical storage unit - usually a whole drive, but can also be a large folder -
	/// regardless of where it physically resides.</para>
	/// <para>It's "recognized" using a number of <see cref="Recognition"/>s.</para>
	/// <para>It can be scanned and have its snapshot saved in the app's database.</para>
	/// <para><see cref="Mapping"/>s can be used to map folders on a volume to database folders.</para>
	/// </summary>
	class Volume {
		public string Label;
        public List<KnownLocation> knownLocations = new List<KnownLocation>();

		public DateTime snapshot_date { get; private set; }

		public string VolumeSerialNumber { get; set; }
        /// <summary>
		/// Total drive capacity, if known.
		/// </summary>
		public long TotalSize { get; set; }
		
		/// <summary>
		/// Last seen free size of the drive.
		/// </summary>
        public long FreeSize { get; set; }

        /// <summary>
		/// When was the volume last seen?
		/// </summary>
		public DateTime LastSeen;

		/// <summary>
		/// Is the volume currently present and accessible?
		/// </summary>
		public bool IsPresent { get; set; }

		[NonSerialized] private Drive current_drive; // be it local or network. Not saved! Expected to changes when drives are added or removed.
		[NonSerialized] private DriveInfo driveinfo; // not saved!; only valid if IsPresent.

		public List<Mapping> mappings = new List<Mapping>();

		public SnapDir root;
		
		/*		
		/// <summary>
		/// Create a Volume from a specified <paramref name="drive"/> (local or network), using its label for a name, and <paramref name="dir"/> as its root point.
		/// </summary>
		public Volume(DriveInfo drive, DirectoryInfo dir) : this(drive, dir.FullName) { }

		/// <summary>
		/// Create a Volume from a specified <paramref name="drive"/> (local or network), using its label for a name, at its root.
		/// </summary>
		public Volume(DriveInfo drive) : this(drive, "X:\\") { }  // X: gets cut off anyway

		/// <summary>
		/// Create a Volume from a specified <paramref name="drive"/> (local or network), using its label for a name, and <paramref name="rootpoint"/> - in a "X:\folder\folder" form - as its root point.
		/// </summary>
		public Volume(DriveInfo drive) {
			this.Label = drive.VolumeLabel;
			this.driveinfo = drive;
			Refresh();
		}
		*/

		/// <summary>
		/// Create a Volume in "offline" mode, for later recognition using methods provided.
		/// </summary>
		/// <param name="label"></param>
		public Volume(string label) {
            this.Label = label;
            this.knownLocations = new List<KnownLocation>();
			this.mappings = new List<Mapping>();
			root = SnapDir.CreateRoot(this);
		}

		public void AddKnownLocation(KnownLocation location) {
			this.knownLocations.Add(location);
		}

		public void AddMapping(string on_volume, string on_master) {
			mappings.Add(new Mapping(this, on_volume, on_master));
		}




		public override string ToString() {
			var where = "somewhere?";
			if (this.IsPresent && current_drive != null && this.current_recognition != null) where = "on "+this.current_recognition.ToString(current_drive);
            return "[" + this.Label + "]: (" + (this.IsPresent ? "PRESENT "+where : "missing") + ")";
        }

		private void Refresh() {
			this.TotalSize = driveinfo.TotalSize;
			this.FreeSize = driveinfo.AvailableFreeSpace;
			LastSeen = DateTime.Now;
		}

		public static void ReFind(object v) {
			if (v is Volume) (v as Volume).ReFind();
		}

		public void ReFind() {
			bool found = false;
			if (CanBeOnLocalDrive) found = FindOnDrives(Program.visibleLocalDrives);
			if (!found && CanBeOnNetworkDrive) found = FindOnDrives(Program.visibleNetworkDrives);
			IsPresent = found;
		}

		public Recognition RecognizedOnDrive(Drive drive) {
            foreach (var loc in knownLocations) {
                if (loc.recognition.IsRecognizedOn(drive)) {
                    return loc.recognition;
                }
            }
            return null;
        }

        public bool FindOnDrives(IEnumerable<Drive> drives) {
			if (drives==null) return false;
			lock (drives) {
				foreach (var drive in drives) { // may take some time!
					Console.WriteLine(drive);
					current_recognition = RecognizedOnDrive(drive);
					current_drive = drive;
					if (!(current_recognition is null)) {
						Console.WriteLine("Detected " + this.Label + " on " + drive.ToString() + " " + (drive is NetworkDrive ? "(network)" : "(local)") );
						this.current_drive = drive;
						return true;
					}
				}
			}
			Console.WriteLine("Missing " + this.Label);
            return false;
        }

		internal string GetCurrentPath() {
			if (!IsPresent) return null;
			return Program.MakeSanePath(current_drive.path + current_recognition.rootpath);
		}

		public bool CheckIfPresent() {
			if (current_drive!=null && RecognizedOnDrive(current_drive)!=null) {
				IsPresent = true;
			} else {
				Reset();
			}
			return IsPresent;
		}
        public void Reset() {
            this.IsPresent = false;
            this.current_drive = null;
        }

        /// <summary>
		/// Does any of the <see cref="Recognition"/>s allow for finding this Volume on a network share?
		/// </summary>
		public bool CanBeOnNetworkDrive {
            get {
                foreach (var loc in knownLocations) 
					if (loc.recognition.CanRecognizeNetworkShare)
                        return true;
				return false;
            }
        }
		/// <summary>
		/// Does any of the <see cref="Recognition"/>s allow for finding this Volume on a local drive?
		/// </summary>
		public bool CanBeOnLocalDrive {
            get {
				foreach (var loc in knownLocations)
					if (loc.recognition.CanRecognizeLocalDrive)
						return true;                
                return false;
            }
        }

		public Recognition current_recognition { get; private set; }

		/// <summary>
		/// Make a snapshot of the volume, if it's online. All <see cref="Mapping"/>s for the drive are scanned in sequence and snap their folders onto the Volume's snapshot stage..
		/// </summary>
		/// <param name="volume_path"></param>
		/// <returns></returns>
		internal bool DoSnapshot() {
			if (!IsPresent) throw new VolumeNotPresentException();
			foreach (var map in mappings) {
				map.DoSnapshot();
			}
			snapshot_date = DateTime.Now;
			Program.hbase.SetDirty();
			int[] counts = { 0, 0 };
			root.GetFileFolderCount(counts);
			Console.WriteLine(counts[0] + " files, " + counts[1] + " folders.");
			return true;
		}
		
		internal DirectoryInfo GetLivePath(string path) {
			if (!IsPresent) return null;
			DirectoryInfo info = new DirectoryInfo(current_drive.path + (current_drive.path.EndsWith(@"\")?"":@"\") + current_recognition.rootpath + path);
			if (info.Exists) return info;
			return null;
		}

		public static bool ParseVolumePath(string path, out string volumename, out string outpath) {
			Regex re = new Regex(@"([^:]+):\\(.*)");
			Match m = re.Match(path);
			if (m.Success) {
				volumename = m.Groups[1].Value;
				outpath = m.Groups[2].Value;
				return true;
			}
			else {
				volumename = null;
				outpath = null;
				return false;
			}
		}

		public void SaveLabel() {
			if (!IsPresent) {
				MessageBox.Show("Unable to write Hydra label to a missing volume: "+Label);
			}

			try {
				System.IO.File.WriteAllText(current_drive.path + "\\hydravol.dat", Label + "\r\n----- This drive is managed by File Hydra.\r\n");
			}
			catch (IOException e) {
			}
			catch (UnauthorizedAccessException e) {
				MessageBox.Show("Unable to write Hydra label to drive "+current_drive.path+".\r\n" + e.Message + "\r\n\r\nCheck your file permissions (or use read-only recognition).");
			}
		}

		public void Render() {
			Program.hbase.root.WipeVolume(this);
			foreach (var m in mappings) {
				m.Render();
			}
		}

	}

	[Serializable]
	internal class VolumeNotPresentException : Exception {
		public VolumeNotPresentException() {
		}

		public VolumeNotPresentException(string message) : base(message) {
		}

		public VolumeNotPresentException(string message, Exception innerException) : base(message, innerException) {
		}

		protected VolumeNotPresentException(SerializationInfo info, StreamingContext context) : base(info, context) {
		}
	}
}
