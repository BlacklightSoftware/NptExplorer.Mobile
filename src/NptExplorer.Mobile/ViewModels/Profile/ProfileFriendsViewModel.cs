using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using NptExplorer.Core.Exceptions;
using NptExplorer.Core.Models;
using NptExplorer.Dto.Requests;
using NptExplorer.Mobile.Helpers;
using NptExplorer.Mobile.Resources;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using Xamarin.Forms;

namespace NptExplorer.Mobile.ViewModels.Profile
{
	public class ProfileFriendsViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		private readonly IDialogService _dialogService;
		private readonly IUserService _userService;

		private string _searchValue;
		private string _signedInUser;

		private List<User> _users;
		private List<User> _allUsers;
		private List<User> _searchResults;

		private User _currentUser;

		public ICommand SearchCommand { get; set; }

		#region public properties
		public User CurrentUser
		{
			get => _currentUser;
			set
			{
				_currentUser = value;
				RaisePropertyChanged(() => CurrentUser);
			}
		}

		public List<User> AllUsers
		{
			get => _allUsers;
			set
			{
				_allUsers = value;
				RaisePropertyChanged(() => AllUsers);
			}
		}

		public List<User> SearchResult
		{
			get => _searchResults;
			set
			{
				_searchResults = value;
				RaisePropertyChanged(() => SearchResult);
			}
		}

		public string SearchValue
		{
			get => _searchValue;
			set
			{
				_searchValue = value;
				SearchForUsers();
				RaisePropertyChanged(() => SearchValue);
			}
		}

		public List<User> Users
		{
			get => _users;
			set
			{
				_users = value;
				RaisePropertyChanged(() => Users);
			}

		}
		#endregion

		public ProfileFriendsViewModel(IUserService userService,
			IDialogService dialogService,
			INavigationService navigationService,
			ISettingsService settingsService) : base(navigationService)
		{
			_settingsService = settingsService;
			_dialogService = dialogService;
			_userService = userService;

			SearchCommand = new Command(() => SearchForUsers());
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

				var userid = await _settingsService.GetUserId();
				_signedInUser = await _settingsService.GetUserName();
				CurrentUser = await _userService.GetUser(userid);
				AllUsers = await _userService.GetAllUsers();
				foreach (var user in AllUsers.Where(x => CurrentUser.Friends.Contains(x.Id)))
				{
					user.IsFriend = true;
				}

				SearchResult = AllUsers.Where(x => x.Name != _signedInUser).ToList();
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

		public void SearchForUsers()
		{
			if (SearchValue.Length >= 1)
			{
				try
				{
					SearchResult = AllUsers.Where(x => x.Name.ToLower().IndexOf(SearchValue.ToLower()) != -1).ToList();
				}
				catch (Exception ex)
				{
					var err = ex;
				}
			}
			else
			{
				SearchResult = AllUsers.Where(x => x.Name != _signedInUser).ToList();
			}
		}

		public async Task AmendFollower(int friendId)
		{
			var request = new UserRequest()
			{
				UserId = CurrentUser.Id.ToString(),
				FriendId = friendId.ToString(),
			};
			await _userService.AmendFollower(request);
			Init();
		}
	}
}
