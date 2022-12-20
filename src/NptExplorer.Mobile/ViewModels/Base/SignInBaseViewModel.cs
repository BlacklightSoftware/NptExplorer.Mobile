using System;
using System.Threading.Tasks;
using NptExplorer.Dto.Requests;
using NptExplorer.Mobile.Services.Abstract;

namespace NptExplorer.Mobile.ViewModels.Base
{

	public class SignInBaseViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		private readonly IAuthService _authService;
		private readonly IUserService _userService;
		private readonly ILoggingService _loggingService;
		private readonly IDialogService _dialogService;
		private readonly ICacheService _cacheService;

		public SignInBaseViewModel(INavigationService navigationService,
			ISettingsService settingsService,
			IAuthService authService,
			IUserService userService,
			ILoggingService loggingService,
			IDialogService dialogService,
			ICacheService cacheService) : base(navigationService)
		{
			_settingsService = settingsService;
			_authService = authService;
			_userService = userService;
			_loggingService = loggingService;
			_dialogService = dialogService;
			_cacheService = cacheService;
		}

		public async Task DetermineSignIn()
		{
			_cacheService.ClearCache();
			_settingsService.ClearAllFilters();

			var language = _settingsService.GetLanguage();
			if (string.IsNullOrEmpty(language))
			{
				await NavigationService.GoToAsync("language");
				return;
			}

			var userName = await _settingsService.GetUserName();
			var userId = await _settingsService.GetUserId();

			var guest = await _settingsService.GetGuest();
			if (!string.IsNullOrEmpty(userName) && guest)
			{
				if (userId == null)
				{
					// handle guest accounts after release
					userId = $"g-{Guid.NewGuid()}";
					await _settingsService.SetUserId(userId);
					var userData = new UserRequest() { UserId = userId, Name = userName };
					await _userService.AddUser(userData);
					await _settingsService.SetRegistered(true);
				}
				await NavigationService.GoToAsync("///explore");
				return;
			}

			var accepted = await _settingsService.GetAcceptedCriteria();
			if (!accepted)
			{
				await NavigationService.GoToAsync("about");
				return;
			}

			var registered = await _settingsService.GetRegistered();
			if (!registered)
			{
				await NavigationService.GoToAsync("signUp");
				return;
			}

			if (await _authService.SignInAsync())
			{
				try
				{
					userName = await _settingsService.GetUserName();
					userId = await _settingsService.GetUserId();
					var userData = new UserRequest() { UserId = userId, Name = userName };
					await _userService.AddUser(userData);
					await _settingsService.SetRegistered(true);
				}
				catch (Exception ex)
				{
					_loggingService.Error(ex);
					await _dialogService.ShowAlertAsync(Resources.AppResources.Error_ErrorTitle,
						Resources.AppResources.Error_ErrorTitle, Resources.AppResources.Error_OkButton);
					return;
				}

				await NavigationService.GoToAsync("///explore");
			}
		}
	}
}