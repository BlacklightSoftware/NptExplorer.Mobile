using System;
using NptExplorer.Mobile.Models;
using NptExplorer.Mobile.ViewModels.Explore;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Explore
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocationPopUp : Popup<ExploreLocation>
	{
		private ExploreLocation Loc { get; set; }

		public LocationPopUp(ExploreLocation loc)
		{
			InitializeComponent();

			Loc = loc;
			var vm = (LocationPopUpViewModel)this.BindingContext;
			vm.Init(loc);
		}

		private void NavigateButton_OnClicked(object sender, EventArgs e)
		{
			Loc.InteractionResult = "navigate";
			Dismiss(Loc);
		}
	}
}