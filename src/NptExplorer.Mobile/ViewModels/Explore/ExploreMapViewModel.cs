using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using MvvmHelpers;
using NptExplorer.Core.Enums;
using NptExplorer.Core.Exceptions;
using NptExplorer.Core.Models;
using NptExplorer.Dto.Models;
using NptExplorer.Dto.Requests;
using NptExplorer.Mobile.Controls;
using NptExplorer.Mobile.Helpers;
using NptExplorer.Mobile.Models;
using NptExplorer.Mobile.Resources;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace NptExplorer.Mobile.ViewModels.Explore
{
	public class ExploreMapViewModel : ViewModelBase
	{
		public ICommand GoToFilter { get; set; }
		public ICommand GoToExploreHome { get; set; }
		public ICommand SearchCommand { get; set; }

		private string _searchValue;
		private bool _showingMap = true;

		private ObservableRangeCollection<LocationOverview> _locations;
		private ExploreLocation _selectedLocation;
		private CustomMap _adventureMap;

		private readonly ILocationService _locationService;
		private readonly IDialogService _dialogService;
		private readonly ISettingsService _settingsService;
		private readonly IMapper _mapper;
		private readonly INavigationService _navigationService;
		private PermissionStatus _status;
		private ExploreFilters _filters;
		private readonly ICacheService _cacheService;

		#region public properties
		public ObservableRangeCollection<LocationOverview> Locations
		{
			get => _locations;
			set
			{
				_locations = value;
				RaisePropertyChanged(() => Locations);
				RaisePropertyChanged(() => LocationCount);
			}
		}
		public CustomMap AdventureMap
		{
			get => _adventureMap;
			set
			{
				_adventureMap = value;
				RaisePropertyChanged(() => AdventureMap);
			}
		}
		public bool ShowingMap
		{
			get => _showingMap;
			set
			{
				_showingMap = value;
				RaisePropertyChanged(() => ShowingMap);
			}
		}
		public string SearchValue
		{
			get => _searchValue;
			set
			{
				_searchValue = value;
				RaisePropertyChanged(() => SearchValue);
			}
		}
		public ExploreLocation SelectedLocation
		{
			get => _selectedLocation;
			set
			{
				_selectedLocation = value;
				RaisePropertyChanged(() => SelectedLocation);
			}
		}
		public int LocationCount => Locations.Count;
		#endregion

		public ExploreMapViewModel(
			ISettingsService settingsService, 
			IDialogService dialogService, 
			ILocationService locationService, 
			INavigationService navigationService,
			IMapper mapper,
			ICacheService cacheService) : base(navigationService)
		{
			_settingsService = settingsService;
			_dialogService = dialogService;
			_locationService = locationService;
			_navigationService = navigationService;
			_mapper = mapper;
			_cacheService = cacheService;

			_locations = new ObservableRangeCollection<LocationOverview>();

			GoToFilter = new Command(NavigateToFilters);
			GoToExploreHome = new Command(NavigateToExploreHome);
			SearchCommand = new Command(async () => await SearchAsync());

			_adventureMap = new CustomMap
			{
				MapType = MapType.Street, 
				HasScrollEnabled = true, 
				HasZoomEnabled = true, 
				IsShowingUser = true
			};
		}

		public async void Init()
		{
			if (IsBusy)
			{
				return;
			}

			try
			{
				StartBusy();
				ScreenTitle = Labels.GetHelloLabel(await _settingsService.GetUserName());
				_status = await PermissionHelper.CheckAndRequestLocationPermission();
				_filters = _settingsService.GetExploreFilters();
				await LoadLocations();
				SetUpMap();
			}
			catch (Exception ex)
			{
				await _dialogService.ShowAlertAsync(
									"Loading issue",
									"Error", "OK");

				if (ex.GetType() != typeof(NoInternetConnectionException))
				{
					//_loggingService.Error(ex);
				}
			}
			finally
			{
				StopBusy();
			}

		}

		public async void NavigateToFilters()
		{
			await NavigationService.GoToAsync("/explorefilters");
		}

		public async void NavigateToExploreHome()
		{
			await NavigationService.GoToAsync("/exploreExplore");
		}

		public async Task GoToSearchedItem(int id)
		{
			await NavigationService.GoToAsync("/exploreSearchedItem", "SearchedItemId", id.ToString());
		}

		private async Task SearchAsync()
		{
			if (IsBusy)
			{
				return;
			}

			StartBusy(AppResources.Global_Searching);
			try
			{
				if (SearchValue.Length >= 3)
				{
					await LoadLocations();
					SetUpMap();
				}
			}
			finally
			{
				StopBusy();
			}
		}

		private async Task LoadLocations()
		{
			var cachedLocations = _cacheService.GetSelectedLocations();
			if (cachedLocations != null && cachedLocations.Any())
			{
				Locations = new ObservableRangeCollection<LocationOverview>(cachedLocations);
				return;
			}

			var locationRequest = new LocationRequest()
			{
				LocationServicesEnabled = _status == PermissionStatus.Granted,
				SearchPhrase = SearchValue,
				MaxRecords = null,
				Filters = _mapper.Map<ExploreFiltersDto>(_filters),
				CurrentLocation = _mapper.Map<LatLongDto>(await GeoLocationHelper.GetLastPosition())
			};

			Locations = new ObservableRangeCollection<LocationOverview>(await _locationService.GetLocations(locationRequest));
		}

		private void SetUpMap()
		{
			var customMap = new CustomMap
			{
				MapType = MapType.Street,
				HasScrollEnabled = true,
				HasZoomEnabled = true,
				IsShowingUser = true
			};

			var position = new Position(Locations[0].Latitude, Locations[0].Longitude);
			AddPins(Locations, customMap);
			customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(5.0)));
			AdventureMap = customMap;
		}

		private void AddPins(ObservableRangeCollection<LocationOverview> locations, CustomMap map)
		{
			var pins = new List<CustomPin>();
			foreach (var loc in locations)
			{
				var pin = new CustomPin
				{
					ItemId = loc.Id,
					Name = loc.LocationName,
					Label = loc.LocationName,
					BadgeLabel = string.Empty,
					Address = string.Empty,
					Type = PinType.Place,
					IconType = BadgeTypes.Anon,
					Position = new Position(loc.Latitude, loc.Longitude)
				};
				if (Device.RuntimePlatform == Device.Android)
				{
					pin.InfoWindowClicked += async (s, args) =>
					{
						await ShowLocationDetailsAsync(s);
					};
				}
				else
				{
					pin.CustomInfoWindowClicked += async (s) =>
					{
						await ShowLocationDetailsAsync(s);
					};
				}
				pins.Add(pin);
			}
			foreach (var cpin in pins)
			{
				map.Pins.Add(cpin);
			}
		}

		private async Task ShowLocationDetailsAsync(object s)
		{
			var pin = (CustomPin)s;
			SelectedLocation = _mapper.Map<ExploreLocation>(Locations.FirstOrDefault(x => x.Id == pin.ItemId));
			var result = await Application.Current.MainPage.Navigation.ShowPopupAsync(new Views.Explore.LocationPopUp(SelectedLocation));

			if (result == null || string.IsNullOrEmpty(result.InteractionResult))
			{
				return;
			}

			if (result.InteractionResult == "navigate")
			{
				await _navigationService.GoToAsync("/exploreSearchedItem", "SearchedItemId", SelectedLocation.Id.ToString());
			}
		}
	}
}
