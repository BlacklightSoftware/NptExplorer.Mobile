using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using NptExplorer.Core.Enums;
using NptExplorer.Core.Extensions;
using NptExplorer.Core.Models;
using NptExplorer.Mobile.Helpers;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using Xamarin.Forms;

namespace NptExplorer.Mobile.ViewModels.Explore
{
	public class ExploreFilterViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		private readonly ICacheService _cacheService;

		private string _sortByFilter;
		private List<Chip> _facilities;
		private List<Chip> _activities;
		private int _distanceSlider;
		private readonly string _culture;

		#region Public properties
		public int DistanceSlider
		{
			get => _distanceSlider;
			set
			{
				_distanceSlider = value;
				RaisePropertyChanged(() => DistanceSlider);
			}
		}
		public string SortByFilter
		{
			get => _sortByFilter;
			set
			{
				_sortByFilter = value;
				RaisePropertyChanged(() => SortByFilter);
			}
		}
		public List<Chip> Facilities
		{
			get => _facilities;
			set
			{
				_facilities = value;
				RaisePropertyChanged(()=> Facilities);
			}
		} 
		public List<Chip> Activities
		{
			get => _activities;
			set
			{
				_activities = value;
				RaisePropertyChanged(()=> Activities);
			}
		} 
		#endregion 

		public ICommand ToggleSelected { get; }
		public ICommand SaveFilters { get; }
		public ICommand ClearFilters { get; }

		public ExploreFilterViewModel(INavigationService navigationService, 
			ISettingsService settingsService, 
			ICacheService cacheService) :base(navigationService)
		{
			_settingsService = settingsService;
			_cacheService = cacheService;

			_culture = _settingsService.GetLanguage();

			ToggleSelected = new Command<Chip>(async (chip) => await ToggleSelectedAsync(chip));
			SaveFilters = new Command(async () => await SaveFiltersAsync());
			ClearFilters = new Command(async () => await ClearFiltersAsync());
		}

		public async void Init()
		{
			if (IsBusy)
			{
				return;
			}

			StartBusy();
			ScreenTitle = Labels.GetHelloLabel(await _settingsService.GetUserName());

			Device.BeginInvokeOnMainThread(async () =>
			{
				await PopulateSelected();
			});

			StopBusy();
		}

		private Task PopulateSelected()
		{
			var culture = _settingsService.GetLanguage();
			var setFilters = _settingsService.GetExploreFilters();

			var activities = EnumExtension.EnumToChips<Activities>(culture);
			foreach (var enumObj in setFilters.ActivitiesFilters)
			{
				var filter = activities.FirstOrDefault(x => x.Id == (int)enumObj);
				if (filter != null)
				{
					filter.Selected = true;
				}
			}

			Activities = activities;

			var facilities = EnumExtension.EnumToChips<Facilities>(culture);
			foreach (var enumObj in setFilters.FacilitiesFilters)
			{
				var filter = facilities.FirstOrDefault(x => x.Id == (int)enumObj);
				if (filter != null)
				{
					filter.Selected = true;
				}
			}

			Facilities = facilities;

			return Task.CompletedTask;
		}

		private Task ToggleSelectedAsync(Chip chip)
		{
			chip.Selected = !chip.Selected;
			return Task.CompletedTask;
		}

		private async Task SaveFiltersAsync()
		{
			if (IsBusy)
			{
				return;
			}

			try
			{
				StartBusy();
				var filters = new ExploreFilters
				{
					FacilitiesFilters = GetSelectedFacilities(),
					ActivitiesFilters =GetSelectedActivities(),
					DistancesInMilesFilters =  GetDistanceSliderValue(),
				};
				_settingsService.SetExploreFilters(filters);
				_cacheService.ClearSelectedLocations();
				await NavigationService.GoBackAsync();
			}
			finally
			{
				StopBusy();
			}
		}

		private async Task ClearFiltersAsync()
		{
			if (IsBusy)
			{
				return;
			}

			try
			{
				StartBusy();
				var filters = new ExploreFilters();
				_settingsService.SetExploreFilters(filters);
				_cacheService.ClearSelectedLocations();
				ClearFiltersUi();
			}
			finally
			{
				StopBusy();
			}
		}

		private void ClearFiltersUi()
		{
			var culture = _settingsService.GetLanguage();

			var activities = EnumExtension.EnumToChips<Activities>(culture);
			Activities = activities;

			var facilities = EnumExtension.EnumToChips<Facilities>(culture);
			Facilities = facilities;
		}

		private List<Activities> GetSelectedActivities()
		{
			var selected = Activities.Where(x => x.Selected);
			return selected.Select(entity => (Activities)entity.Id).ToList();
		}

		private List<Facilities> GetSelectedFacilities()
		{
			var selected = Facilities.Where(x => x.Selected);
			return selected.Select(entity => (Facilities)entity.Id).ToList();

		}

		public int GetDistanceSliderValue()
		{
			return DistanceSlider;
		}
	}
}