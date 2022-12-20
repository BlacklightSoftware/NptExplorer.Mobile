using System.Globalization;
using System.Windows.Input;
using NptExplorer.Mobile.Constants;
using NptExplorer.Mobile.Resources;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NptExplorer.Mobile.ViewModels
{
	public class SettingViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		private readonly IAuthService _authService;
		private readonly IDialogService _dialogService;
		private readonly IUserService _userService;
		private string _selectedLanguage;
		private string _allowFollowers;
		private string _selectedUnits;

		public ICommand SaveSettingsCommand { get; set; }
		public ICommand GoToTerms { get; set; }
		public ICommand GoToPrivacy { get; set; }
		public ICommand GoToRatings { get; set; }
		public ICommand LogOut { get; set; }
		public ICommand DeleteAccountCommand { get; set; }

		#region Public properties 
		public string SelectedLanguage
		{
			get => _selectedLanguage;
			set
			{
				_selectedLanguage = value;
				RaisePropertyChanged(() => SelectedLanguage);
			}
		}
		public string AllowFollowers
		{
			get => _allowFollowers;
			set
			{
				_allowFollowers = value;
				RaisePropertyChanged(() => AllowFollowers);
			}
		}
		public string SelectedUnits
		{
			get => _selectedUnits;
			set
			{
				_selectedUnits = value;
				RaisePropertyChanged(() => SelectedUnits);
			}
		}

		#endregion

		public SettingViewModel(
			INavigationService navigationService, 
			ISettingsService settingsService, 
			IAuthService authService,
			IDialogService dialogService,
			IUserService userService) : base(navigationService)
		{
			_settingsService = settingsService;
			_authService = authService;
			_dialogService = dialogService;
			_userService = userService;

			SaveSettingsCommand = new Command(SaveSettings);
			GoToTerms = new Command(NavigateToTerms);
			GoToPrivacy = new Command(NavigateToPrivacy);
			GoToRatings = new Command(NavigateToRatings);
			LogOut = new Command(LogOutAsync);
			DeleteAccountCommand = new Command(DeleteAccountAsync);
		}

		public void Init()
		{
			if (IsBusy)
			{
				return;
			}

			StartBusy();

			SelectedLanguage = _settingsService.GetLanguage();
			SelectedUnits = _settingsService.GetUnits();
			AllowFollowers = _settingsService.GetFollowers();

			StopBusy();
		}

		public async void SaveSettings()
		{
			_settingsService.SetLanguage(SelectedLanguage);
			_settingsService.SetUnits(SelectedUnits);
			_settingsService.SetFollowers(AllowFollowers);
			LocalizationResourceManager.Current.CurrentCulture = new CultureInfo(SelectedLanguage);

			await NavigationService.GoBackAsync();
		}

		public void NavigateToTerms()
		{
			if (DeviceInfo.Platform == DevicePlatform.Android)
			{
				GoToUrl(SystemConstants.TermsAndroid);
			}
			else if (DeviceInfo.Platform == DevicePlatform.iOS)
			{
				GoToUrl(SystemConstants.TermsIos);
			}
		}

		public void NavigateToPrivacy()
		{
			if (DeviceInfo.Platform == DevicePlatform.Android)
			{
				GoToUrl(SystemConstants.PolicyAndroid);
			}
			else if (DeviceInfo.Platform == DevicePlatform.iOS)
			{
				GoToUrl(SystemConstants.PolicyIos);
			}
		}

		public async void NavigateToRatings()
		{
			await NavigationService.GoToAsync("/rating");
		}

		public async void LogOutAsync()
		{
			await _authService.SignOutAsync();
			await NavigationService.GoToAsync("/landing");
		}
		
		private async void DeleteAccountAsync()
		{
			var response = await _dialogService.ConfirmAsync(AppResources.DeleteAccount_Description, AppResources.DeleteAccount_Title, AppResources.Yes_Option, AppResources.No_Option);
			if (!response)
			{
				return;
			}

			StartBusy();
			var result = await _userService.DeleteUser();
			StopBusy();

			if (!result)
			{
				await _dialogService.ShowAlertAsync(AppResources.DeleteAccountError, AppResources.DeleteAccount_Title,
					AppResources.Error_OkButton);
				return;
			}

			await _settingsService.SetRegistered(false);
			await _dialogService.ShowAlertAsync(AppResources.DeleteAccountConfirmation,
				AppResources.DeleteAccount_Title,
				AppResources.Error_OkButton);
			LogOutAsync();
		}
	}
}