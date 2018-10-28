using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileHydra {
	/// <summary>
	/// <para>Represents a physical drive, currently accessible somewhere.</para>
	/// <para>A <see cref="Volume"/>Volume</see> may reside on it.</para>
	/// </summary>
	public class Drive {
		// saved data
		public string path;

		public string GetHydraLabel() {
			try {
				string[] lines = System.IO.File.ReadAllLines(path + "\\hydravol.dat");
				if (lines.Length > 0 && lines[0].Length > 0) {
					return lines[0].Trim();
				}
			}
			catch (IOException e) {
				return null;
			}
			return null;
		}

		public virtual bool IsAvailable() { return false; }

		override public string ToString() {
			return path;
		}
	}

}
