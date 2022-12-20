using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using MvvmHelpers;
using NptExplorer.Core.Models;
using NptExplorer.Dto.Models;
using NptExplorer.Dto.Requests;
using NptExplorer.Mobile.Helpers;
using NptExplorer.Mobile.Resources;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NptExplorer.Mobile.ViewModels.Adventure
{
	public class AdventureHomeViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		private readonly IAdventureService _adventureService;
		private ObservableRangeCollection<ChallengeOverview> _challenges;
		private ObservableRangeCollection<Trail> _trails;
		private string _searchValue;
		private PermissionStatus _status;
		private Filters _filters;
		private readonly IMapper _mapper;
		private readonly ICacheService _cacheService;

		public ObservableRangeCollection<ChallengeOverview> Challenges
		{
			get => _challenges;
			set
			{
				_challenges = value;
				RaisePropertyChanged(() => Challenges);
			}
		}

		public ObservableRangeCollection<Trail> Trails
		{
			get => _trails;
			set
			{
				_trails = value;
				RaisePropertyChanged(() => Trails);
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

		public ICommand SearchCommand { get; }
		public ICommand GoToFilter { get; }
		public ICommand GoToTrails { get; }
		public ICommand GoToChallenges { get; }

		public AdventureHomeViewModel(INavigationService navigationService,
			ISettingsService settingsService,
			IAdventureService adventureService,
			IMapper mapper,
			ICacheService cacheService) : base(navigationService)
		{
			_settingsService = settingsService;
			_adventureService = adventureService;
			_mapper = mapper;
			_cacheService = cacheService;

			SearchCommand = new Command(async () => await SearchAdventures());
			GoToFilter = new Command(async () => await NavigateToFilter());
			GoToTrails = new Command(async () => await NavigateToTrails());
			GoToChallenges = new Command(async () => await NavigateToChallenges());
		}

		public async void Init()
		{
			if (IsBusy)
			{
				return;
			}

			StartBusy();

			ScreenTitle = Labels.GetHelloLabel(await _settingsService.GetUserName());
			_status = await PermissionHelper.CheckAndRequestLocationPermission();
			_filters = _settingsService.GetFilters();

			await LoadChallenges(string.Empty, 5);
			await LoadTrails(string.Empty, 5);

			StopBusy();
		}

		public async Task GoToChallengeDetails(int id)
		{
			_cacheService.SetSelectedChallenge(id);
			await NavigationService.GoToAsync("/adventureChallengeDetails", "ChallengeId", id.ToString());
		}

		public async Task GoToTrailDetails(int id)
		{
			await NavigationService.GoToAsync("/adventureTrailDetails", "TrailId", id.ToString());
		}

		private async Task SearchAdventures()
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
					Trails.Clear();
					await LoadTrails(SearchValue, 5);

					Challenges.Clear();
					await LoadChallenges(SearchValue, 5);

				}
				else if (string.IsNullOrEmpty(SearchValue))
				{
					await LoadChallenges(string.Empty, 5);
					await LoadTrails(string.Empty, 5);
				}
			}
			finally
			{
				StopBusy();
			}
		}

		private async Task NavigateToFilter()
		{
			await NavigationService.GoToAsync("/adventureFilter");
		}

		private async Task NavigateToTrails()
		{
			await NavigationService.GoToAsync("/adventureTrails");
		}

		private async Task NavigateToChallenges()
		{
			await NavigationService.GoToAsync("/adventureChallenges");
		}

		private async Task LoadTrails(string searchPhrase, int maxRecords)
		{
			var locationEnabled = _status == PermissionStatus.Granted;
			var trailRequest = new TrailRequest()
			{
				LocationServicesEnabled = locationEnabled,
				SearchPhrase = searchPhrase,
				MaxRecords = maxRecords,
				Filters = _mapper.Map<FiltersDto>(_filters),
				CurrentLocation = _mapper.Map<LatLongDto>(await GeoLocationHelper.GetLastPosition())
			};

			Trails = new ObservableRangeCollection<Trail>(await _adventureService.GetTrails(trailRequest));
		}

		private async Task LoadChallenges(string searchPhrase, int maxRecords)
		{
			var locationEnabled = _status == PermissionStatus.Granted;
			var challengeRequest = new ChallengeRequest()
			{
				UserId = await _settingsService.GetUserId(),
				LocationServicesEnabled = locationEnabled,
				SearchPhrase = searchPhrase,
				MaxRecords = maxRecords,
				Filters = _mapper.Map<FiltersDto>(_filters),
				CurrentLocation = _mapper.Map<LatLongDto>(await GeoLocationHelper.GetLastPosition())
			};

			Challenges =
				new ObservableRangeCollection<ChallengeOverview>(
					await _adventureService.GetChallenges(challengeRequest));
		}
	}
}