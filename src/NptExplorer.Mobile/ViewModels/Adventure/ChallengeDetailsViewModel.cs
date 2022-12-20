using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
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
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace NptExplorer.Mobile.ViewModels.Adventure
{
	public class ChallengeDetailsViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		private readonly IAdventureService _adventureService;
		private readonly IDialogService _dialogService;
		private readonly ILoggingService _loggingService;
		private readonly IMapper _mapper;
		private readonly ICacheService _cacheService;
		private CustomMap _adventureMap;
		private Challenge _challenge;
		private readonly string _culture;
		private ChallengePoi _selectedPoi;
		private PermissionStatus _status;
		private bool _displayPopup;
		private bool _checkedIn;
		private string _userId;
		private List<CustomPin> _customPins;
		private List<CheckIn> _checkIns;

		#region Public properties
		public CustomMap AdventureMap
		{
			get => _adventureMap;
			set
			{
				_adventureMap = value;
				RaisePropertyChanged(() => AdventureMap);
			}
		}

		public Challenge Challenge
		{
			get => _challenge;
			set
			{
				_challenge = value;
				RaisePropertyChanged(() => Challenge);
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

		public ICommand GoToFilter { get; }
		public ICommand CloseDrawerCommand { get; }
		public ICommand NavigateCommand { get; }
		public ICommand LogCommand { get; }
		public ICommand CheckInCommand { get; }

		public ChallengeDetailsViewModel(INavigationService navigationService,
			ISettingsService settingsService,
			IAdventureService adventureService,
			IDialogService dialogService,
			ILoggingService loggingService,
			IMapper mapper,
			ICacheService cacheService) : base(navigationService)
		{
			_settingsService = settingsService;
			_adventureService = adventureService;
			_dialogService = dialogService;
			_loggingService = loggingService;
			_mapper = mapper;
			_cacheService = cacheService;

			_adventureMap = new CustomMap
			{
				MapType = MapType.Street,
				HasScrollEnabled = true,
				HasZoomEnabled = true,
				IsShowingUser = false
			};
			_culture = _settingsService.GetLanguage();

			GoToFilter = new Command(async () => await NavigateToFilter());
			CloseDrawerCommand = new Command(() => DisplayPopup = false);
			NavigateCommand = new Command(async () => await GoToChallengeNavigation());
			LogCommand = new Command(async () => await LogAsync());
			CheckInCommand = new Command(async () => await CheckInAsync());
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
				_status = await PermissionHelper.CheckAndRequestLocationPermission();
				_checkIns = _cacheService.GetCheckIns();

				int challengeId;
				if (string.IsNullOrEmpty(id))
				{
					challengeId = _cacheService.GetSelectedChallenge();
				}
				else
				{
					int.TryParse(id, out challengeId);
				}

				if (challengeId > 0)
				{
					_userId = await _settingsService.GetUserId();
					Challenge = await _adventureService.GetChallenge(challengeId, _userId);

					var customMap = new CustomMap
					{
						MapType = MapType.Street,
						HasScrollEnabled = true,
						HasZoomEnabled = true,
						IsShowingUser = false
					};
					var position = new Position(Challenge.Position.Latitude, Challenge.Position.Longitude);
					var mapSpan = new MapSpan(position, 0.06, 0.05);
					AddPins(Challenge.PointsOfInterest, customMap);
					customMap.MoveToRegion(mapSpan);
					AdventureMap = customMap;
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
			}
			finally
			{
				StopBusy();
			}
		}

		private void AddPins(List<PointOfInterest> pointsOfInterest, CustomMap map)
		{
			_customPins = new List<CustomPin>();
			foreach (var poi in pointsOfInterest)
			{
				CustomPin pin = BuildPin(poi);
				_customPins.Add(pin);
			}

			foreach (var cPin in _customPins)
			{
				map.Pins.Add(cPin);
			}
		}

		private void UpdatePin(PointOfInterest poi)
		{
			var pin = BuildPin(poi);
			var oldPin = _customPins.FirstOrDefault(x => x.ItemId == poi.Id);
			_customPins.Remove(oldPin);
			_customPins.Add(pin);

			_adventureMap.Pins.Clear();
			foreach (var cPin in _customPins)
			{
				_adventureMap.Pins.Add(cPin);
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
				pin.InfoWindowClicked += async (s, args) => { await ShowChallengeDetailsAsync(s); };
			}
			else
			{
				pin.CustomInfoWindowClicked += async (s) => { await ShowChallengeDetailsAsync(s); };
			}

			return pin;
		}

		private Task ShowChallengeDetailsAsync(object s)
		{
			var pin = (CustomPin)s;
			SelectedPoi = _mapper.Map<ChallengePoi>(Challenge.PointsOfInterest.FirstOrDefault(x => x.Id == pin.ItemId));

			CheckedIn = _checkIns.Any(x => x.BadgeId == SelectedPoi.BadgeId);
			DisplayPopup = true;

			return Task.CompletedTask;
		}

		private async Task GoToChallengeNavigation()
		{
			if (_status != PermissionStatus.Granted)
			{
				await _dialogService.ShowAlertAsync(AppResources.Error_LocationServicesDetail, AppResources.Error_LocationServicesTitle, AppResources.Error_OkButton);
				return;
			}

			DisplayPopup = false;
			_cacheService.SetSelectedPoi(SelectedPoi);
			await NavigationService.GoToAsync("/adventureChallengeNavigate");
		}

		private async Task NavigateToFilter()
		{
			await NavigationService.GoToAsync("/adventureChallengeFilter");
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
								var listPoi = Challenge.PointsOfInterest.FirstOrDefault(x => x.Id == SelectedPoi.Id);
								if (listPoi != null)
								{
									listPoi.Collected = true;
								}

								UpdatePin(SelectedPoi);
								DisplayPopup = false;
								ShowBadgePopUp((BadgeTypes)SelectedPoi.BadgeTypeId);
							}

							return;
						default:
							DisplayPopup = false;
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