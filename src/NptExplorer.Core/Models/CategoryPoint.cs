using System;
using System.Collections.Generic;
using System.Text;

namespace NptExplorer.Core.Models
{
	public class CategoryPoint
	{
		public int Id { get; set; }
		public int Adventurer { get; set; }
		public int Champion { get; set; }
		public int Hero { get; set; }
		public int Rockstar { get; set; }
		public object? BadgeType { get; set; }
	}
}
