namespace NptExplorer.Core.Models
{
	public class PointOfInterest
	{
		public int Id { get; set; }
		public string? NameEnglish { get; set; }
		public string? NameWelsh { get; set; }
		public string? DescriptionEnglish { get; set; }
		public string? DescriptionWelsh { get; set; }
		public string? Image { get; set; }
		public LatLong? Position { get; set; }
		public int BadgeId { get; set; }
		public int BadgeTypeId { get; set; }
		public bool Collected { get; set; }
		public string? Language { get; set; }

		public string? Label => Language == "cy" ? NameWelsh : NameEnglish;
		public string? Description => Language == "cy" ? DescriptionWelsh : DescriptionEnglish;
	}
}