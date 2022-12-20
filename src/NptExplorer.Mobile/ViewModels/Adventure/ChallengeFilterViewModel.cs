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

namespace NptExplorer.Mobile.ViewModels.Adventure
{

	public class ChallengeFilterViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		private List<Chip> _badges;
		private bool _completed;

		public List<Chip> Badges
		{
			get => _badges;
			set
			{
				_badges = value;
				RaisePropertyChanged(() => Badges);
			}
		}

		public bool Completed
		{
			get => _completed;
			set
			{
				_completed = value;
				RaisePropertyChanged(() => Completed);
			}
		}

		public ICommand ToggleSelected { get; }
		public ICommand SaveFilters { get; }
		public ICommand ClearFilters { get; }

		public ChallengeFilterViewModel(INavigationService navigationService, ISettingsService settingsService) : base(
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
			var setFilters = _settingsService.GetChallengeFilters();

			var badges = EnumExtension.EnumToChips<BadgeTypes>(culture, new List<Enum> { BadgeTypes.Trail, BadgeTypes.Anon });
			foreach (var enumObj in setFilters.BadgeFilters)
			{
				var filter = badges.FirstOrDefault(x => x.Id == (int)enumObj);
				if (filter != null)
				{
					filter.Selected = true;
				}
			}

			Badges = badges;
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
				var filters = new ChallengeFilters
				{
					BadgeFilters = GetSelectedBadges(),
				};
				_settingsService.SetChallengeFilters(filters);
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

			var badges = EnumExtension.EnumToChips<BadgeTypes>(culture, new List<Enum> { BadgeTypes.Trail, BadgeTypes.Anon });
			Badges = badges;
		}

		private List<BadgeTypes> GetSelectedBadges()
		{
			var selected = Badges.Where(x => x.Selected);
			return selected.Select(entity => (BadgeTypes)entity.Id).ToList();
		}
	}
}