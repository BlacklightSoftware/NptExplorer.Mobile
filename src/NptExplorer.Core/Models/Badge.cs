using NptExplorer.Core.Enums;

namespace NptExplorer.Core.Models
{
	public class Badge
	{
		public int BadgeId { get; set; }
		public BadgeTypes BadgeTypeId { get; set; }
		public bool Collected { get; set; }
		public int? PointOfInterestId { get; set; }
		public int? TrailId { get; set; }
	}
}