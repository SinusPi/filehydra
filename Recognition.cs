using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace FileHydra {
	[Serializable]
    public abstract class Recognition {
		public readonly bool CanRecognizeLocalDrive;
		public readonly bool CanRecognizeNetworkShare;
		public string rootpath;

		protected Recognition (bool local,bool network,string rootpath) {
			CanRecognizeLocalDrive = local;
			CanRecognizeNetworkShare = network;
			this.rootpath = rootpath;
		}
		public abstract bool IsRecognizedOn(Drive drive);
		protected bool DriveTypeCorrect(Drive drive) {
			return (drive is NetworkDrive && CanRecognizeNetworkShare)
				|| (drive is LocalDrive && CanRecognizeLocalDrive);
		}

		internal string ToString(Drive drive) {
			return (drive.ToString() + "+" + rootpath);
		}
	}

	/// <summary>
	/// Recognizes a <see cref="Volume"/> by a volume label set on a <see cref="Drive"/> (local).
	/// </summary>
	[Serializable]
	public class RecognitionVolumeLabel : Recognition {
        public string label;

        public RecognitionVolumeLabel(string label,string rootpath=@"\"):base(local:true,network:false,rootpath:rootpath) {
            this.label = label;
        }

        public override bool IsRecognizedOn(Drive drive) {
			if (!DriveTypeCorrect(drive)) return false;
            for (var i = 1; i < 10; i++) {  // retries, in case of pesky IOException "drive not ready" errors when trying to read the volume label. What is that about...
                try {
                    var localdrive = drive as LocalDrive;
                    if (localdrive == null) return false;
                    return localdrive.driveinfo.VolumeLabel == label;
                } catch (IOException e) {
                }
                System.Threading.Thread.Sleep(100);
            }
            return false;
        }
    }

	/// <summary>
	/// Recognizes a <see cref="Volume"/> by presence of a <see cref="Drive"/> being a network share matching a \\SERVER\SHARE format (network only).
	/// </summary>
	[Serializable]
	public class RecognitionServerShare : Recognition {
        public string servershare;
        public bool is_regex=false;

		public RecognitionServerShare(string servershare,string rootpath=@"\"):base(local:false,network:true,rootpath:rootpath) {
            this.servershare = servershare;
        }
        public override bool IsRecognizedOn(Drive drive) {
			if (!DriveTypeCorrect(drive)) return false;
			for (var i = 1; i < 10; i++) {  // retries, in case of pesky IOException "drive not ready" errors
                try {
                    var netdrive = drive as NetworkDrive;
                    if (netdrive == null) return false;
                    if (!is_regex) return netdrive.share.Server + "\\" + netdrive.share.NetName == servershare;
                    else {
                        Regex re = new Regex(servershare);
                        return re.IsMatch(netdrive.share.Server + "\\" + netdrive.share.NetName);
                    }
                } catch (IOException e) {
                }
                System.Threading.Thread.Sleep(100);
            }
            return false;
        }
    }

	/// <summary>
	/// Recognizes a <see cref="Volume"/> by presence of a file on a <see cref="Drive"/> (local or network).
	/// </summary>
	[Serializable]
	public class RecognitionFile : Recognition {
        public string filepath;

		public RecognitionFile(string filepath, string rootpath = @"\") : base(local: true, network: true, rootpath: rootpath) {
			this.filepath = filepath;
		}
		public override bool IsRecognizedOn(Drive drive) {
			if (!DriveTypeCorrect(drive)) return false;
			for (var i = 1; i < 10; i++) {  // retries, in case of pesky IOException "drive not ready" errors
				try {
					return System.IO.File.Exists(drive.path + "\\" + filepath)
						|| System.IO.Directory.Exists(drive.path + "\\" + filepath);
				}
				catch (IOException e) {
				}
				System.Threading.Thread.Sleep(100);
			}
			return false;
		}
    }

	/// <summary>
	/// Recognizes a <see cref="Volume"/> by a specific Hydra label file on a <see cref="Drive"/> (local or network).
	/// </summary>
	[Serializable]
	public class RecognitionHydraLabel : Recognition {
        public string label;

		public RecognitionHydraLabel(string label, string rootpath = @"\") : base(local: true, network: true, rootpath: rootpath) {
			this.label = label;
		}
        public override bool IsRecognizedOn(Drive drive) {
			if (!DriveTypeCorrect(drive)) return false;
			for (var i = 1; i < 10; i++) {  // retries, in case of pesky IOException "drive not ready" errors
                try {
                    string[] lines = System.IO.File.ReadAllLines(drive.path + "\\hydralab.dat");
                    return (lines.Length > 0) && (lines[0].Trim() == label);
                } catch (IOException e) {
                } catch (AccessViolationException e) {
                }
                System.Threading.Thread.Sleep(100);
            }
            return false;
        }
    }
}
