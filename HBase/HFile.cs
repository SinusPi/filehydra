using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileHydra {
	[Serializable]
	class HFile {
		public string name;
		public DateTime mtime;

		public List<SnapFile> instances = new List<SnapFile>();

		public HFile(string name) {
			this.name = name;
		}

		public HFile(string name,DateTime mtime) : this(name) {
			this.mtime = mtime;
		}
		public HFile(FileInfo file)
			: this(file.Name,file.LastWriteTime) {
		}

		internal void AddInstance(SnapFile sf) {
			foreach (SnapFile f in instances) {
				if (f.volume == sf.volume) return;
			}
			instances.Add(sf);
		}
		internal void ClearInstancesByVolume(Volume v) {
			instances = instances.Where(i => i.volume == v).ToList();
		}
	}

}
