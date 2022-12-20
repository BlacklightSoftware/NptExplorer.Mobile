using System;
using System.ComponentModel;
using NptExplorer.Mobile.Controls;
using NptExplorer.Mobile.ViewModels.Adventure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Adventure
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdventureHomeView : PageBase<AdventureHomeViewModel>
	{
		public AdventureHomeView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			ViewModel.Init();
		}

		private void Challenge_Clicked(object sender, EventArgs e)
		{
			var control = (IdButton)sender;
			var id = control.Id;
			Device.InvokeOnMainThreadAsync(async () =>
			{
				await ViewModel.GoToChallengeDetails(id);
			});
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