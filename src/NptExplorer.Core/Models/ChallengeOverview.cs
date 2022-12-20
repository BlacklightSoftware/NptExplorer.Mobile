using System;
using System.Collections.Generic;
using System.Linq;
using NptExplorer.Core.Enums;

namespace NptExplorer.Core.Models
{
	public class ChallengeOverview : Location
	{
		public int LocationId { get; set; }
		public string? LocationNameEnglish { get; set; }
		public string? LocationNameWelsh { get; set; }
		public string? ChallengeImage { get; set; }
		public new List<Badge> Badges { get; set; }
		public string? Language { get; set; }

		public string? ChallengeName => Language == "cy" ? LocationNameWelsh : LocationNameEnglish;

		public int TotalBadges => Badges.Count(x => x.BadgeTypeId == BadgeTypes.Nature);
		public int TotalNature => Badges.Count(x => x.BadgeTypeId == BadgeTypes.Nature);
		public int TotalHeritage => Badges.Count(x => x.BadgeTypeId == BadgeTypes.Heritage);
		public int TotalWellbeing => Badges.Count(x => x.BadgeTypeId == BadgeTypes.Wellbeing);
		public int TotalTrail => Badges.Count(x => x.BadgeTypeId == BadgeTypes.Trail);

		public int CollectedBadges => Badges.Count(x => x.Collected);
		public int CollectedNature => Badges.Count(x => x.BadgeTypeId == BadgeTypes.Nature && x.Collected);
		public int CollectedHeritage => Badges.Count(x => x.BadgeTypeId == BadgeTypes.Heritage && x.Collected);
		public int CollectedWellbeing => Badges.Count(x => x.BadgeTypeId == BadgeTypes.Wellbeing && x.Collected);
		public int CollectedTrail => Badges.Count(x => x.BadgeTypeId == BadgeTypes.Trail && x.Collected);

		public string BadgePercentage
		{
			get
			{
				if (TotalBadges == 0)
				{
					return "0%";
				}

				var val = CollectedBadges / TotalBadges * 100;
				return $"{Math.Floor((decimal)val)}%";
			}
		}

		public ChallengeOverview()
		{
			Badges = new List<Badge>();
		}
	}
}