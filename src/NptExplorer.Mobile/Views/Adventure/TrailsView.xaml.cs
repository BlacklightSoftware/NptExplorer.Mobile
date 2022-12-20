using System;
using NptExplorer.Mobile.Controls;
using NptExplorer.Mobile.ViewModels.Adventure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Adventure
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TrailsView : PageBase<TrailsViewModel>
	{
		public TrailsView()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Init();
        }

        private void Trail_Clicked(object sender, EventArgs e)
        {
	        var control = (IdButton)sender;
	        var id = control.Id;
	        Device.InvokeOnMainThreadAsync(async () =>
	        {
		        await ViewModel.GoToTrailDetails(id);
	        });
        }
	}
}