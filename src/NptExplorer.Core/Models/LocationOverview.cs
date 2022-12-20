using System.Collections.Generic;

namespace NptExplorer.Core.Models
{
	public class LocationOverview
	{
		public int Id { get; set; }
		public string NameEnglish { get; set; }
		public string NameWelsh { get; set; }
		public string PrimaryImage { get; set; }
		public List<int> Facilities { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string? Language { get; set; }
		public string? LocationName => Language == "cy" ? NameWelsh : NameEnglish;
	}
}