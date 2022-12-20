using System.Windows.Input;
using NptExplorer.Mobile.Constants;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.Validations;
using NptExplorer.Mobile.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NptExplorer.Mobile.ViewModels
{
	public class AboutViewModel : SignInBaseViewModel
	{
		private readonly ISettingsService _settingsService;
		private ValidatableObject<bool> _acceptedCriteria;

		public ICommand ExecuteNext { get; set; }
		public ICommand GoToTerms { get; set; }
		public ICommand GoToPrivacy { get; set; }

		public ValidatableObject<bool> AcceptedCriteria
		{
			get => _acceptedCriteria;
			set
			{
				_acceptedCriteria = value;
				RaisePropertyChanged(() => AcceptedCriteria);
			}
		}

		public AboutViewModel(INavigationService navigationService,
			ISettingsService settingsService,
			IAuthService authService,
			IUserService userService,
			ILoggingService loggingService,
			IDialogService dialogService,
			ICacheService cacheService) : base(navigationService, settingsService, authService, userService, loggingService, dialogService, cacheService)
		{
			_settingsService = settingsService;

			_acceptedCriteria = new ValidatableObject<bool>();
			_acceptedCriteria.Validations.Add(new IsTrueRule<bool> { ValidationMessage = "You must accept the T&C's and Privacy Policy" });

			ExecuteNext = new Command(NextAsync);
			GoToTerms = new Command(NavigateToTerms);
			GoToPrivacy = new Command(NavigateToPrivacy);
		}

		public async void NextAsync()
		{
			if (!ValidateTerms())
			{
				return;
			}

			await _settingsService.SetAcceptedCriteria();

			await DetermineSignIn();
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

		public async void NavigateToPrivacy()
		{
			await NavigationService.GoToAsync("/helpPrivacy");
		}

		private bool ValidateTerms()
		{
			return _acceptedCriteria.Validate();
		}
	}
}