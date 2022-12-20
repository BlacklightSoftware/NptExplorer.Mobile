using NptExplorer.Mobile.ViewModels.Explore;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Explore
{
	[QueryProperty(nameof(SearchedItemId), nameof(SearchedItemId))]
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExploreSearchedItemView : PageBase<ExploreSearchedItemViewModel>
	{
		public string SearchedItemId { get; set; }
		public ExploreSearchedItemView()
		{
			InitializeComponent();
		}


		protected override void OnAppearing()
		{
			base.OnAppearing();
			ViewModel.Init(SearchedItemId);
		}
	}
}