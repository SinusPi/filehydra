using FileHydra.NetworkEnums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileHydra {
	class NetworkDrive : Drive {
		public string server;
		public Share share;

		public NetworkDrive(Share share) {
			this.server = share.Server;
			this.share = share;

			this.path = share.Path;
			if (this.path == "") // remote drive, not mapped
				this.path = share.Server + "\\" + share.NetName;
		}

		override public bool IsAvailable() {
			try {
				if (System.IO.File.Exists(share.Path))
					return true;
			} catch (IOException e) { }
			return false;
		}

		public override string ToString() {
			return share.Server + "\\" + share.NetName + " (" + share.Remark + ")";
		}
	}

}
