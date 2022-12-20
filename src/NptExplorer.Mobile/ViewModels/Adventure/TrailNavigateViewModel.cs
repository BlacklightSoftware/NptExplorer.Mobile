using System;
using System.Linq;
using NptExplorer.Core.Models;
using NptExplorer.Mobile.Controls;
using NptExplorer.Mobile.Helpers;
using NptExplorer.Mobile.Resources;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace NptExplorer.Mobile.ViewModels.Adventure
{
	public class TrailNavigateViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		private readonly IAdventureService _adventureService;
		private readonly IDialogService _dialogService;
		private readonly ILoggingService _loggingService;
		private TrailRoute _trail;
		private CustomMap _trailMap;

		public CustomMap TrailMap
		{
			get => _trailMap;
			set
			{
				_trailMap = value;
				RaisePropertyChanged(() => TrailMap);
			}
		}

		public TrailRoute Trail
		{
			get => _trail;
			set
			{
				_trail = value;
				RaisePropertyChanged(() => Trail);
			}
		}

		public TrailNavigateViewModel(INavigationService navigationService,
			ISettingsService settingsService,
			IAdventureService adventureService,
			IDialogService dialogService,
			ILoggingService loggingService) : base(navigationService)
		{
			_settingsService = settingsService;
			_adventureService = adventureService;
			_dialogService = dialogService;
			_loggingService = loggingService;
		}

		public async void Init(string id)
		{
			if (IsBusy)
			{
				return;
			}

			StartBusy();

			try
			{
				ScreenTitle = Labels.GetHelloLabel(await _settingsService.GetUserName());

				if (int.TryParse(id, out var trailId))
				{
					Trail = await _adventureService.GetTrailRoute(trailId);

					var currentLocation = await GeoLocationHelper.GetLastPosition();
					var customMap = new CustomMap
					{
						MapType = MapType.Street,
						HasScrollEnabled = true,
						HasZoomEnabled = true,
						IsShowingUser = true
					};

					var position = new Position(currentLocation.Latitude, currentLocation.Longitude);

					customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(0.5)));
					BuildRoute(customMap);
					TrailMap = customMap;
				}
				else
				{
					await _dialogService.ShowAlertAsync(AppResources.Error_MapDetail, AppResources.Error_MapTitle,
						AppResources.Error_MapOkButton);
					await NavigationService.GoBackAsync();
				}
			}
			catch (Exception ex)
			{
				_loggingService.Error(ex);
				await _dialogService.ShowAlertAsync(AppResources.Error_MapDetail, AppResources.Error_MapTitle,
					AppResources.Error_MapOkButton);
				await NavigationService.GoBackAsync();
			}
			finally
			{
				StopBusy();
			}
		}

		private void BuildRoute(CustomMap map)
		{
			if (Trail.Route != null && !Trail.Route.Any())
			{
				throw new Exception("No route associated with Trail");
			}

			foreach (var group in Trail.Route)
			{
				var polyline = new Polyline { StrokeColor = ColorConverters.FromHex("#1DA1F2"), StrokeWidth = 8 };
				foreach (var point in group)
				{
					polyline.Geopath.Add(new Position(point.Latitude, point.Longitude));
				}
				map.MapElements.Add(polyline);
			}
		}
	}
}