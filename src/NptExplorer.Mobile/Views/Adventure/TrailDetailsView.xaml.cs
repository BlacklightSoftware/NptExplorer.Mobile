using NptExplorer.Mobile.ViewModels.Adventure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Adventure
{
	[QueryProperty(nameof(TrailId), nameof(TrailId))]
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TrailDetailsView : PageBase<TrailDetailsViewModel>
	{
		public string TrailId { get; set; }

		public TrailDetailsView()
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