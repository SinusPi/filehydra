using System;
using System.Runtime.Serialization;

namespace FileHydra {
	[Serializable]
	internal class VolumeUnknownException : Exception {
		public VolumeUnknownException() {
		}

		public VolumeUnknownException(string message) : base(message) {
		}

		public VolumeUnknownException(string message, Exception innerException) : base(message, innerException) {
		}

		protected VolumeUnknownException(SerializationInfo info, StreamingContext context) : base(info, context) {
		}
	}
}