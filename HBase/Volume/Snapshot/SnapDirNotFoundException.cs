using System;
using System.Runtime.Serialization;

namespace FileHydra {
	[Serializable]
	internal class SnapDirNotFoundException : Exception {
		private string first_token;
		private string fullpath;
		private string path;

		public SnapDirNotFoundException() {
		}

		public SnapDirNotFoundException(string message) : base(message) {
		}

		public SnapDirNotFoundException(string message, Exception innerException) : base(message, innerException) {
		}

		public SnapDirNotFoundException(string first_token, string fullpath, string path) {
			this.first_token = first_token;
			this.fullpath = fullpath;
			this.path = path;
		}

		protected SnapDirNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) {
		}
	}
}