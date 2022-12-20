using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NptExplorer.Core.Enums;
using NptExplorer.Core.Models;
using NptExplorer.Mobile.Services.Abstract;
using Xamarin.Essentials;

namespace NptExplorer.Mobile.Services.Concrete
{
    public class SettingsService : ISettingsService
    {
        #region Securage Storage
        // These values can be retained by the device even if it has been uninstalled
        private static readonly string _acceptedCriteria = "ss_accepted_criteria";
        private static readonly string _userName = "ss_user_name";
        private static readonly string _userId = "ss_user_id";
		private static readonly string _registered = "ss_registered";
		private static readonly string _guest = "ss_guest";
		private static readonly string _explorerBoard = "ss_explorer_board";

		public async Task SetAcceptedCriteria()
		{
			await SecureStorage.SetAsync(_acceptedCriteria, true.ToString());
		}

		public async Task<bool> GetAcceptedCriteria()
		{
			var result = await SecureStorage.GetAsync(_acceptedCriteria);
			return Convert.ToBoolean(result);
		}

		public async Task SetUserName(string userName)
		{
			await SecureStorage.SetAsync(_userName, userName);
		}

		public async Task<string> GetUserName()
		{
			return await SecureStorage.GetAsync(_userName);
		}

		public Task ClearUserName()
		{
			SecureStorage.Remove(_userName);
			return Task.CompletedTask;
		}

		public async Task SetUserId(string userId)
		{
			await SecureStorage.SetAsync(_userId, userId);
		}

		public async Task<string> GetUserId()
		{
			return await SecureStorage.GetAsync(_userId);
		}

		public Task ClearUserId()
		{
			SecureStorage.Remove(_userId);
			return Task.CompletedTask;
		}

		public async Task SetRegistered(bool registered)
		{
			await SecureStorage.SetAsync(_registered, registered.ToString());
		}

		public async Task<bool> GetRegistered()
		{
			var result = await SecureStorage.GetAsync(_registered);
			return Convert.ToBoolean(result);
		}

		public async Task SetGuest(bool guest)
		{
			await SecureStorage.SetAsync(_guest, guest.ToString());
		}

		public async Task<bool> GetGuest()
		{
			var result = await SecureStorage.GetAsync(_guest);
			return Convert.ToBoolean(result);
		}

		public async Task SetExplorerBoard(bool set)
		{
			await SecureStorage.SetAsync(_explorerBoard, set.ToString());
		}

		public async Task<bool> GetExplorerBoard()
		{
			var result = await SecureStorage.GetAsync(_explorerBoard);
			return Convert.ToBoolean(result);
		}

		#endregion

		#region Preferences
		// these values are deleted when the app is uninstalled
		private static readonly string _preferencesSharedName = "SwanseaGi";
		private static readonly string _hasLaunched = "pref_has_launched";
        private static readonly string _loggedIn = "pref_logged_in";
        private static readonly string _sortBy = "pref_sot_by";
        private static readonly string _badgeFilters = "pref_badge_filters";
        private static readonly string _habitatFilters = "pref_habitat_filters";
        private static readonly string _difficultyFilters = "pref_difficulty_filters";
        private static readonly string _distanceFilters = "pref_distance_filters";
        private static readonly string _trailTimeFilters = "pref_trail_time_filters";
        private static readonly string _facilitiesFilters = "pref_facilities_filters";
        private static readonly string _activitiesFilters = "pref_activities_filters";
        private static readonly string _distanceInMilesFilters = "pref_distance_in_miles_filters";
        private static readonly string _challengeBadgeFilters = "pref_challenge_badge_filters";
        private static readonly string _challengeBadgeCompleted = "pref_challenge_badge_completed";
        private static readonly string _settingsLanguage = "pref_settings_language";
        private static readonly string _settingsFollowers = "pref_settings_followers";
        private static readonly string _settingsUnits = "pref_settings_units";

		public void SetLoggedIn(bool loggedIn)
		{
			Preferences.Set(_loggedIn, loggedIn, _preferencesSharedName);
		}

		public bool IsLoggedIn()
		{
			return Preferences.Get(_loggedIn, false, _preferencesSharedName);
		}

		public void SetHasLaunched()
		{
			Preferences.Set(_hasLaunched, true, _preferencesSharedName);
		}

		public bool GetHasLaunched()
		{
			return Preferences.Get(_hasLaunched, false, _preferencesSharedName);
		}

		public void SetLanguage(string selectedLanguage)
		{
			Preferences.Set(_settingsLanguage, selectedLanguage, _preferencesSharedName);
		}

		public string GetLanguage()
		{
			return Preferences.Get(_settingsLanguage, "", _preferencesSharedName);
		}

		public void SetFollowers(string allowFollowers)
		{
			Preferences.Set(_settingsFollowers, allowFollowers, _preferencesSharedName);
		}

		public string GetFollowers()
		{
			return Preferences.Get(_settingsFollowers, "followers_no", _preferencesSharedName);
		}

		public void SetUnits(string selectedUnits)
		{
			Preferences.Set(_settingsUnits, selectedUnits, _preferencesSharedName);
		}

		public string GetUnits()
		{
			return Preferences.Get(_settingsUnits, "units_miles", _preferencesSharedName);
		}
		
		public void SetFilters(Filters filters)
		{
			Preferences.Set(_sortBy, filters.SortBy, _preferencesSharedName);
			Preferences.Set(_badgeFilters, JsonConvert.SerializeObject(filters.BadgeFilters), _preferencesSharedName);
			Preferences.Set(_habitatFilters, JsonConvert.SerializeObject(filters.HabitatFilters), _preferencesSharedName);
			Preferences.Set(_difficultyFilters, JsonConvert.SerializeObject(filters.DifficultyFilters), _preferencesSharedName);
			Preferences.Set(_distanceFilters, JsonConvert.SerializeObject(filters.DistanceFilters), _preferencesSharedName);
			Preferences.Set(_trailTimeFilters, JsonConvert.SerializeObject(filters.TrailTimeFilters), _preferencesSharedName);
			Preferences.Set(_facilitiesFilters, JsonConvert.SerializeObject(filters.FacilitiesFilters), _preferencesSharedName);
			Preferences.Set(_activitiesFilters, JsonConvert.SerializeObject(filters.ActivitiesFilters), _preferencesSharedName);
			Preferences.Set(_distanceInMilesFilters, JsonConvert.SerializeObject(filters.DistancesInMilesFilters), _preferencesSharedName);
		}

		public Filters GetFilters()
		{
			var filters = new Filters
			{
				SortBy = Preferences.Get(_sortBy, "", _preferencesSharedName)
			};

			var badges = Preferences.Get(_badgeFilters, "", _preferencesSharedName);
			filters.BadgeFilters = string.IsNullOrEmpty(badges)
				? new List<BadgeTypes>()
				: JsonConvert.DeserializeObject<IList<BadgeTypes>>(badges);

			var habitats = Preferences.Get(_habitatFilters, "", _preferencesSharedName);
			filters.HabitatFilters = string.IsNullOrEmpty(habitats)
				? new List<Habitats>()
				: JsonConvert.DeserializeObject<IList<Habitats>>(habitats);

			var difficulties = Preferences.Get(_difficultyFilters, "", _preferencesSharedName);
			filters.DifficultyFilters = string.IsNullOrEmpty(difficulties)
				? new List<Difficulties>()
				: JsonConvert.DeserializeObject<IList<Difficulties>>(difficulties);

			var distances = Preferences.Get(_distanceFilters, "", _preferencesSharedName);
			filters.DistanceFilters = string.IsNullOrEmpty(distances)
				? new List<Distances>()
				: JsonConvert.DeserializeObject<IList<Distances>>(distances);

			var trailsTimes = Preferences.Get(_trailTimeFilters, "", _preferencesSharedName);
			filters.TrailTimeFilters = string.IsNullOrEmpty(trailsTimes)
				? new List<TrailTimes>()
				: JsonConvert.DeserializeObject<IList<TrailTimes>>(trailsTimes);
			var facilitiesTimes = Preferences.Get(_facilitiesFilters, "", _preferencesSharedName);
			filters.FacilitiesFilters = string.IsNullOrEmpty(facilitiesTimes)
				? new List<Facilities>()
				: JsonConvert.DeserializeObject<IList<Facilities>>(facilitiesTimes);
			var activitiesFilters = Preferences.Get(_activitiesFilters, "", _preferencesSharedName);
			filters.ActivitiesFilters = string.IsNullOrEmpty(activitiesFilters)
				? new List<Activities>()
				: JsonConvert.DeserializeObject<IList<Activities>>(activitiesFilters);
			var distanceInMilesFilters = Preferences.Get(_distanceInMilesFilters, "", _preferencesSharedName);
			filters.DistancesInMilesFilters = string.IsNullOrEmpty(distanceInMilesFilters)
				? new int()
				: JsonConvert.DeserializeObject<int>(distanceInMilesFilters);

			return filters;
		}

		public void ClearFilters()
		{
			Preferences.Remove(_sortBy, _preferencesSharedName);
			Preferences.Remove(_badgeFilters, _preferencesSharedName);
			Preferences.Remove(_habitatFilters, _preferencesSharedName);
			Preferences.Remove(_difficultyFilters, _preferencesSharedName);
			Preferences.Remove(_distanceFilters, _preferencesSharedName);
			Preferences.Remove(_trailTimeFilters, _preferencesSharedName);
			Preferences.Remove(_activitiesFilters, _preferencesSharedName);
			Preferences.Remove(_facilitiesFilters, _preferencesSharedName);
			Preferences.Remove(_distanceInMilesFilters, _preferencesSharedName);
		}

		public void SetChallengeFilters(ChallengeFilters filters)
		{
			Preferences.Set(_challengeBadgeCompleted, filters.Completed, _preferencesSharedName);
			Preferences.Set(_challengeBadgeFilters, JsonConvert.SerializeObject(filters.BadgeFilters), _preferencesSharedName);
		}

		public ChallengeFilters GetChallengeFilters()
		{
			var filters = new ChallengeFilters
			{
				Completed = Preferences.Get(_challengeBadgeCompleted, false, _preferencesSharedName)
			};

			var badges = Preferences.Get(_challengeBadgeFilters, "", _preferencesSharedName);
			filters.BadgeFilters = string.IsNullOrEmpty(badges)
				? new List<BadgeTypes>()
				: JsonConvert.DeserializeObject<IList<BadgeTypes>>(badges);

			return filters;
		}

		public void ClearChallengeFilters()
		{
			Preferences.Remove(_challengeBadgeCompleted, _preferencesSharedName);
			Preferences.Remove(_challengeBadgeFilters, _preferencesSharedName);
		}

		public void SetExploreFilters(ExploreFilters filters)
		{
			Preferences.Set(_facilitiesFilters, JsonConvert.SerializeObject(filters.FacilitiesFilters), _preferencesSharedName);
			Preferences.Set(_activitiesFilters, JsonConvert.SerializeObject(filters.ActivitiesFilters), _preferencesSharedName);
			Preferences.Set(_distanceInMilesFilters, JsonConvert.SerializeObject(filters.DistancesInMilesFilters), _preferencesSharedName);
		}

		public ExploreFilters GetExploreFilters()
		{
			var filters = new ExploreFilters();

			var facilitiesTimes = Preferences.Get(_facilitiesFilters, "", _preferencesSharedName);
			filters.FacilitiesFilters = string.IsNullOrEmpty(facilitiesTimes)
				? new List<Facilities>()
				: JsonConvert.DeserializeObject<IList<Facilities>>(facilitiesTimes);
			var activitiesFilters = Preferences.Get(_activitiesFilters, "", _preferencesSharedName);
			filters.ActivitiesFilters = string.IsNullOrEmpty(activitiesFilters)
				? new List<Activities>()
				: JsonConvert.DeserializeObject<IList<Activities>>(activitiesFilters);
			
			return filters;
		}

		public void ClearExploreFilters()
		{
			Preferences.Remove(_activitiesFilters, _preferencesSharedName);
			Preferences.Remove(_facilitiesFilters, _preferencesSharedName);
			Preferences.Remove(_distanceInMilesFilters, _preferencesSharedName);
		}

		public void ClearAllFilters()
		{
			ClearFilters();
			ClearChallengeFilters();
			ClearExploreFilters();
		}

		#endregion
    }
}