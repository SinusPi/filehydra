using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileHydra {
	[Serializable]
	[System.Diagnostics.DebuggerDisplay("{ToString()}")]
	class HDir {
		public string name;
		public string fullpath;
		public List<HDir> dirs; // filled from vdirs or created empty
		public List<HFile> files; // filled from vdirs/vfiles
		[NonSerialized] public List<Volume> on_volumes = new List<Volume>();

		internal HFile GetFile(string v,bool allow_creation=false)
		{
			foreach (HFile f in files)
				if (f.name == v) return f;
			if (allow_creation)
			{
				HFile f = new HFile(v);
				files.Add(f);
				return f;
			}
			return null;
		}

		public HDir parent;

		private HDir(string name) {
			this.name = name;
			dirs = new List<HDir>();
			files = new List<HFile>();
			fullpath = name;
		}
		private HDir(DirectoryInfo dir)
			: this(dir.Name) {
		}

		public static HDir SpawnRoot() {
			HDir root = new FileHydra.HDir("%:");
			root.fullpath = "%:";
			return root;
		}

		public HDir SpawnSubdir(string name) {
			HDir newbie = new HDir(name)
			{
				parent = this,
				fullpath = this.fullpath + "\\" + name
			};
			this.dirs.Add(newbie);
			return newbie;
		}

		override public string ToString() {
			return "{HDir} " + fullpath;
		}

		/**
		 * <summary>Find <i>path</i> in a given folder. Note: search for "%:\FOO\BAR\BLAH" from anywhere, but preferably from Program.masterdir - but search for "BAR\BLAH" from inside "FOO".</summary>
		 */
		internal HDir GetPath(string path,bool allow_creation=false) {
			while (path.Substring(path.Length-1)=="\\") path = path.Substring(0, path.Length - 1);
			string[] path_tokens = path.Split("\\".ToCharArray(), 2);
			string first_token = path_tokens[0];
			if (first_token == "%:") {
				if (name != "%:") // this would be a request for the hydra root
					return Program.hbase.root.GetPath(path, allow_creation);
				if (path_tokens.Length == 1)
					return this;
				// chop the %\ off the start and go from there.
				path_tokens = path_tokens[1].Split("\\".ToCharArray(), 2);
				first_token = path_tokens[0];
			}

			HDir dir = this.FindHDir(first_token,allow_creation);
			if (dir==null)
				throw new HDirNotFoundException(first_token, this.fullpath, path);
			if (path_tokens.Length > 1) {
				string remainder = path_tokens[1];
				return dir.GetPath(remainder, allow_creation);
			} else {
				return dir;
			}
		}

		internal HDir FindHDir(string name,bool allow_creation) {
			foreach (HDir hdir in dirs)
				if (hdir.name == name)
					return hdir;

			if (!allow_creation)
				return null;

			// let's make an hdir
			return this.SpawnSubdir(name);
		}

		internal void Clear() {
			dirs.Clear();
			files.Clear();
			on_volumes.Clear();
		}

		internal void WipeVolume(Volume volume) {
			for (int i = files.Count - 1; i >= 0; i--) {
				var hf = files[i];
				hf.ClearInstancesByVolume(volume);
				if (hf.instances.Count == 0)
					files.RemoveAt(i);
			}
			for (int i = dirs.Count - 1; i >= 0; i--) {
				var hd = dirs[i];
				hd.on_volumes.Remove(volume);
				if (hd.on_volumes.Count == 0)
					dirs.RemoveAt(i);
				else
					hd.WipeVolume(volume);
			}
		}

		internal void AddScanned(SnapDir subsd) {
			var hd = this;
			while (hd != null) {
				if (!hd.on_volumes.Contains(subsd.volume)) hd.on_volumes.Add(subsd.volume);
				hd = hd.parent;
			}
		}
	}

}
