using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileHydra {
	class LocalDrive : Drive {
		public DriveInfo driveinfo;

		public LocalDrive(DriveInfo drive) {
			this.path = drive.Name;
			this.driveinfo = drive;
		}

		public override bool IsAvailable() {
			try {
				if (driveinfo.RootDirectory.Exists)
					return true;
			}
			catch (IOException e) {
			}
			return false;
		}

		public override string ToString() {
			return path + " (" + driveinfo.VolumeLabel + ")";
		}

	}

}
