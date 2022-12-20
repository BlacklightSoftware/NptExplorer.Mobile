using System.Threading.Tasks;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace NptExplorer.Mobile.ViewModels
{
	public class LandingViewModel : SignInBaseViewModel
	{
		public LandingViewModel(INavigationService navigationService,
			ISettingsService settingsService,
			IAuthService authService,
			IUserService userService,
			ILoggingService loggingService,
			IDialogService dialogService,
			ICacheService cacheService) : base(navigationService, settingsService, authService, userService, loggingService, dialogService, cacheService)
		{
			NavigateToLanguage = new Command(async () => await GoToLanguageScreen());
			OpenDevSettingsCommand = new Command(async () => await GoToDevSettings());
		}

		public ICommand NavigateToLanguage { get; set; }
		public ICommand OpenDevSettingsCommand { get; set; }

		public async Task GoToLanguageScreen()
		{
			await DetermineSignIn();
		}

		private async Task GoToDevSettings()
		{
#if DEBUG || STAGING || UAT
			await NavigationService.GoToAsync("devSettings");
#endif
		}
	}
}