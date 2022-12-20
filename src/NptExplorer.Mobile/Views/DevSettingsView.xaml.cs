using NptExplorer.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DevSettingsView : PageBase<DevSettingsViewModel>
	{
		public DevSettingsView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			_ = ViewModel.Init();
		}
	}
}