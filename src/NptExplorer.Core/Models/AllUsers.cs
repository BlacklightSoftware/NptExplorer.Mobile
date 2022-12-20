using System;
using System.Collections.Generic;
using System.Text;
using NptExplorer.Dto.Models;

namespace NptExplorer.Core.Models
{
	public class AllUsers
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string UserId { get; set; }
		public List<int> Badges { get; set; }
		public bool isFriend { get; set; }

		public AllUsers()
		{
			Badges = new List<int>();
		}
	}
}
