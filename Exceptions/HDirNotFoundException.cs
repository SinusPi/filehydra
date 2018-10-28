using System;
using System.Runtime.Serialization;

namespace FileHydra {
	[Serializable]
	internal class HDirNotFoundException : Exception {
		private string fullname;
		private string path;
		private string v;

		public HDirNotFoundException() {
		}

		public HDirNotFoundException(string message) : base(message) {
		}

		public HDirNotFoundException(string message, Exception innerException) : base(message, innerException) {
		}

		public HDirNotFoundException(string v, string fullname, string path) {
			this.v = v;
			this.fullname = fullname;
			this.path = path;
		}

		protected HDirNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) {
		}
	}
}