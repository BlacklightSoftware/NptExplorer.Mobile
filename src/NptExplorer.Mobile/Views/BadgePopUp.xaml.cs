using System;
using NptExplorer.Core.Enums;
using NptExplorer.Mobile.ViewModels;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BadgePopUp : Popup<BadgeTypes>
	{
		private readonly BadgeTypes _badge;

		public BadgePopUp(BadgeTypes badge)
		{
			InitializeComponent();

			var vm = (BadgePopUpViewModel)this.BindingContext;
			_badge = badge;
			vm.Init(badge);
		}

		private void Close_Tapped(object sender, EventArgs e)
		{
			Dismiss(_badge);
		}
	}
}