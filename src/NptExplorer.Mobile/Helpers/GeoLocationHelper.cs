using System;
using System.Threading;
using System.Threading.Tasks;
using NptExplorer.Core.Models;
using Xamarin.Essentials;

namespace NptExplorer.Mobile.Helpers
{
	public static class GeoLocationHelper
	{
		private static CancellationTokenSource _cts;

		public static double KilometerToMeter(double km)
		{
			var meter = km * 1000;

			return meter;
		}

		public static async Task<LatLong> GetPosition()
		{
			var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
			if (status != PermissionStatus.Granted)
			{
				return null;
			}
			
			var request = new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(5));
			_cts = new CancellationTokenSource();
			var position = await Geolocation.GetLocationAsync(request, _cts.Token);
			return position != null
				? new LatLong(position.Latitude, position.Longitude)
				: null;
		}

		public static async Task<LatLong> GetLastPosition()
		{
			var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
			if (status != PermissionStatus.Granted)
			{
				return null;
			}
			
			var position = await Geolocation.GetLastKnownLocationAsync();
			return position != null
				? new LatLong(position.Latitude, position.Longitude)
				: await GetPosition();
		}

		public static Task<double> CalculateDistance(LatLong startPosition, LatLong endPosition, DistanceUnits unit = DistanceUnits.Kilometers)
		{
			var distance = Xamarin.Essentials.Location.CalculateDistance(startPosition.Latitude,
				startPosition.Longitude, endPosition.Latitude, endPosition.Longitude, unit);
			return Task.FromResult(distance);
		}
	}
}