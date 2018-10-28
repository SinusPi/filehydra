using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FileHydra {
	[Serializable]
	[System.Diagnostics.DebuggerDisplay("{ToString()}")]
	class SnapDir {
		public string name;
		public DateTime mtime;
		public List<SnapDir> dirs;
		public List<SnapFile> files;

		public SnapDir parent;
		[NonSerialized]	public string fullpath;
		public Volume volume;

		private SnapDir(string name, Volume volume, SnapDir parent) {
			this.name = name;
			this.volume = volume;
			this.parent = parent;
			if (parent != null)
				this.fullpath = parent.fullpath + (parent.name == @"\" ? "" : @"\") + this.name;
			else
				this.fullpath = @"\";
			dirs = new List<SnapDir>();
			files = new List<SnapFile>();
		}
		//private VDir(DirectoryInfo dir,Volume v)
		//	: this(dir.Name,v) {
		//}

		/// <summary>
		/// Create a root point for the volume's snapshot.
		/// </summary>
		/// <param name="v">Parent volume</param>
		/// <returns></returns>
		public static SnapDir CreateRoot(Volume v) {
			return new SnapDir(@"\", v, null);
		}
		public SnapDir AddSubdir(string name) {
			SnapDir vd = new SnapDir(name, this.volume, this);
			dirs.Add(vd);
			return vd;
		}
		public SnapDir AddSubdirWithSnapshot(DirectoryInfo di) {
			var subdir = GetSubdir(di.Name, true);
			subdir.FillWithSnapshot(di);
			return subdir;
		}

		/// <exception cref="SnapDirNotFoundException">Thrown when folder is not found.</exception>
		internal SnapDir GetPath(string path, bool allow_creation) {
			// trim trailing backslashes
			while (path.EndsWith(@"\")) path = path.Substring(0, path.Length - 1);
			if (path.StartsWith(@"\")) {
				if (this.name != @"\")
					throw new SnapDirNotFoundException("Searching for " + path + " on " + this.fullpath + " which is not root!");
				else
					path = path.Substring(1);
			}
			string[] path_tokens = path.Split(@"\".ToCharArray(), 2);
			string first_token = path_tokens[0];
			SnapDir dir = this.GetSubdir(first_token, allow_creation);
			if (dir == null)
				throw new SnapDirNotFoundException(first_token, this.fullpath, path);
			if (path_tokens.Length > 1) {
				string remainder = path_tokens[1];
				return dir.GetPath(remainder, allow_creation);
			}
			else {
				return dir;
			}
		}

		internal SnapDir GetSubdir(string name, bool allow_creation) {
			foreach (SnapDir sdir in dirs)
				if (sdir.name == name)
					return sdir;

			if (!allow_creation)
				return null;

			// let's make an hdir
			return this.AddSubdir(name);
		}

		internal void AddFile(FileInfo filesi) {
			var snapfile = new SnapFile(filesi);
			snapfile.volume = volume;
			files.Add(snapfile);
		}

		internal void AddFile(string name) {
			var snapfile = new SnapFile(name);
			snapfile.volume = volume;
			files.Add(snapfile);
		}

		override public string ToString() {
			return "{VDir} [" + volume.Label +"]:"+fullpath;
		}

		internal bool FillWithSnapshot(DirectoryInfo dir) {
			mtime = dir.LastWriteTime;
			try {
				foreach (DirectoryInfo di in dir.EnumerateDirectories()) {
					try {
						if (di.Attributes.HasFlag(FileAttributes.ReparsePoint) && Utils.JunctionPoint.Exists(di.FullName)) {
							Console.WriteLine("Snapshot: d " + di.FullName + " is a junction.");
							continue;
						}
						Console.WriteLine("Snapshot: d " + di.FullName);
						SnapDir sub = AddSubdirWithSnapshot(di);
					} catch (System.IO.IOException e) {
						Console.WriteLine("ERROR snapshotting " + di.FullName);
					}
				}
				files.Clear();
				foreach (FileInfo fi in dir.EnumerateFiles()) {
					Console.WriteLine("Snapshot: f " + fi.FullName);
					AddFile(fi);
				}
			}
			catch (System.UnauthorizedAccessException) {
				Console.WriteLine("Snapshot: d " + dir.FullName + " got UnauthorizedAccess");
			}
			catch (System.IO.DirectoryNotFoundException) {
				Console.WriteLine("Snapshot: d " + dir.FullName + " got DirectoryNotFound");
			}
			catch (System.IO.FileNotFoundException) {
				Console.WriteLine("Snapshot: d " + dir.FullName + " got FileNotFound");
			}
			return true;
		}

		public bool GetFileFolderCount(int[] counts) {
			if (counts.Length < 2) return false;
			counts[0] += files.Count;
			counts[1] += dirs.Count;
			foreach (SnapDir sd in dirs)
				sd.GetFileFolderCount(counts);
			return true;
		}

		internal void RenderTo(HDir hd) {
			foreach (SnapFile sf in files) {
				HFile hf = hd.GetFile(sf.name, true);
				hf.AddInstance(sf);
			}
			foreach (SnapDir subsd in dirs) {
				HDir subhd = hd.GetPath(subsd.name, true);
				subhd.AddScanned(subsd);
				subsd.RenderTo(subhd);
			}
			if (files.Count + dirs.Count > 0)
				hd.AddScanned(this);
		}
	}
}
