using System.Threading.Tasks;
using System.Windows.Input;
using NptExplorer.Mobile.Resources;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using Xamarin.Forms;

namespace NptExplorer.Mobile.ViewModels
{
	public class RatingViewModel : ViewModelBase
    {
        public RatingViewModel(INavigationService navigationService):base(navigationService)
        {
            ExecutePreviousPage = new Command(async () => await previousPage());
            ExecuteSubmitRating = new Command(() => submitRating());
    }

        public ICommand ExecutePreviousPage { get; set; }
        public ICommand ExecuteSubmitRating { get; set; }

        public async Task previousPage()
        {
            await NavigationService.GoBackAsync();
        }

        public async void submitRating()
        {
            var isAppStore = await Application.Current.MainPage.DisplayAlert(AppResources.Rating_StorePopupTitle, AppResources.Rating_StorePopupMessage, AppResources.Rating_StorePopupAccept, AppResources.Rating_StorePopupCancel);

            if (isAppStore)
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                     // Navigate to the Play Store
                     //Device.OpenUri(new Uri(""));
                } 
                else
                {
                    // Navigate to the App Store
                    //Device.OpenUri(new Uri(""));
                }
            }

            var isShare = await Application.Current.MainPage.DisplayAlert(AppResources.Rating_SubmittedTitle, AppResources.Rating_SubmittedMessage, AppResources.Rating_SubmittedShare, AppResources.Rating_SubmittedClose);
        }
    }
}