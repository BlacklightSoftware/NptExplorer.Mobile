using System;
using NptExplorer.Mobile.Controls;
using NptExplorer.Mobile.ViewModels.Profile;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfileFriendsView : PageBase<ProfileFriendsViewModel>
	{
		public ProfileFriendsView()
		{
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			ViewModel.Init();
		}

		private void FollowButton_Clicked(object sender, EventArgs e)
		{
			var control = (IdButton)sender;
			var id = control.Id;
			Device.InvokeOnMainThreadAsync(async () =>
			{
				await ViewModel.AmendFollower(id);
			});
		}
	}
}