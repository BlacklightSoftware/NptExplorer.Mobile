namespace NptExplorer.Core.Models
{
	public class LatLong
	{
		public double Longitude { get; set; }
		public double Latitude { get; set; }

		public LatLong(double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}
	}
}