using NptExplorer.Mobile.ViewModels.Adventure;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Adventure
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChallengeFilterView : PageBase<ChallengeFilterViewModel>
	{
		public ChallengeFilterView()
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