using System;
using System.Collections.Generic;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using NptExplorer.Mobile.Constants;
using NptExplorer.Mobile.Services.Abstract;
using Xamarin.Essentials;

namespace NptExplorer.Mobile.Services.Concrete{

public class AppCenterLoggingService : ILoggingService
{
	public AppCenterLoggingService()
	{
		AppCenter.LogLevel = LogLevel.Verbose;
		AppCenter.Start(
			$"ios={SystemConstants.AppCenterIosKey}​;android={SystemConstants.AppCenterAndroidKey}",
			typeof(Analytics),
			typeof(Crashes));
	}

	public void Debug(string message)
	{
		Analytics.TrackEvent(nameof(Debug), new Dictionary<string, string> { { nameof(message), message }, { "Platform", DeviceInfo.Platform.ToString() } });
	}

	public void Warning(string message)
	{
		Analytics.TrackEvent(nameof(Warning), new Dictionary<string, string> { { nameof(message), message },  { "Platform", DeviceInfo.Platform.ToString() } });
	}

	public void Error(Exception exception)
	{
		Crashes.TrackError(exception, new Dictionary<string, string> { { "Platform", DeviceInfo.Platform.ToString() } });
	}

	public void Track(string message)
	{
		Analytics.TrackEvent(message, new Dictionary<string, string> { { "Platform", DeviceInfo.Platform.ToString() } });
	}
}
}