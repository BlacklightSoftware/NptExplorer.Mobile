using System;

namespace NptExplorer.Mobile.Models
{
	public class CheckIn
	{
		public CheckIn(int badgeId, DateTime utcNow)
		{
			BadgeId = badgeId;
			DateCheckedIn = utcNow;
		}

		public int BadgeId { get; set; }
		public DateTime DateCheckedIn { get; set; }
	}
}