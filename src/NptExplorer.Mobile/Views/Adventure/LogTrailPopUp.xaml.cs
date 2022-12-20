using System;
using NptExplorer.Mobile.Models;
using NptExplorer.Mobile.ViewModels.Adventure;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Adventure
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogTrailPopUp : Popup<ChallengeTrail>
	{
		private ChallengeTrail Trail { get; set; }

		public LogTrailPopUp(ChallengeTrail trail)
		{
			InitializeComponent();

			Trail = trail;
			var vm = (LogTrailPopUpViewModel)this.BindingContext;
			vm.Init(trail);
		}

		private void TrailCompleted(object sender, EventArgs e)
		{
			Trail.InteractionResult = "yes";
			Dismiss(Trail);
		}

		private void TrailNotCompleted(object sender, EventArgs e)
		{
			Trail.InteractionResult = "no";
			Dismiss(Trail);
		}

		private void Cancel_Tapped(object sender, EventArgs e)
		{
			Trail.InteractionResult = "";
			Dismiss(Trail);
		}
	}
}