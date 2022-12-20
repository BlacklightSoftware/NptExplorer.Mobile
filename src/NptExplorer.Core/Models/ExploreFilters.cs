using System.Collections.Generic;
using NptExplorer.Core.Enums;

namespace NptExplorer.Core.Models
{
	public class ExploreFilters
	{
		public IList<Facilities> FacilitiesFilters { get; set; }
		public IList<Activities> ActivitiesFilters { get; set; }
		public int DistancesInMilesFilters { get; set; }

		public ExploreFilters()
		{
			FacilitiesFilters = new List<Facilities>();
			ActivitiesFilters = new List<Activities>();
			DistancesInMilesFilters = new int();
		}
	}
}