using System.Globalization;
using Microsoft.Identity.Client;
using NptExplorer.Mobile.Factory;
using NptExplorer.Mobile.Resources;
using NptExplorer.Mobile.Services.Abstract;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

[assembly: ExportFont("materialdesignicons.ttf", Alias = "IconFont")]
namespace NptExplorer.Mobile
{
	public partial class App : Application
    {
	    public static IPublicClientApplication AuthenticationClient { get; private set; }
	    public static object ParentWindow { get; set; }

	    public App()
        {
            Startup.Init();
            InitializeComponent();

            SetSelectedLanguage();
            SetStartUpPage();
        }

        private void SetSelectedLanguage()
        {
            var settingsService = LocatorFactory.Resolve<ISettingsService>();
            LocalizationResourceManager.Current.PropertyChanged += (_, _) => AppResources.Culture = LocalizationResourceManager.Current.CurrentCulture;
            LocalizationResourceManager.Current.Init(AppResources.ResourceManager);
            var language = settingsService.GetLanguage();
            if (string.IsNullOrEmpty(language))
            {
	            language = "en";
            }
            LocalizationResourceManager.Current.CurrentCulture = new CultureInfo(language);
        }

        private void SetStartUpPage()
        {
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
