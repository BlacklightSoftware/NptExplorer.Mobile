using System;
using System.Threading.Tasks;
using FFImageLoading.Forms.Platform;
using Foundation;
using Microsoft.Identity.Client;
using NptExplorer.Mobile.Factory;
using NptExplorer.Mobile.Services.Abstract;
using UIKit;

namespace NptExplorer.Mobile.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            global::Xamarin.Forms.FormsMaterial.Init();
			Xamarin.FormsMaps.Init();

			CachedImageRenderer.Init();
			CachedImageRenderer.InitImageSourceHandler();

			AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
			TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

            LoadApplication(new App());

            App.ParentWindow = null;

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
	        AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(url);
	        return true;
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
