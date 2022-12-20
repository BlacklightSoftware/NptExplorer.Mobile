using System.Collections.Generic;
using System.ComponentModel;
using NptExplorer.Core.Enums;

namespace NptExplorer.Core.Models
{
	public class Filters
	{
		public string? SortBy { get; set; }
		public IList<BadgeTypes> BadgeFilters { get; set; }
		public IList<Difficulties> DifficultyFilters { get; set; }
		public IList<Distances> DistanceFilters { get; set; }
		public IList<Habitats> HabitatFilters { get; set; }
		public IList<TrailTimes> TrailTimeFilters { get; set; }
		public IList<Facilities> FacilitiesFilters { get; set; }
		public IList<Activities> ActivitiesFilters { get; set; }
		public int DistancesInMilesFilters { get; set; }

		public Filters()
		{
			BadgeFilters = new List<BadgeTypes>();
			DifficultyFilters = new List<Difficulties>();
			DistanceFilters = new List<Distances>();
			HabitatFilters = new List<Habitats>();
			TrailTimeFilters = new List<TrailTimes>();
			FacilitiesFilters = new List<Facilities>();
			ActivitiesFilters = new List<Activities>();
			DistancesInMilesFilters = new int();
		}
	}
}