using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileHydra {
	[Serializable]
	class Volumes : List<Volume> {
		public static bool is_dirty;

		/*
		public static void Load() {
			try {
				KnownVolumes knownVolumes = BinarySerialization.ReadFromBinaryFile<KnownVolumes>(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\filehydra.dat");
			}
			catch (IOException e) {
				MessageBox.Show(e.Message);
			}
			is_dirty = true;
		}

		internal void Save() {
			BinarySerialization.WriteToBinaryFile<KnownVolumes>(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\filehydra.dat", this);
		}
		*/

		public void FindAllOnKnownDrives(bool do_reset) {
			List<Volume> results = new List<Volume>();
			foreach (var volume in this) {
				volume.ReFind();
				is_dirty = true;
			}
		}

		public Volume FindByName(string volumename) {
			foreach (var vol in this) {
				if (vol.Label == volumename)
					return vol;
			}
			return null;
		}

		public Volume this[string name] {
			get {
				return this.FindByName(name);
			}
		}
	}

}
