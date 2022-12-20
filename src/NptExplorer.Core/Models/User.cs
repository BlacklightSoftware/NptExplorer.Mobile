#nullable disable
using System.Collections.Generic;

namespace NptExplorer.Core.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string UserId { get; set; }
		public string ExplorerLevel { get; set; }
		public int Points { get; set; }
		public int Position { get; set; }
		public List<Badge> Badges { get; set; }
		public List<int> Friends { get; set; }
		public bool IsFriend { get; set; }
		public bool ExplorerBoard { get; set; }

		public User()
		{
			Friends = new List<int>();
			Badges = new List<Badge>();
		}
	}
}
