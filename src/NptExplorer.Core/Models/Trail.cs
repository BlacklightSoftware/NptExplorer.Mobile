using NptExplorer.Core.Enums;

namespace NptExplorer.Core.Models
{
	public class Trail : Location
	{
		public new int Id { get; set; }
		public int LocationId { get; set; }
		public string? NameEnglish { get; set; }
		public string? NameWelsh { get; set; }
		public string? LocationNameEnglish { get; set; }
		public string? LocationNameWelsh { get; set; }
		public string? TrailImage { get; set; }
		public string? TrailMapImage { get; set; }
		public Difficulties Difficulty { get; set; }
		public decimal DistanceMiles { get; set; }
		public decimal DistanceKm { get; set; }
		public int TimeHours { get; set; }
		public int TimeMinutes { get; set; }
		public LatLong? StartingPosition { get; set; }
		public string? StartPointDetailsEnglish { get; set; }
		public string? StartPointDetailsWelsh { get; set; }
		public int BadgeId { get; set; }
		public bool Collected { get; set; }

		public string? Language { get; set; }
		public string? TrailName => Language == "cy" ? NameWelsh : NameEnglish;
		public string? LocationName => Language == "cy" ? LocationNameWelsh : LocationNameEnglish;
		public string? StartPointDetails => Language == "cy" ? StartPointDetailsWelsh : StartPointDetailsEnglish;
	}
}