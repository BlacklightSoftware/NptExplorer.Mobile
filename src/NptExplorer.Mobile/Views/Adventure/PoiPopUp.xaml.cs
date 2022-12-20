using System;
using NptExplorer.Mobile.Models;
using NptExplorer.Mobile.ViewModels.Adventure;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Adventure
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PoiPopUp : Popup<ChallengePoi>
	{
		private ChallengePoi Poi { get; set; }

		public PoiPopUp(ChallengePoi poi)
		{
			InitializeComponent();

			Poi = poi;
			var vm = (PoiPopUpViewModel)this.BindingContext;
			vm.Init(poi);
		}

		private void Yes_Tapped(object sender, EventArgs e)
		{
			Poi.InteractionResult = "yes";
			Dismiss(Poi);
		}

		private void No_Tapped(object sender, EventArgs e)
		{
			Poi.InteractionResult = "no";
			Dismiss(Poi);
		}

		private void Cancel_Tapped(object sender, EventArgs e)
		{
			Poi.InteractionResult = "";
			Dismiss(Poi);
		}
	}
}