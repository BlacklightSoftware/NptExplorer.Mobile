using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using MvvmHelpers;
using NptExplorer.Core.MockData;
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
	public class TrailsViewModel : ViewModelBase
	{
		private ObservableRangeCollection<Core.Models.Trail> _trails;
		private string _searchValue;
		private readonly IAdventureService _adventureService;
		private readonly ISettingsService _settingsService;
		private readonly IMapper _mapper;
		private Filters _filters;
		private PermissionStatus _status;

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

		public ObservableRangeCollection<Trail> Trails
		{
			get => _trails;
			set
			{
				_trails = value;
				RaisePropertyChanged(() => Trails);
			}
		}

		#endregion

		public TrailsViewModel(INavigationService navigationService,
			IAdventureService adventureService,
			ISettingsService settingsService,
			IMapper mapper) : base(navigationService)
		{
			_adventureService = adventureService;
			_settingsService = settingsService;
			_mapper = mapper;

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

			await LoadTrails(string.Empty);
			StopBusy();
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

			StartBusy();
			try
			{
				if (SearchValue.Length >= 3)
				{
					Trails.Clear();
					await LoadTrails(SearchValue);
				}
				else if (string.IsNullOrEmpty(SearchValue))
				{
					await LoadTrails(string.Empty);
				}
			}
			finally
			{
				StopBusy();
			}
		}

		private async Task LoadTrails(string searchPhrase)
		{
			var trailRequest = new TrailRequest()
			{
				LocationServicesEnabled = _status == PermissionStatus.Granted,
				SearchPhrase = searchPhrase,
				Filters = _mapper.Map<FiltersDto>(_filters),
				CurrentLocation = _mapper.Map<LatLongDto>(await GeoLocationHelper.GetLastPosition())
			};

			Trails = new ObservableRangeCollection<Trail>(await _adventureService.GetTrails(trailRequest));
		}

		private async Task NavigateToFilter()
		{
			await NavigationService.GoToAsync("/adventureFilter");
		}
	}
}