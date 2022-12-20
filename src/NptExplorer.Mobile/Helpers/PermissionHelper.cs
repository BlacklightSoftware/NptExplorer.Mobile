using System.Threading.Tasks;
using NptExplorer.Mobile.Factory;
using NptExplorer.Mobile.Resources;
using NptExplorer.Mobile.Services.Abstract;
using Xamarin.Essentials;

namespace NptExplorer.Mobile.Helpers
{

	public static class PermissionHelper
	{
		public static async Task<PermissionStatus> CheckAndRequestLocationPermission()
		{
			var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

			if (status == PermissionStatus.Granted)
			{
				return status;
			}

			var dialogService = LocatorFactory.Resolve<IDialogService>();

			if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
			{
				// Prompt the user to turn on in settings
				// On iOS once a permission has been denied it may not be requested again from the application
				await dialogService.ShowAlertAsync(AppResources.LocationServiceIosPermissionDetails,
					AppResources.LocationServiceIosPermissionTitle, AppResources.Error_OkButton);
				return status;
			}

			if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
			{
				// Prompt the user with additional information as to why the permission is needed
				await dialogService.ShowAlertAsync(AppResources.LocationServicePermissionDetails,
					AppResources.LocationServicePermissionTitle, AppResources.Error_OkButton);
			}

			status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

			return status;
		}
	}
}