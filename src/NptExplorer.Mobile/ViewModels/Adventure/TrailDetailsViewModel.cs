using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using NptExplorer.Core.Enums;
using NptExplorer.Core.Models;
using NptExplorer.Dto.Requests;
using NptExplorer.Mobile.Helpers;
using NptExplorer.Mobile.Models;
using NptExplorer.Mobile.Resources;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.Services.Concrete;
using NptExplorer.Mobile.ViewModels.Base;
using NptExplorer.Mobile.Views.Adventure;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NptExplorer.Mobile.ViewModels.Adventure
{
	public class TrailDetailsViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		private readonly IAdventureService _adventureService;
		private readonly IDialogService _dialogService;
		private readonly ICacheService _cacheService;
		private Trail _trail;
		private bool _checkedIn;
		private BadgeTypes _badge;
		private string _userId;
		private List<CheckIn> _checkIns;

		public Trail Trail
		{
			get => _trail;
			set
			{
				_trail = value;
				RaisePropertyChanged(() => Trail);
			}
		}

		public BadgeTypes Badge
		{
			get => _badge;
			set
			{
				_badge = value;
				RaisePropertyChanged(() => Badge);
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

		public ICommand GoToNavigateTrails { get; }
		public ICommand LogTrail { get; }
		public ICommand CheckIn { get; }

		public TrailDetailsViewModel(INavigationService navigationService,
			ISettingsService settingsService,
			IAdventureService adventureService,
			IDialogService dialogService,
			ICacheService cacheService) : base(navigationService)
		{
			_settingsService = settingsService;
			_adventureService = adventureService;
			_dialogService = dialogService;
			_cacheService = cacheService;

			GoToNavigateTrails = new Command(async () => await GoToNavigateTrailsAsync());
			LogTrail = new Command(async () => await LogTrailAsync());
			CheckIn = new Command(async () => await CheckInAsync());
		}

		private async Task CheckInAsync()
		{
			_cacheService.SetCheckIn(Trail.BadgeId);
			_checkIns = _cacheService.GetCheckIns();
			CheckedIn = _checkIns.Any(x => x.BadgeId == Trail.BadgeId);

			await _dialogService.ShowAlertAsync(AppResources.Trail_CheckInSuccessDetail,
				AppResources.Trail_CheckInSuccessTitle, AppResources.Trail_CheckInSuccessButton);
		}

		public async void Init(string id)
		{
			if (IsBusy)
			{
				return;
			}

			StartBusy();

			ScreenTitle = Labels.GetHelloLabel(await _settingsService.GetUserName());
			Badge = BadgeTypes.Trail;
			_checkIns = _cacheService.GetCheckIns();

			if (int.TryParse(id, out var trailId))
			{
				_userId = await _settingsService.GetUserId();
				Trail = await _adventureService.GetTrail(trailId, _userId);
				CheckedIn = _checkIns.Any(x => x.BadgeId == Trail.BadgeId);
			}
			else
			{
				await _dialogService.ShowAlertAsync(AppResources.Error_MapDetail, AppResources.Error_MapTitle,
					AppResources.Error_MapOkButton);
			}

			StopBusy();
		}

		private async Task GoToNavigateTrailsAsync()
		{
			var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
			if (status != PermissionStatus.Granted)
			{
				await _dialogService.ShowAlertAsync(AppResources.Error_LocationServicesDetail,
					AppResources.Error_LocationServicesTitle, AppResources.Error_LocationServicesButton);
				return;
			}

			await NavigationService.GoToAsync("/adventureTrailNavigate", "TrailId", Trail.Id.ToString());
		}

		private async Task LogTrailAsync()
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
					var distance = await GeoLocationHelper.CalculateDistance(position, Trail.StartingPosition);

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

				var result = await Application.Current.MainPage.Navigation.ShowPopupAsync(
					new LogTrailPopUp(
						new ChallengeTrail { TrailName = Trail.TrailName }));
				if (result != null)
				{
					switch (result.InteractionResult)
					{
						case "yes":
							var logged = await _adventureService.LogUserBadge(new UserBadgeRequest { UserId = _userId, BadgeId = Trail.BadgeId, CheckedIn = CheckedIn });
							if (!logged)
							{
								await _dialogService.ShowAlertAsync(AppResources.Challenge_BadgeErrorDescription,
									AppResources.Challenge_BadgeErrorTitle,
									AppResources.Error_OkButton);
							}
							else
							{
								_cacheService.RemoveCheckIn(Trail.BadgeId);
								Trail.Collected = true;
								RaisePropertyChanged(() => Trail);
								ShowBadgePopUp(BadgeTypes.Trail);
							}

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