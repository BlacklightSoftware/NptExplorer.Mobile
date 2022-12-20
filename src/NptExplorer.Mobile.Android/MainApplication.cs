using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;

namespace NptExplorer.Mobile.Droid
{
	[Application]
	public class MainApplication : Application
	{
		public MainApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
		{
		}

		public override void StartActivity(Intent intent)
		{
			// Hack to prevent split screen issues, needs to happen here
			// as disabling in attributes causes app to crash
			if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
			{
				intent.RemoveFlags(ActivityFlags.LaunchAdjacent);
			}

			base.StartActivity(intent);
		}
	}
}