using System.Windows.Input;
using NptExplorer.Mobile.Constants;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NptExplorer.Mobile.ViewModels.Help
{
	public class PrivacyPolicyViewModel : ViewModelBase
	{
		public ICommand GoToPolicy { get; set; }

		public PrivacyPolicyViewModel(INavigationService navigationService) : base(navigationService)
		{
			GoToPolicy = new Command(NavigateToPolicy);
		}

		public void NavigateToPolicy()
		{
			if (DeviceInfo.Platform == DevicePlatform.Android)
			{
				GoToUrl(SystemConstants.PolicyAndroid);
			}
			else if (DeviceInfo.Platform == DevicePlatform.iOS)
			{
				GoToUrl(SystemConstants.PolicyIos);
			}
		}
	}
}