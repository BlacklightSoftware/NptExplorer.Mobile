using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MonkeyCache;
using MonkeyCache.FileStore;
using NptExplorer.Core.Models;
using NptExplorer.Dto.Models;
using NptExplorer.Mobile.Constants;
using NptExplorer.Mobile.Models;
using NptExplorer.Mobile.Services.Abstract;
using Xamarin.Essentials;
using static MonkeyCache.FileStore.Barrel;

namespace NptExplorer.Mobile.Services.Concrete
{
	public class CacheService : ICacheService
	{
		private readonly IBarrel _barrel;
		private readonly IRequestProviderService _requestProviderService;
		private readonly ILoggingService _loggingService;
		private readonly IMapper _mapper;

		public static readonly string PrerequisiteDataFile = "prerequisiteData.json";
		public static readonly string SelectedPointOfInterest = "selectedPoi.json";
		public static readonly string SelectedChallenge = "selectedChallenge.json";
		public static readonly string CheckIns = "checkIns.json";
		public static readonly string SelectedLocations = "selectedLocations.json";

		public CacheService(ILoggingService loggingService, IRequestProviderService requestProviderService, IMapper mapper)
		{
			Barrel.ApplicationId = AppInfo.PackageName;
			_barrel = Create(FileSystem.AppDataDirectory);

			_loggingService = loggingService;
			_requestProviderService = requestProviderService;
			_mapper = mapper;
		}

		public void SetPrerequisiteData(PrerequisiteData questions)
		{
			_barrel.Add(PrerequisiteDataFile, questions, TimeSpan.FromHours(3));
		}

		public async Task<PrerequisiteData> GetPrerequisiteData()
		{
			if (_barrel.IsExpired(PrerequisiteDataFile))
			{
				await GetPrerequisiteDataFromServer();
			}

			return _barrel.Get<PrerequisiteData>(key: PrerequisiteDataFile);
		}

		public async Task GetPrerequisiteDataFromServer()
		{
			_loggingService.Track("Getting Prerequisite Data");

			try
			{
				var response = await _requestProviderService.AttemptAndRetry(() =>
					_requestProviderService.Get<PrerequisiteDataDto>(ApiUrls.GetPrerequisiteData));
				var data = _mapper.Map<PrerequisiteData>(response);
				if (data != null)
				{
					SetPrerequisiteData(data);
				}
			}
			catch (Exception ex)
			{
				_loggingService.Track($"Error getting prerequisite data - will used cached if available");
			}
		}

		public void SetSelectedChallenge(int challengeId)
		{
			_barrel.Add(SelectedChallenge, challengeId, TimeSpan.FromDays(1));
		}

		public int GetSelectedChallenge()
		{
			return !_barrel.IsExpired(SelectedChallenge)
				? _barrel.Get<int>(key: SelectedChallenge)
				: 0;
		}

		public void ClearSelectedChallenge()
		{
			_barrel.Empty(SelectedChallenge);
		}

		public void SetSelectedPoi(ChallengePoi poi)
		{
			_barrel.Add(SelectedPointOfInterest, poi, TimeSpan.FromDays(1));
		}

		public ChallengePoi GetSelectedPoi()
		{
			return !_barrel.IsExpired(SelectedPointOfInterest)
				? _barrel.Get<ChallengePoi>(key: SelectedPointOfInterest)
				: new ChallengePoi();
		}

		public void ClearSelectedPoi()
		{
			_barrel.Empty(SelectedPointOfInterest);
		}

		public void SetCheckIn(int badgeId)
		{
			var list = GetCheckIns();
			if (list.Any(x => x.BadgeId == badgeId))
			{
				return;
			}

			list.Add(new CheckIn(badgeId, DateTime.UtcNow));
			_barrel.Add(CheckIns, list, TimeSpan.FromDays(1));
		}

		public List<CheckIn> GetCheckIns()
		{
			if (_barrel.IsExpired(CheckIns))
			{
				return new List<CheckIn>();
			}
			var checkIns = _barrel.Get<List<CheckIn>>(key: CheckIns);
			return checkIns ?? new List<CheckIn>();
		}

		public void ClearExpiredCheckIns()
		{
			var list = GetCheckIns();
			var newList = list.Where(checkIn => checkIn.DateCheckedIn > DateTime.UtcNow.AddHours(-6)).ToList();
			_barrel.Add(CheckIns, newList, TimeSpan.FromDays(1));
		}

		public void RemoveCheckIn(int badgeId)
		{
			var list = GetCheckIns();
			var itemToRemove = list.SingleOrDefault(x => x.BadgeId == badgeId);
			if (itemToRemove != null)
			{
				_ = list.Remove(itemToRemove);
			}

			_barrel.Add(CheckIns, list, TimeSpan.FromDays(1));
		}

		public void SetSelectedLocations(List<LocationOverview> locations)
		{
			_barrel.Add(SelectedLocations, locations, TimeSpan.FromDays(1));
		}

		public List<LocationOverview> GetSelectedLocations()
		{
			if (_barrel.IsExpired(SelectedLocations))
			{
				return new List<LocationOverview>();
			}
			var locations = _barrel.Get<List<LocationOverview>>(key: SelectedLocations);
			return locations ?? new List<LocationOverview>();
		}

		public void ClearSelectedLocations()
		{
			_barrel.Empty(SelectedLocations);
		}

		public void ClearCache()
		{
			ClearExpiredCheckIns();
			ClearSelectedPoi();
			ClearSelectedChallenge();
			ClearSelectedLocations();
		}
	}
}