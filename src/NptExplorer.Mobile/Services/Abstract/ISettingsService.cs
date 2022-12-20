using System.Threading.Tasks;
using NptExplorer.Core.Models;

namespace NptExplorer.Mobile.Services.Abstract
{
    public interface ISettingsService
    {
	    #region Settings
	    Task SetAcceptedCriteria();
	    Task<bool> GetAcceptedCriteria();
	    Task SetUserName(string userName);
	    Task<string> GetUserName();
	    Task ClearUserName();
	    Task SetUserId(string userId);
	    Task<string> GetUserId();
	    Task ClearUserId();
		Task SetRegistered(bool registered);
		Task<bool> GetRegistered();
		Task SetGuest(bool guest);
		Task<bool> GetGuest();
		#endregion

		#region Preferences
		void SetLoggedIn(bool loggedIn);
		bool IsLoggedIn();
		void SetHasLaunched();
		bool GetHasLaunched();
		void SetLanguage(string selectedLanguage);
		string GetLanguage();
		void SetFollowers(string allowFollowers);
		string GetFollowers();
		void SetUnits(string selectedUnits);
		string GetUnits();
		void SetFilters(Filters filters);
		Filters GetFilters();
		void ClearFilters();
		void SetChallengeFilters(ChallengeFilters filters);
		ChallengeFilters GetChallengeFilters();
		void ClearChallengeFilters();
		void SetExploreFilters(ExploreFilters filters);
		ExploreFilters GetExploreFilters();
		void ClearExploreFilters();
		void ClearAllFilters();
		#endregion

    }
}