using System.Threading.Tasks;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;
using NptExplorer.Dto.Requests;
using System;
using NptExplorer.Mobile.Resources;
using NptExplorer.Mobile.Validations;

namespace NptExplorer.Mobile.ViewModels
{
	public class SignUpViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		private readonly IAuthService _authService;
		private readonly IUserService _userService;
		private readonly ILoggingService _loggingService;
		private readonly IDialogService _dialogService;
		private bool _showOptions;
		private bool _showGuestSignUp;
		private ValidatableObject<string> _displayName;

		public bool ShowOptions
		{
			get => _showOptions;
			set
			{
				_showOptions = value;
				RaisePropertyChanged(() => ShowOptions);
			}
		}
		public bool ShowGuestSignUp
		{
			get => _showGuestSignUp;
			set
			{
				_showGuestSignUp = value;
				RaisePropertyChanged(() => ShowGuestSignUp);
			}
		}
		public ValidatableObject<string> DisplayName
		{
			get => _displayName;
			set
			{
				_displayName = value;
				RaisePropertyChanged(() => DisplayName);
			}
		}

		public ICommand CreateAccountCommand { get; set; }
		public ICommand ContinueAsGuestCommand { get; set; }
		public ICommand RegisterAsGuestCommand { get; set; }
		public ICommand SignInCommand { get; set; }

		public SignUpViewModel(INavigationService navigationService,
			ISettingsService settingsService,
			IAuthService authService,
			IUserService userService,
			ILoggingService loggingService,
			IDialogService dialogService) : base(navigationService)
		{
			_settingsService = settingsService;
			_authService = authService;
			_userService = userService;
			_loggingService = loggingService;
			_dialogService = dialogService;

			_displayName = new ValidatableObject<string>();
			_displayName.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = AppResources.Guest_UsernamePlaceholder });
			_displayName.Validations.Add(new IsValidUserName<string>() { ValidationMessage = AppResources.Guest_UsernameDescription });

			CreateAccountCommand = new Command(async () => await SignInAsync());
			ContinueAsGuestCommand = new Command(async () => await ContinueAsGuestAsync());
			RegisterAsGuestCommand = new Command(async () => await RegisterAsGuestAsync());
			SignInCommand = new Command(async () => await SignInAsync());
		}

		public void Init()
		{
			ShowOptions = true;
			ShowGuestSignUp = false;
		}

		private Task ContinueAsGuestAsync()
		{
			ShowOptions = false;
			ShowGuestSignUp = true;
			return Task.CompletedTask;
		}

		private async Task RegisterAsGuestAsync()
		{
			try
			{
				if (!ValidateUserName())
				{
					return;
				}

				var userId = $"g-{Guid.NewGuid()}";
				await _settingsService.SetUserId(userId);
				await _settingsService.SetUserName(DisplayName.Value);
				await _settingsService.SetGuest(true);
				var userData = new UserRequest() { UserId = userId, Name = DisplayName.Value };
				await _userService.AddUser(userData);
				await _settingsService.SetRegistered(true);
			}
			catch (Exception ex)
			{
				_loggingService.Error(ex);
				await _dialogService.ShowAlertAsync(AppResources.Error_ErrorTitle,
					AppResources.Error_ErrorTitle, AppResources.Error_OkButton);
				await NavigationService.GoToAsync("/landing");
			}
			await NavigationService.GoToAsync("///explore");
		}

		private bool ValidateUserName()
		{
			return _displayName.Validate();
		}

		private async Task SignInAsync()
		{
			if (await _authService.SignInAsync())
			{
				try
				{
					var registered = await _settingsService.GetRegistered();

					if (!registered)
					{
						var userName = await _settingsService.GetUserName();
						var userId = await _settingsService.GetUserId();
						var userData = new UserRequest() { UserId = userId, Name = userName };
						await _userService.AddUser(userData);
						await _settingsService.SetRegistered(true);
					}
				}
				catch (Exception ex)
				{
					_loggingService.Error(ex);
					await _dialogService.ShowAlertAsync(AppResources.Error_ErrorTitle,
						AppResources.Error_ErrorTitle, AppResources.Error_OkButton);
					await NavigationService.GoToAsync("/landing");
				}
				await NavigationService.GoToAsync("///explore");
			}
		}
	}
}