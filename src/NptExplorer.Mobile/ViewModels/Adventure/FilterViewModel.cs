using System;
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
using BadgeTypes = NptExplorer.Core.Enums.BadgeTypes;

namespace NptExplorer.Mobile.ViewModels.Adventure
{
	public class FilterViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		private string _sortByFilter;
		private List<Chip> _habitats;
		private List<Chip> _difficulties;
		private List<Chip> _distances;
		private List<Chip> _trailTimes;
		private List<Chip> _badges;

		#region Public properties

		public string SortByFilter
		{
			get => _sortByFilter;
			set
			{
				_sortByFilter = value;
				RaisePropertyChanged(() => SortByFilter);
			}
		}

		public List<Chip> Badges
		{
			get => _badges;
			set
			{
				_badges = value;
				RaisePropertyChanged(() => Badges);
			}
		}

		public List<Chip> Habitats
		{
			get => _habitats;
			set
			{
				_habitats = value;
				RaisePropertyChanged(() => Habitats);
			}
		}

		public List<Chip> Difficulties
		{
			get => _difficulties;
			set
			{
				_difficulties = value;
				RaisePropertyChanged(() => Difficulties);
			}
		}

		public List<Chip> Distances
		{
			get => _distances;
			set
			{
				_distances = value;
				RaisePropertyChanged(() => Distances);
			}
		}

		public List<Chip> TrailTimes
		{
			get => _trailTimes;
			set
			{
				_trailTimes = value;
				RaisePropertyChanged(() => TrailTimes);
			}
		}

		#endregion

		public ICommand ToggleSelected { get; }
		public ICommand SaveFilters { get; }
		public ICommand ClearFilters { get; }

		public FilterViewModel(INavigationService navigationService, ISettingsService settingsService) : base(
			navigationService)
		{
			_settingsService = settingsService;
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
			var setFilters = _settingsService.GetFilters();

			if (!string.IsNullOrEmpty(setFilters.SortBy))
			{
				SortByFilter = setFilters.SortBy;
			}
			else
			{
				SortByFilter = "miles";
			}

			var badges = EnumExtension.EnumToChips<BadgeTypes>(culture, new List<Enum>{BadgeTypes.Anon});
			foreach (var enumObj in setFilters.BadgeFilters)
			{
				var filter = badges.FirstOrDefault(x => x.Id == (int)enumObj);
				if (filter != null)
				{
					filter.Selected = true;
				}
			}

			Badges = badges;

			var habitats = EnumExtension.EnumToChips<Habitats>(culture);
			foreach (var enumObj in setFilters.HabitatFilters)
			{
				var filter = habitats.FirstOrDefault(x => x.Id == (int)enumObj);
				if (filter != null)
				{
					filter.Selected = true;
				}
			}

			Habitats = habitats;

			var difficulties = EnumExtension.EnumToChips<Difficulties>(culture);
			foreach (var enumObj in setFilters.DifficultyFilters)
			{
				var filter = difficulties.FirstOrDefault(x => x.Id == (int)enumObj);
				if (filter != null)
				{
					filter.Selected = true;
				}
			}

			Difficulties = difficulties;

			var distances = EnumExtension.EnumToChips<Distances>(culture);
			foreach (var enumObj in setFilters.DistanceFilters)
			{
				var filter = distances.FirstOrDefault(x => x.Id == (int)enumObj);
				if (filter != null)
				{
					filter.Selected = true;
				}
			}

			Distances = distances;

			var times = EnumExtension.EnumToChips<TrailTimes>(culture);
			foreach (var enumObj in setFilters.TrailTimeFilters)
			{
				var filter = times.FirstOrDefault(x => x.Id == (int)enumObj);
				if (filter != null)
				{
					filter.Selected = true;
				}
			}

			TrailTimes = times;

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
				var filters = new Filters
				{
					SortBy = SortByFilter,
					BadgeFilters = GetSelectedBadges(),
					HabitatFilters = GetSelectedHabitats(),
					DifficultyFilters = GetSelectedDifficulties(),
					DistanceFilters = GetSelectedDistances(),
					TrailTimeFilters = GetSelectedTrailTimes()
				};
				_settingsService.SetFilters(filters);
				await NavigationService.GoBackAsync();
			}
			finally
			{
				StopBusy();
			}
		}

		private Task ClearFiltersAsync()
		{
			if (IsBusy)
			{
				return Task.CompletedTask;
			}

			try
			{
				StartBusy();
				var filters = new Filters();
				_settingsService.SetFilters(filters);
				ClearFiltersUi();
			}
			finally
			{
				StopBusy();
			}

			return Task.CompletedTask;
		}

		private void ClearFiltersUi()
		{
			var culture = _settingsService.GetLanguage();

			SortByFilter = "miles";
			var badges = EnumExtension.EnumToChips<BadgeTypes>(culture, new List<Enum>{BadgeTypes.Anon});
			Badges = badges;

			var habitats = EnumExtension.EnumToChips<Habitats>(culture);
			Habitats = habitats;

			var difficulties = EnumExtension.EnumToChips<Difficulties>(culture);
			Difficulties = difficulties;

			var distances = EnumExtension.EnumToChips<Distances>(culture);
			Distances = distances;

			var times = EnumExtension.EnumToChips<TrailTimes>(culture);
			TrailTimes = times;
		}

		private List<BadgeTypes> GetSelectedBadges()
		{
			var selected = Badges.Where(x => x.Selected);
			return selected.Select(entity => (BadgeTypes)entity.Id).ToList();
		}

		private List<Habitats> GetSelectedHabitats()
		{
			var selected = Habitats.Where(x => x.Selected);
			return selected.Select(entity => (Habitats)entity.Id).ToList();
		}

		private List<Difficulties> GetSelectedDifficulties()
		{
			var selected = Difficulties.Where(x => x.Selected);
			return selected.Select(entity => (Difficulties)entity.Id).ToList();
		}

		private List<Distances> GetSelectedDistances()
		{
			var selected = Distances.Where(x => x.Selected);
			return selected.Select(entity => (Distances)entity.Id).ToList();
		}

		private List<TrailTimes> GetSelectedTrailTimes()
		{
			var selected = TrailTimes.Where(x => x.Selected);
			return selected.Select(entity => (TrailTimes)entity.Id).ToList();
		}
	}
}