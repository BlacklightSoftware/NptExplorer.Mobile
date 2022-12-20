using System;
using NptExplorer.Mobile.Controls;
using NptExplorer.Mobile.ViewModels.Explore;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Explore
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExploreMapView : PageBase<ExploreMapViewModel>
	{
		public ExploreMapView()
		{
			InitializeComponent();
		}

		private void SearchedItem_Clicked(object sender, EventArgs e)
		{
			var control = (IdButton)sender;
			var id = control.Id;
			Device.InvokeOnMainThreadAsync(async () =>
			{
				await ViewModel.GoToSearchedItem(id);
			});
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			ViewModel.Init();
		}
	}
}