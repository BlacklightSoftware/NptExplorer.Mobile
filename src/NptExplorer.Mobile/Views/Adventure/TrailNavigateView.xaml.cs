using NptExplorer.Mobile.ViewModels.Adventure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Adventure
{
	[QueryProperty(nameof(TrailId), nameof(TrailId))]
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TrailNavigateView : PageBase<TrailNavigateViewModel>
	{
		public string TrailId { get; set; }

		public TrailNavigateView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			ViewModel.Init(TrailId);
		}
	}
}