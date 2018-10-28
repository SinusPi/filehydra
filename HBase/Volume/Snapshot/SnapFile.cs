using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileHydra {
	[Serializable]
	class SnapFile {
		public string name;
		public long size = -1;
		public DateTime mtime = DateTime.MinValue;
		[NonSerialized] public Volume volume;
		public SnapFile(string name) {
			this.name = name;
		}
		public SnapFile(FileInfo fi)
			: this(fi.Name) {
			size = fi.Length;
			mtime = fi.LastWriteTime;
		}
	}

}
