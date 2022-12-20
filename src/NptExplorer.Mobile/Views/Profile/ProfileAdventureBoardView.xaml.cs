using System;
using NptExplorer.Mobile.ViewModels.Profile;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfileAdventureBoardView : PageBase<ProfileAdventureBoardViewModel>
	{
		public ProfileAdventureBoardView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			ViewModel.Init();
		}

		private void CheckRadioButton_Clicked(object sender, EventArgs e)
		{
			Device.InvokeOnMainThreadAsync(async () =>
			{
				await ViewModel.CheckRadioChange();
			});
		}
	}
}