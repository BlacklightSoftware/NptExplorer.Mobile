using System.Collections.Generic;
using NptExplorer.Dto.Models;

namespace NptExplorer.Core.Models
{
	public class TrailRoute : Trail
	{
		public List<List<LatLongDto>> Route { get; set; }	}
}