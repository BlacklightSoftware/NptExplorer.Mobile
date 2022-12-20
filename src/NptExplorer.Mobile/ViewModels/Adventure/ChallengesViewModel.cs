using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using MvvmHelpers;
using NptExplorer.Core.Models;
using NptExplorer.Dto.Models;
using NptExplorer.Dto.Requests;
using NptExplorer.Mobile.Helpers;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NptExplorer.Mobile.ViewModels.Adventure
{
	public class ChallengesViewModel : ViewModelBase
	{
		private readonly IAdventureService _adventureService;
		private readonly ISettingsService _settingsService;
		private readonly IMapper _mapper;
		private ObservableRangeCollection<ChallengeOverview> _challenges;
		private string _searchValue;
		private PermissionStatus _status;
		private Filters _filters;
		private readonly ICacheService _cacheService;

		public ICommand SearchCommand { get; }
		public ICommand GoToFilter { get; }

		#region public properties

		public string SearchValue
		{
			get => _searchValue;
			set
			{
				_searchValue = value;
				RaisePropertyChanged(() => SearchValue);
			}
		}

		public ObservableRangeCollection<ChallengeOverview> Challenges
		{
			get => _challenges;
			set
			{
				_challenges = value;
				RaisePropertyChanged(() => Challenges);
			}
		}

		#endregion

		public ChallengesViewModel(INavigationService navigationService,
			IAdventureService adventureService,
			ISettingsService settingsService,
			IMapper mapper,
			ICacheService cacheService) : base(navigationService)
		{
			_adventureService = adventureService;
			_settingsService = settingsService;
			_mapper = mapper;
			_cacheService = cacheService;

			SearchCommand = new Command(async () => await SearchAdventures());
			GoToFilter = new Command(async () => await NavigateToFilter());
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

			await LoadChallenges(SearchValue);
			StopBusy();
		}

		public async Task GoToChallengeDetails(int id)
		{
			_cacheService.SetSelectedChallenge(id);
			await NavigationService.GoToAsync("/adventureChallengeDetails", "ChallengeId", id.ToString());
		}

		private async Task SearchAdventures()
		{
			if (IsBusy)
			{
				return;
			}

			StartBusy();
			try
			{
				if (SearchValue.Length >= 3)
				{
					Challenges.Clear();
					await LoadChallenges(SearchValue);
				}
				else if (string.IsNullOrEmpty(SearchValue))
				{
					await LoadChallenges(string.Empty);
				}
			}
			finally
			{
				StopBusy();
			}
		}

		private async Task LoadChallenges(string searchPhrase)
		{
			var challengeRequest = new ChallengeRequest()
			{
				UserId = await _settingsService.GetUserId(),
				LocationServicesEnabled = _status == PermissionStatus.Granted,
				SearchPhrase = searchPhrase,
				Filters = _mapper.Map<FiltersDto>(_filters),
				CurrentLocation = _mapper.Map<LatLongDto>(await GeoLocationHelper.GetLastPosition())
			};

			Challenges =
				new ObservableRangeCollection<ChallengeOverview>(
					await _adventureService.GetChallenges(challengeRequest));
		}

		private async Task NavigateToFilter()
		{
			await NavigationService.GoToAsync("/adventureFilter");
		}
	}
}