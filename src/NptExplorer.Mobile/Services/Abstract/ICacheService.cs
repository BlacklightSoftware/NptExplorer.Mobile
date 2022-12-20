using System.Collections.Generic;
using System.Threading.Tasks;
using NptExplorer.Core.Models;
using NptExplorer.Mobile.Models;

namespace NptExplorer.Mobile.Services.Abstract
{

	public interface ICacheService
	{
		Task<PrerequisiteData> GetPrerequisiteData();
		void SetSelectedChallenge(int id);
		int GetSelectedChallenge();
		void ClearSelectedChallenge();
		void SetSelectedPoi(ChallengePoi poi);
		ChallengePoi GetSelectedPoi();
		void ClearSelectedPoi();
		List<CheckIn> GetCheckIns();
		void SetCheckIn(int badgeId);
		void ClearExpiredCheckIns();
		void RemoveCheckIn(int badgeId);
		void SetSelectedLocations(List<LocationOverview> locations);
		List<LocationOverview> GetSelectedLocations();
		void ClearSelectedLocations();
		void ClearCache();
	}
}