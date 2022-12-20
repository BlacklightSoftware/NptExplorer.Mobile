using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers.Commands;
using NptExplorer.Core.Enums;
using NptExplorer.Core.Exceptions;
using NptExplorer.Core.Models;
using NptExplorer.Dto.Requests;
using NptExplorer.Mobile.Helpers;
using NptExplorer.Mobile.Resources;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;

namespace NptExplorer.Mobile.ViewModels.Profile
{
	public class ProfileAdventureBoardViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		private readonly IDialogService _dialogService;
		private readonly IUserService _userService;
		private readonly ICacheService _cacheService;

		private List<User> _allUsers;
		private List<User> _allFriends;
		private List<User> _filteredUsers;
		private List<User> _filteredFriends;
		private List<User> _boardList;

		private string _signedInUser;
		private User _currentUser;

		private string _sortByFilter;
		private PrerequisiteData _prerequisiteData;

		#region Public properties
		public User CurrentUser
		{
			get => _currentUser;
			set
			{
				_currentUser = value;
				RaisePropertyChanged(() => CurrentUser);
			}
		}

		public string SignedInUser
		{
			get => _signedInUser;
			set
			{
				_signedInUser = value;
				RaisePropertyChanged(() => SignedInUser);
			}
		}

		public List<User> FilteredUsers
		{
			get => _filteredUsers;
			set
			{
				_filteredUsers = value;
				RaisePropertyChanged(() => FilteredUsers);
			}
		}

		public List<User> FilteredFriends
		{
			get => _filteredFriends;
			set
			{
				_filteredFriends = value;
				RaisePropertyChanged(() => FilteredFriends);
			}
		}
		public List<User> BoardList
		{
			get => _boardList;
			set
			{
				_boardList = value;
				RaisePropertyChanged(() => BoardList);
			}
		}

		public string SortByFilter
		{
			get => _sortByFilter;
			set
			{
				_sortByFilter = value;
				RaisePropertyChanged(() => SortByFilter);
			}
		}
		#endregion

		public ICommand GoToFriends { get; set; }
		public ICommand FilterByWellbeing { get; set; }
		public ICommand FilterByNature { get; set; }
		public ICommand FilterByHeritage { get; set; }
		public ICommand FilterByTrail { get; set; }
		public ICommand LeaveBoard { get; set; }

		public ProfileAdventureBoardViewModel(IUserService userService,
			INavigationService navigationService,
			ISettingsService settingsService,
			IDialogService dialogService,
			ICacheService cacheService) : base(navigationService)
		{
			_settingsService = settingsService;
			_dialogService = dialogService;
			_userService = userService;
			_cacheService = cacheService;

			GoToFriends = new Command(NavigateToFriends);
			FilterByWellbeing = new Command(FilterByWellbeingAsync);
			FilterByNature = new Command(FilterByNatureAsync);
			FilterByHeritage = new Command(FilterByHeritageAsync);
			FilterByTrail = new Command(FilterByTrailAsync);
			LeaveBoard = new Command(LeaveBoardAsync);
		}

		public async void Init()
		{
			if (IsBusy)
			{
				return;
			}

			try
			{
				StartBusy();
				ScreenTitle = Labels.GetHelloLabel(await _settingsService.GetUserName());
				var userId = await _settingsService.GetUserId();
				_prerequisiteData = await _cacheService.GetPrerequisiteData();
				SortByFilter = "board_friends";

				_allUsers = await _userService.GetAllUsers();
				CurrentUser = await _userService.GetUser(userId);
				_allFriends = _allUsers.Where(x => CurrentUser.Friends.Contains(x.Id))
					.Where(x => x.IsFriend = true).ToList();
				
				await FilterBoard(null);
			}
			catch (Exception ex)
			{
				await _dialogService.ShowAlertAsync(
					AppResources.Error_LoadingLabel,
					AppResources.Error_ErrorTitle, AppResources.Error_OkButton);

				if (ex.GetType() != typeof(NoInternetConnectionException))
				{
					//_loggingService.Error(ex);
				}
			}
			finally
			{
				StopBusy();
			}
		}

		public Task CheckRadioChange()
		{
			switch (SortByFilter)
			{
				case "board_all":
				{
					for (var i = 0; i < FilteredUsers.Count; i++)
					{
						FilteredUsers[i].Position = i + 1;
					}
					BoardList = FilteredUsers;
					break;
				}
				case "board_friends":
				{
					for (var i = 0; i < FilteredFriends.Count; i++)
					{
						FilteredFriends[i].Position = i + 1;
					}
					BoardList = FilteredFriends;
					break;
				}
			}

			return Task.CompletedTask;
		}

		public async void NavigateToFriends()
		{
			await NavigationService.GoToAsync("/profileFriends");
		}

		public string GetExplorerLevel(int collectedPoints, BadgeTypes? filter)
		{
			int heroLevel;
			int championLevel;
			int adventurerLevel;

			switch (filter)
			{
				case BadgeTypes.Wellbeing:
					heroLevel = _prerequisiteData.WellbeingHeroLevel;
					championLevel = _prerequisiteData.WellbeingChampionLevel;
					adventurerLevel = _prerequisiteData.WellbeingAdventureLevel;
					break;
				case BadgeTypes.Nature:
					heroLevel = _prerequisiteData.NatureHeroLevel;
					championLevel = _prerequisiteData.NatureChampionLevel;
					adventurerLevel = _prerequisiteData.NatureAdventureLevel;
					break;
				case BadgeTypes.Heritage:
					heroLevel = _prerequisiteData.HeritageHeroLevel;
					championLevel = _prerequisiteData.HeritageChampionLevel;
					adventurerLevel = _prerequisiteData.HeritageAdventureLevel;
					break;
				case BadgeTypes.Trail:
					heroLevel = _prerequisiteData.TrailHeroLevel;
					championLevel = _prerequisiteData.TrailChampionLevel;
					adventurerLevel = _prerequisiteData.TrailAdventureLevel;
					break;
				default:
					heroLevel = _prerequisiteData.OverallHeroLevel;
					championLevel = _prerequisiteData.OverallChampionLevel;
					adventurerLevel = _prerequisiteData.OverallAdventureLevel;
					break;
			}

			return collectedPoints >= heroLevel ? Badges.Hero.ToString() :
				collectedPoints >= championLevel ? Badges.Champion.ToString() :
				collectedPoints >= adventurerLevel ? Badges.Adventurer.ToString() : Badges.Explorer.ToString();
		}

		public int BadgeToPoints(User user, BadgeTypes? filter)
		{
			var wellbeingPoints = user.Badges.Count(x => x.BadgeTypeId == BadgeTypes.Wellbeing) * _prerequisiteData.WellbeingBadgePoints;
			var naturePoints = user.Badges.Count(x => x.BadgeTypeId == BadgeTypes.Nature) * _prerequisiteData.NatureBadgePoints;
			var heritagePoints = user.Badges.Count(x => x.BadgeTypeId == BadgeTypes.Heritage) * _prerequisiteData.HeritageBadgePoints;
			var trailPoints = user.Badges.Count(x => x.BadgeTypeId == BadgeTypes.Trail) * _prerequisiteData.TrailBadgePoints;

			return filter switch
			{
				BadgeTypes.Wellbeing => wellbeingPoints,
				BadgeTypes.Nature => naturePoints,
				BadgeTypes.Heritage => heritagePoints,
				BadgeTypes.Trail => trailPoints,
				_ => wellbeingPoints + naturePoints + heritagePoints + trailPoints
			};
		}

		private async void FilterByWellbeingAsync()
		{
			await FilterBoard(BadgeTypes.Wellbeing);
		}

		private async void FilterByNatureAsync()
		{
			await FilterBoard(BadgeTypes.Nature);
		}

		private async void FilterByHeritageAsync()
		{
			await FilterBoard(BadgeTypes.Heritage);
		}

		private async void FilterByTrailAsync()
		{
			await FilterBoard(BadgeTypes.Trail);
		}

		private async Task FilterBoard(BadgeTypes? badge)
		{
			var allUsers = _allUsers;
			foreach (var user in allUsers)
			{
				user.Points = BadgeToPoints(user, badge);
				user.ExplorerLevel = GetExplorerLevel(user.Points, badge);
			}

			FilteredUsers = allUsers.OrderByDescending((x) => x.Points).ToList();
			for (var i = 0; i < FilteredUsers.Count; i++)
			{
				FilteredUsers[i].Position = i + 1;
			}

			FilteredFriends = _allFriends.OrderByDescending((x) => x.Points).ToList();
			for (var i = 0; i < FilteredFriends.Count; i++)
			{
				FilteredFriends[i].Position = i + 1;
			}

			await CheckRadioChange();
		}

		private async void LeaveBoardAsync()
		{
			if (IsBusy)
			{
				return;
			}

			try
			{
				var result = await _userService.SetExplorerBoardInclusion(new ExplorerBoardRequest { UserId = CurrentUser.Id, Include = false });
				if (!result)
				{
					await _dialogService.ShowAlertAsync(AppResources.Profile_JoinExplorerBoardErrorMessage,
						AppResources.Error_ErrorTitle, AppResources.Error_OkButton);
					return;
				}

				await NavigationService.GoToAsync("///profile");
			}
			catch
			{
				await _dialogService.ShowAlertAsync(AppResources.Profile_JoinExplorerBoardErrorMessage,
					AppResources.Error_ErrorTitle, AppResources.Error_OkButton);
			}
		}
	}
}
