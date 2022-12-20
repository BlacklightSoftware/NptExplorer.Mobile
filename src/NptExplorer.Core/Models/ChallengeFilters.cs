using System.Collections.Generic;
using NptExplorer.Core.Enums;

namespace NptExplorer.Core.Models
{
	public class ChallengeFilters
	{
		public bool Completed { get; set; }
		public IList<BadgeTypes> BadgeFilters { get; set; }

		public ChallengeFilters()
		{
			BadgeFilters = new List<BadgeTypes>();
		}
	}
}