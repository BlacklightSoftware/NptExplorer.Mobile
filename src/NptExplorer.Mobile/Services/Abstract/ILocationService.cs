using System.Collections.Generic;
using NptExplorer.Core.Models;
using System.Threading.Tasks;
using NptExplorer.Dto.Requests;

namespace NptExplorer.Mobile.Services.Abstract
{
	public interface ILocationService
	{
		Task<Location> GetLocation(string id);
		Task<List<LocationOverview>> GetLocationsOverview(LocationRequest locationRequest);
		Task<List<LocationOverview>> GetSearchedLocation(LocationRequest locationRequest);
		Task<List<LocationOverview>> GetLocations(LocationRequest locationRequest);
	}
}
