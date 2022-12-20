using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using NptExplorer.Core.Enums;
using NptExplorer.Core.Extensions;
using NptExplorer.Core.Models;
using NptExplorer.Dto.Requests;
using NptExplorer.Mobile.Controls;
using NptExplorer.Mobile.Helpers;
using NptExplorer.Mobile.Models;
using NptExplorer.Mobile.Resources;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using NptExplorer.Mobile.Views.Adventure;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Position = Xamarin.Forms.Maps.Position;

namespace NptExplorer.Mobile.ViewModels.Adventure
{
	public class ChallengeNavigateViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		private readonly IDialogService _dialogService;
		private readonly ILoggingService _loggingService;
		private readonly ICacheService _cacheService;
		private readonly IAdventureService _adventureService;

		private ChallengePoi _selectedPoi;
		private CustomMap _poiMap;
		private readonly string _culture;
		private string _distanceToPoi;
		private bool _displayPopup;
		private bool _checkedIn;
		private string _userId;
		private List<CheckIn> _checkIns;

		#region Public properties
		public CustomMap PoiMap
		{
			get => _poiMap;
			set
			{
				_poiMap = value;
				RaisePropertyChanged(() => PoiMap);
			}
		}
		public ChallengePoi SelectedPoi
		{
			get => _selectedPoi;
			set
			{
				_selectedPoi = value;
				RaisePropertyChanged(() => SelectedPoi);
			}
		}
		public string DistanceToPoi
		{
			get => _distanceToPoi;
			set
			{
				_distanceToPoi = value;
				RaisePropertyChanged(() => DistanceToPoi);
			}
		}
		public bool DisplayPopup
		{
			get => _displayPopup;
			set
			{
				_displayPopup = value;
				RaisePropertyChanged(() => DisplayPopup);
			}
		}
		public bool CheckedIn
		{
			get => _checkedIn;
			set
			{
				_checkedIn = value;
				RaisePropertyChanged(() => CheckedIn);
			}
		}
		#endregion

		public ICommand CloseDrawerCommand { get; }
		public ICommand LogCommand { get; }
		public ICommand CheckInCommand { get; }

		public ChallengeNavigateViewModel(INavigationService navigationService,
			ISettingsService settingsService,
			IDialogService dialogService,
			ILoggingService loggingService,
			ICacheService cacheService,
			IAdventureService adventureService) : base(navigationService)
		{
			_settingsService = settingsService;
			_dialogService = dialogService;
			_loggingService = loggingService;
			_cacheService = cacheService;
			_adventureService = adventureService;

			_culture = _settingsService.GetLanguage();
			CloseDrawerCommand = new Command(() => DisplayPopup = false);
			LogCommand = new Command(async () => await LogAsync());
			CheckInCommand = new Command(async () => await CheckInAsync());
		}

		public async void Init()
		{
			if (IsBusy)
			{
				return;
			}

			StartBusy();

			try
			{
				ScreenTitle = Labels.GetHelloLabel(await _settingsService.GetUserName());
				_userId = await _settingsService.GetUserId();
				_checkIns = _cacheService.GetCheckIns();

				var poi = _cacheService.GetSelectedPoi();
				if (poi == null)
				{
					await _dialogService.ShowAlertAsync(AppResources.Error_MapDetail, AppResources.Error_MapTitle,
						AppResources.Error_MapOkButton);
					return;
				}

				SelectedPoi = poi;
				CheckedIn = _checkIns.Any(x => x.BadgeId == SelectedPoi.BadgeId);

				var currentLocation = await GeoLocationHelper.GetLastPosition();
				var customMap = new CustomMap
				{
					MapType = MapType.Street,
					HasScrollEnabled = true,
					HasZoomEnabled = true,
					IsShowingUser = true
				};

				var position = new Position(currentLocation.Latitude, currentLocation.Longitude);

				var pin = BuildPin(SelectedPoi);
				customMap.Pins.Add(pin);

				customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(0.5)));
				PoiMap = customMap;
				await StartTrackingLocation();
			}
			catch (Exception ex)
			{
				_loggingService.Error(ex);
				await _dialogService.ShowAlertAsync(AppResources.Error_MapDetail, AppResources.Error_MapTitle,
					AppResources.Error_MapOkButton);
			}
			finally
			{
				StopBusy();
			}
		}

		private CustomPin BuildPin(PointOfInterest poi)
		{
			var badge = EnumExtension.GetDescription<BadgeTypes>(_culture, (BadgeTypes)poi.BadgeTypeId);
			var pin = new CustomPin
			{
				ItemId = poi.Id,
				Name = poi.Label,
				Label = poi.Label,
				BadgeLabel = poi.Collected ? $"1 {badge} collected" : $"Collect a {badge} badge",
				Address = string.Empty,
				Type = PinType.Place,
				IconType = (BadgeTypes)poi.BadgeTypeId,
				Position = new Position(poi.Position.Latitude, poi.Position.Longitude)
			};
			if (Device.RuntimePlatform == Device.Android)
			{
				pin.InfoWindowClicked += async (s, args) =>
				{
					await ShowChallengeDetailsAsync();
				};
			}
			else
			{
				pin.CustomInfoWindowClicked += async (s) =>
				{
					await ShowChallengeDetailsAsync();
				};
			}

			return pin;
		}

		private void UpdatePin(PointOfInterest poi)
		{
			var pin = BuildPin(poi);
			_poiMap.Pins.Clear();
			_poiMap.Pins.Add(pin);
		}

		private async Task StartTrackingLocation()
		{
			if (CrossGeolocator.Current.IsListening)
			{
				return;
			}

			await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true);

			CrossGeolocator.Current.PositionChanged += UpdateDistance;
		}

		public async Task StopListening()
		{
			if (!CrossGeolocator.Current.IsListening)
			{
				return;
			}

			await CrossGeolocator.Current.StopListeningAsync();

			CrossGeolocator.Current.PositionChanged -= UpdateDistance;
		}

		private void UpdateDistance(object sender, PositionEventArgs e)
		{
			var position = new LatLong(e.Position.Latitude, e.Position.Longitude);
			var distance = GeoLocationHelper.CalculateDistance(position, SelectedPoi.Position).Result;
			DistanceToPoi = $"{AppResources.Distance_Label}: {distance:N2} Km";		}

		private Task ShowChallengeDetailsAsync()
		{
			DisplayPopup = true;
			return Task.CompletedTask;
		}

		private async Task CheckInAsync()
		{
			_cacheService.SetCheckIn(SelectedPoi.BadgeId);
			_checkIns = _cacheService.GetCheckIns();
			CheckedIn = _checkIns.Any(x => x.BadgeId == SelectedPoi.BadgeId);

			await _dialogService.ShowAlertAsync(AppResources.Trail_CheckInSuccessDetail,
				AppResources.Trail_CheckInSuccessTitle, AppResources.Trail_CheckInSuccessButton);
		}

		private async Task LogAsync()
		{
			if (IsBusy)
			{
				return;
			}

			try
			{
				StartBusy();

				var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
				if (status == PermissionStatus.Granted)
				{
					var position = await GeoLocationHelper.GetLastPosition();
					var distance = await GeoLocationHelper.CalculateDistance(position, SelectedPoi.Position);

					if (GeoLocationHelper.KilometerToMeter(distance) > 200 && !CheckedIn)
					{
						await _dialogService.ShowAlertAsync(AppResources.Trail_CheckInDetail,
							AppResources.Trail_CheckInTitle, AppResources.Trail_CheckInButton);
						return;
					}
				}
				else
				{
					if (!CheckedIn)
					{
						await _dialogService.ShowAlertAsync(AppResources.Trail_CheckInDetail,
							AppResources.Trail_CheckInTitle,
							AppResources.Trail_CheckInButton);
						return;
					}
				}

				var result = await Application.Current.MainPage.Navigation.ShowPopupAsync(new PoiPopUp(SelectedPoi));
				if (result != null)
				{
					switch (result.InteractionResult)
					{
						case "yes":
							var logged = await _adventureService.LogUserBadge(new UserBadgeRequest{ UserId = _userId, BadgeId = SelectedPoi.BadgeId, CheckedIn = CheckedIn});
							if (!logged)
							{
								await _dialogService.ShowAlertAsync(AppResources.Challenge_BadgeErrorDescription,
									AppResources.Challenge_BadgeErrorTitle,
									AppResources.Error_OkButton);
							}
							else
							{
								_cacheService.RemoveCheckIn(SelectedPoi.BadgeId);
								SelectedPoi.Collected = true;
								RaisePropertyChanged(() => SelectedPoi);

								UpdatePin(SelectedPoi);
								DisplayPopup = false;
								ShowBadgePopUp((BadgeTypes)SelectedPoi.BadgeTypeId);
							}

							return;
						case "no":
							DisplayPopup = false;
							await NavigationService.GoToAsync("/adventureChallengeDetails", "ChallengeId",
								string.Empty);
							return;
						default:
							return;
					}
				}
			}
			finally
			{
				StopBusy();
			}
		}
	}
}