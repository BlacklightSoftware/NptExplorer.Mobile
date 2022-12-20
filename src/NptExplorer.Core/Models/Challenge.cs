using System.Collections.Generic;

namespace NptExplorer.Core.Models
{
	public class Challenge
	{
		public int LocationId { get; set; }
		public string? LocationNameEnglish { get; set; }
		public string? LocationNameWelsh { get; set; }
		public LatLong? Position { get; set; }
		public List<PointOfInterest> PointsOfInterest { get; set; }

		public string? Language { get; set; }
		public string? ChallengeName => Language == "cy" ? LocationNameWelsh : LocationNameEnglish;

		public Challenge()
		{
			PointsOfInterest = new List<PointOfInterest>();
		}
	}
}