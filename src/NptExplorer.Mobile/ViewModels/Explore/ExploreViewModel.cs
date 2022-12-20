using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using MvvmHelpers;
using MvvmHelpers.Commands;
using NptExplorer.Core.Exceptions;
using NptExplorer.Core.Models;
using NptExplorer.Core.Models.SocialMedia;
using NptExplorer.Dto.Models;
using NptExplorer.Dto.Requests;
using NptExplorer.Mobile.Helpers;
using NptExplorer.Mobile.Resources;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using Xamarin.Essentials;

namespace NptExplorer.Mobile.ViewModels.Explore
{
	public class ExploreViewModel : ViewModelBase
	{
		private string _searchValue;
		private bool _showingMap = true;
		private bool _showingLocations;

		private ObservableRangeCollection<LocationOverview> _searchList;
		private ObservableRangeCollection<LocationOverview> _locations;

		private readonly ILocationService _locationService;
		private readonly IDialogService _dialogService;
		private readonly ISettingsService _settingsService;
		private PermissionStatus _status;
		private readonly IMapper _mapper;
		private readonly ITwitterService _twitterService;
		private IEnumerable<Tweet> _tweets;
		private ExploreFilters _filters;
		private readonly ICacheService _cacheService;

		public ICommand GoToFilter { get; set; }
		public ICommand GoToMap { get; set; }
		public ICommand SearchCommand { get; set; }

		#region public properties
		public IEnumerable<Tweet> Tweets
		{
			get => _tweets;
			set
			{
				_tweets = value;
				RaisePropertyChanged(() => Tweets);
			}
		}
		public ObservableRangeCollection<LocationOverview> Locations
		{
			get => _locations;
			set
			{
				_locations = value;
				RaisePropertyChanged(() => Locations);
			}
		}
		public ObservableRangeCollection<LocationOverview> SearchList
		{
			get => _searchList;
			set
			{
				_searchList = value;
				RaisePropertyChanged(() => SearchList);
				RaisePropertyChanged(() => LocationCount);
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
		public bool ShowingLocations
		{
			get => _showingLocations;
			set
			{
				_showingLocations = value;
				RaisePropertyChanged(() => ShowingLocations);
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
		public int LocationCount => SearchList.Count;
		#endregion

		public ExploreViewModel(ISettingsService settingsService,
			INavigationService navigationService,
			IDialogService dialogService,
			ILocationService locationService,
			IMapper mapper,
			ITwitterService twitterService,
			ICacheService cacheService) : base(navigationService)
		{
			_settingsService = settingsService;
			_locationService = locationService;
			_dialogService = dialogService;
			_mapper = mapper;
			_twitterService = twitterService;
			_cacheService = cacheService;

			_searchList = _locations = new ObservableRangeCollection<LocationOverview>();
			GoToFilter = new Command(NavigateToFilters);
			GoToMap = new Command(NavigateToMap);
			SearchCommand = new Command(async() => await SearchAsync());
		}

		public async void Init()
		{
			if (IsBusy)
			{
				return;
			}

			try
			{
				StartBusy("Loading...");
				ScreenTitle = Labels.GetHelloLabel(await _settingsService.GetUserName());
				_status = await PermissionHelper.CheckAndRequestLocationPermission();
				_filters = _settingsService.GetExploreFilters();
				await LoadLocations();
				
				Tweets = await _twitterService.GetPostsAsync();
			}
			catch (Exception ex)
			{
				await _dialogService.ShowAlertAsync(
					AppResources.Error_LoadingLabel,
					AppResources.Error_ErrorTitle, AppResources.Error_OkButton);

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

		public async Task GoToSearchedItem(int id)
		{
			await NavigationService.GoToAsync("/exploreSearchedItem", "SearchedItemId", id.ToString());
		}

		public async void NavigateToFilters()
		{
			await NavigationService.GoToAsync("/explorefilters");
		}

		public async void NavigateToMap()
		{
			_cacheService.SetSelectedLocations(Locations.ToList());
			await NavigationService.GoToAsync("/exploremap");
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
				switch (SearchValue.Length)
				{
					case >= 3:
					{
						await LoadLocations();
						break;
					}
					case 0:
						await LoadLocations();
						break;
				}
			}
			finally
			{
				StopBusy();
			}
		}

		private async Task LoadLocations()
		{
			int? maxRecords = null;

			if (string.IsNullOrEmpty(SearchValue) && 
			    (_filters == null || 
			     (_filters.DistancesInMilesFilters == 0 && 
			     !_filters.ActivitiesFilters.Any() &&
			     !_filters.FacilitiesFilters.Any())))
			{
				ShowingMap = true;
				ShowingLocations = false;
				maxRecords = 5;
			}
			else
			{
				ShowingMap = false;
				ShowingLocations = true;
			}
			
			var cachedLocations = _cacheService.GetSelectedLocations();
			if (cachedLocations != null && cachedLocations.Any())
			{
				SearchList = Locations = new ObservableRangeCollection<LocationOverview>(cachedLocations);
				return;
			}

			var locationRequest = new LocationRequest()
			{
				LocationServicesEnabled = _status == PermissionStatus.Granted,
				SearchPhrase = SearchValue,
				MaxRecords = maxRecords,
				Filters = _mapper.Map<ExploreFiltersDto>(_filters),
				CurrentLocation = _mapper.Map<LatLongDto>(await GeoLocationHelper.GetLastPosition())
			};

			SearchList = Locations = new ObservableRangeCollection<LocationOverview>(await _locationService.GetLocations(locationRequest));
		}
	}
}