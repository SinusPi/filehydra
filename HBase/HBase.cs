using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileHydra
{
	//[System.Diagnostics.DebuggerDisplay("{ToString()}")]
	/// <summary>
	/// HBase represents a whole database of files and their hypothetical locations. 
	/// </summary>
	[Serializable]
	class HBase
	{
		/// <summary>
		/// Give it a name, if you want.
		/// </summary>
		public string name;

		/// <summary>
		/// HBase file structure starts here. Folder structure and file metadata are added here. No actual file contents are stored, of course.
		/// Volatile!
		/// </summary>
		[NonSerialized] public HDir root = HDir.SpawnRoot();


		/// <summary>
		/// List of known drives or server shares parts of the base may reside on. Multiple ways of recognizing a volume are available.
		/// </summary>
		public Volumes volumes;

		[NonSerialized] private bool dirty;

		internal static HBase CreateExample()
		{
			HBase hb = new HBase
			{
				name = "Example Base",
			};

			/*
			HDir r;
			r = hb.root.GetPath(@"%:\cars", true); r.GetFile("ford", true); r.GetFile("mazda", true);
			r = hb.root.GetPath(@"%:\fruit", true); r.GetFile("apple", true); r.GetFile("orange", true);
			r = hb.root.GetPath(@"%:\fruit\green", true); r.GetFile("cucumber", true); r.GetFile("avocado", true);
			r = hb.root.GetPath(@"%:\animals", true); r.GetFile("cat", true); r.GetFile("dog", true);
			r = hb.root.GetPath(@"%:\animals\large", true); r.GetFile("elephant", true); r.GetFile("whale", true);
			*/

			if (hb.volumes == null) hb.volumes = new Volumes();

			if (hb.volumes==null) hb.volumes = new Volumes();

			hb.volumes.Add(new Volume("smarkofag"));
			hb.volumes["smarkofag"].AddKnownLocation(new KnownLocation(new RecognitionVolumeLabel("SD_MAKROFAG")));
			hb.volumes.Add(new Volume("koszyk_c"));
			hb.volumes["koszyk_c"].AddKnownLocation(new KnownLocation(new RecognitionServerShare(@"\\KOSZYK\C")));
			hb.volumes["koszyk_c"].AddKnownLocation(new KnownLocation(new RecognitionFile(@"Users\Ailinon")));
			hb.volumes.Add(new Volume("kaszalot"));
			hb.volumes["kaszalot"].AddKnownLocation(new KnownLocation(new RecognitionFile(@"kaszalot.txt")));
			hb.volumes.Add(new Volume("sigejt bekap"));
			hb.volumes["sigejt bekap"].AddKnownLocation(new KnownLocation(new RecognitionFile(@"sigejt bekap.txt")));
			hb.volumes.Add(new Volume("paluch"));
			hb.volumes["paluch"].AddKnownLocation(new KnownLocation(new RecognitionVolumeLabel("PALUCH")));
			hb.volumes.Add(new Volume("monolit_c"));
			hb.volumes["monolit_c"].AddKnownLocation(new KnownLocation(new RecognitionVolumeLabel("SSD Windows")));
			hb.volumes.Add(new Volume("nautilus_c"));
			hb.volumes["nautilus_c"].AddKnownLocation(new KnownLocation(new RecognitionVolumeLabel("ACER")));

			hb.volumes["koszyk_c"]?.AddMapping(@"\ZDJECIA\MISC", @"%:\FOTO\MISC");
			hb.volumes["monolit_c"]?.AddMapping(@"\FOTO\MISC", @"%:\FOTO\MISC");
			hb.volumes["monolit_c"]?.AddMapping(@"\FOTO\MISC2", @"%:\FOTO\MISC");
			hb.volumes["monolit_c"]?.AddMapping(@"\FOTO\łyżwy", @"%:\FOTO\łyżwy");

			return hb;
		}

		internal void Render() {
			foreach (var v in volumes) {
				v.Render();
			}
		}

		internal void Save(string filename) {
			BinarySerialization.WriteToBinaryFile(filename, this);
			dirty = false;
		}

		internal static HBase Load(string filename=null) {
			HBase hbase = BinarySerialization.ReadFromBinaryFile<HBase>(filename);
			hbase.dirty = false;
			return hbase;
		}

		internal void SetDirty() {
			dirty = true;
			Program.UpdateTitle();
		}
		internal bool IsDirty() {
			return dirty;
		}

	}
}
