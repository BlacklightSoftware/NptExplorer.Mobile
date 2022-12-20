using System.ComponentModel;
using NptExplorer.Mobile.ViewModels.Adventure;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Adventure
{
	[QueryProperty(nameof(ChallengeId), nameof(ChallengeId))]
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChallengeDetailsView : PageBase<ChallengeDetailsViewModel>
	{
		public string ChallengeId { get; set; }

		public ChallengeDetailsView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			ViewModel.Init(ChallengeId);
			ViewModel.PropertyChanged += Vm_PropertyChanged;
		}

		private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case nameof(ViewModel.DisplayPopup):
					ShowHidePopup(ViewModel.DisplayPopup);
					break;
			}
		}

		private void ShowHidePopup(bool show)
		{
			var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
			void Callback(double input) => MyDraggableView.HeightRequest = input;
			const uint rate = 300;
			const uint length = 500;

			if (show)
			{
				if (MyDraggableView.Height == 0)
				{
					const double startHeight = 0;
					var endHeight = mainDisplayInfo.Height / 4;
					Easing easing = Easing.CubicOut;
					MyDraggableView.Animate("anim", Callback, startHeight, endHeight, rate, length, easing);
				}
				else
				{
					var startHeight = mainDisplayInfo.Height / 4;
					const double endHeight = 0;
					Easing easing = Easing.SinOut;
					MyDraggableView.Animate("anim", Callback, startHeight, endHeight, rate, length, easing);
				}
			}
			else
			{
				var startHeight = mainDisplayInfo.Height / 4;
				const double endHeight = 0;
				Easing easing = Easing.SinOut;
				MyDraggableView.Animate("anim", Callback, startHeight, endHeight, rate, length, easing);
			}
		}
	}
}