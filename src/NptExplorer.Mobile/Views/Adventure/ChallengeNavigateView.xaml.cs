using System.ComponentModel;
using NptExplorer.Mobile.ViewModels.Adventure;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Adventure
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChallengeNavigateView : PageBase<ChallengeNavigateViewModel>
	{
		public ChallengeNavigateView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			ViewModel.Init();
			ViewModel.PropertyChanged += Vm_PropertyChanged;
		}
		
		protected override async void OnDisappearing()
		{
			base.OnDisappearing();
			await ViewModel.StopListening();
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