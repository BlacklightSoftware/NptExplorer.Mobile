using NptExplorer.Mobile.ViewModels.Profile;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileView : PageBase<ProfileViewModel>
    {
        public ProfileView()
        {
            InitializeComponent();
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();
			ViewModel.Init();
		}
    }
}