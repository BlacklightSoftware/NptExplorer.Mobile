using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using NptExplorer.Dto.Requests;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace NptExplorer.Mobile.ViewModels
{
	public class LanguageViewModel : SignInBaseViewModel
    {
        private readonly ISettingsService _settingsService;

        public LanguageViewModel(INavigationService navigationService,
	        ISettingsService settingsService,
	        IAuthService authService,
	        IUserService userService,
	        ILoggingService loggingService,
	        IDialogService dialogService, 
	        ICacheService cacheService) : base(navigationService, settingsService, authService, userService, loggingService, dialogService, cacheService)
        {
            _settingsService = settingsService;

            NavigateWithWelsh = new Command(WelshOption);
            NavigateWithEnglish = new Command(EnglishOption);
        }

        public ICommand NavigateWithEnglish { get; set; } 
        public ICommand NavigateWithWelsh { get; set; }

        public async void EnglishOption()
        {
            _settingsService.SetLanguage("en");
            LocalizationResourceManager.Current.CurrentCulture = new CultureInfo("en");
            await NavigateNext();
        }

        public async void WelshOption()
        {
            _settingsService.SetLanguage("cy");
            LocalizationResourceManager.Current.CurrentCulture = new CultureInfo("cy");
            await NavigateNext();
        }

        private async Task NavigateNext()
        {
	        var acceptedCriteria = await _settingsService.GetAcceptedCriteria();
	        if (acceptedCriteria)
	        {
		        await DetermineSignIn();
	        }
	        else
	        {
		        await NavigationService.GoToAsync("about");
	        }
        }
    }
}