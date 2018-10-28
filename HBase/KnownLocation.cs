namespace FileHydra {
	public class KnownLocation {
		public Recognition recognition = null;
		public string rootpath = @"\";

		public KnownLocation(Recognition recognition,string rootpath=@"\")
		{
			this.recognition = recognition;
			this.rootpath = rootpath;
		}
	}
}