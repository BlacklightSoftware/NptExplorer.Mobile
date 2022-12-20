using System;
using System.Collections.Generic;
using System.Text;
using NptExplorer.Core.Enums;

namespace NptExplorer.Core.Models
{
	public class TestUser
	{
		public string Username { get; set; }
		public string Image { get; set; }
		public Badges Badge { get; set; }
		public int Points { get; set; }
		public int Position { get; set; }
	}
}
