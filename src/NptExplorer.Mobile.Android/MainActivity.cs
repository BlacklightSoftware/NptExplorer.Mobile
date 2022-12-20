using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using FFImageLoading.Forms.Platform;
using Microsoft.Identity.Client;
using NptExplorer.Mobile.Factory;
using NptExplorer.Mobile.Services.Abstract;
using Plugin.CurrentActivity;

namespace NptExplorer.Mobile.Droid
{
	[Activity(Label = "NptExplorer.Mobile",
		Theme = "@style/MainTheme",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize |
							   ConfigChanges.UiMode |
							   ConfigChanges.ScreenLayout |
							   ConfigChanges.SmallestScreenSize |
							   ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		private const int RequestLocationId = 0;
		private readonly string[] LocationPermissons =
		{
			Manifest.Permission.AccessFineLocation,
			Manifest.Permission.AccessCoarseLocation
		};

		protected override void OnStart()
		{
			base.OnStart();

			if ((int)Build.VERSION.SdkInt >= 23)
			{
				if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
				{
					RequestPermissions(LocationPermissons, RequestLocationId);
				}
				else
				{
					// Permission already granted - display a message.
				}
			}
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;

			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);

			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			UserDialogs.Init(this);

			CachedImageRenderer.Init(enableFastRenderer: true);
			CachedImageRenderer.InitImageViewHandler();

			Xamarin.FormsMaps.Init(this, savedInstanceState);
			CrossCurrentActivity.Current.Init(this, savedInstanceState);

			AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
			Xamarin.Essentials.VersionTracking.Track();

			LoadApplication(new App());
			App.ParentWindow = this;
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
		{
			var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", e.Exception);
			LogUnhandledException(newExc);
		}

		private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			var newExc = new Exception("CurrentDomainOnUnhandledException", e.ExceptionObject as Exception);
			LogUnhandledException(newExc);
		}

		private static void LogUnhandledException(Exception exception)
		{
			try
			{
				var logger = LocatorFactory.Resolve<ILoggingService>();
				logger.Error(exception);
			}
			catch
			{
				// suppress logging exceptions
			}
		}
	}
}