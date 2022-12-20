using NptExplorer.Core.Exceptions;
using NptExplorer.Mobile.Helpers;
using NptExplorer.Mobile.Resources;
using System;
using System.Threading.Tasks;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using System.Windows.Input;
using NptExplorer.Core.Enums;
using NptExplorer.Core.Models;
using NptExplorer.Dto.Requests;
using Xamarin.Forms;
using System.Linq;

namespace NptExplorer.Mobile.ViewModels.Profile
{
	public class ProfileViewModel : ViewModelBase
	{
		private readonly ISettingsService _settingsService;
		private readonly IDialogService _dialogService;
		private readonly IUserService _userService;
		private readonly ICacheService _cacheService;

		private User _user;

		private string _explorerLevel;
		private string _wellbeingLevel;
		private string _natureLevel;
		private string _heritageLevel;
		private string _trailLevel;

		private string _heroCategory;
		private string _rockStarCategory;
		private string _adventurerCategory;
		private string _championCategory;

		private string _heritageComplete;
		private string _rockStarComplete;
		private string _wellbeingComplete;
		private string _natureComplete;
		private string _trailComplete;

		private string _nptTrophy;
		private string _trailTrophy;
		private string _activeTrophy;
		private string _cyclingTrophy;
		private string _starTrophy;
		private string _natureTrophy;
		private string _mindfulnessTrophy;
		private string _heritageTrophy;

		private int _wellbeingBadgesCollected;
		private int _natureBadgesCollected;
		private int _heritageBadgesCollected;
		private int _trailBadgesCollected;
		private int _ratingBadgesCollected;

		private int _wellbeingPoints;
		private int _naturePoints;
		private int _heritagePoints;
		private int _trailPoints;
		private int _ratingPoints;

		private int _points;
		private PrerequisiteData _prerequisiteData;
		private bool _showExplorerBoard;

		#region public properties
		public User User
		{
			get => _user;
			set
			{
				_user = value;
				RaisePropertyChanged(() => User);
			}
		}
		public int WellbeingPoints
		{
			get => _wellbeingPoints;
			set
			{
				_wellbeingPoints = value;
				RaisePropertyChanged(() => WellbeingPoints);
			}
		}
		public int NaturePoints
		{
			get => _naturePoints;
			set
			{
				_naturePoints = value;
				RaisePropertyChanged(() => NaturePoints);
			}
		}
		public int HeritagePoints
		{
			get => _heritagePoints;
			set
			{
				_heritagePoints = value;
				RaisePropertyChanged(() => HeritagePoints);
			}
		}
		public int TrailPoints
		{
			get => _trailPoints;
			set
			{
				_trailPoints = value;
				RaisePropertyChanged(() => TrailPoints);
			}
		}
		public int RatingPoints
		{
			get => _ratingPoints;
			set
			{
				_ratingPoints = value;
				RaisePropertyChanged(() => RatingPoints);
			}
		}
		public string NptTrophy
		{
			get => _nptTrophy;
			set
			{
				_nptTrophy = value;
				RaisePropertyChanged(() => NptTrophy);
			}
		}
		public string TrailTrophy
		{
			get => _trailTrophy;
			set
			{
				_trailTrophy = value;
				RaisePropertyChanged(() => TrailTrophy);
			}
		}
		public string ActiveTrophy
		{
			get => _activeTrophy;
			set
			{
				_activeTrophy = value;
				RaisePropertyChanged(() => ActiveTrophy);
			}
		}
		public string CyclingTrophy
		{
			get => _cyclingTrophy;
			set
			{
				_cyclingTrophy = value;
				RaisePropertyChanged(() => CyclingTrophy);
			}
		}
		public string StarTrophy
		{
			get => _starTrophy;
			set
			{
				_starTrophy = value;
				RaisePropertyChanged(() => StarTrophy);
			}
		}
		public string NatureTrophy
		{
			get => _natureTrophy;
			set
			{
				_natureTrophy = value;
				RaisePropertyChanged(() => NatureTrophy);
			}
		}
		public string MindfulnessTrophy
		{
			get => _mindfulnessTrophy;
			set
			{
				_mindfulnessTrophy = value;
				RaisePropertyChanged(() => MindfulnessTrophy);
			}
		}
		public string HeritageTrophy
		{
			get => _heritageTrophy;
			set
			{
				_heritageTrophy = value;
				RaisePropertyChanged(() => HeritageTrophy);
			}
		}
		public string HeritageComplete
		{
			get => _heritageComplete;
			set
			{
				_heritageComplete = value;
				RaisePropertyChanged(() => HeritageComplete);
			}
		}
		public string RockStarComplete
		{
			get => _rockStarComplete;
			set
			{
				_rockStarComplete = value;
				RaisePropertyChanged(() => RockStarComplete);
			}
		}
		public string WellbeingComplete
		{
			get => _wellbeingComplete;
			set
			{
				_wellbeingComplete = value;
				RaisePropertyChanged(() => WellbeingComplete);
			}
		}
		public string NatureComplete
		{
			get => _natureComplete;
			set
			{
				_natureComplete = value;
				RaisePropertyChanged(() => NatureComplete);
			}
		}
		public string TrailComplete
		{
			get => _trailComplete;
			set
			{
				_trailComplete = value;
				RaisePropertyChanged(() => TrailComplete);
			}
		}
		public string HeroCategory
		{
			get => _heroCategory;
			set
			{
				_heroCategory = value;
				RaisePropertyChanged(() => HeroCategory);
			}
		}
		public string ChampionCategory
		{
			get => _championCategory;
			set
			{
				_championCategory = value;
				RaisePropertyChanged(() => ChampionCategory);
			}
		}
		public string AdventurerCategory
		{
			get => _adventurerCategory;
			set
			{
				_adventurerCategory = value;
				RaisePropertyChanged(() => AdventurerCategory);
			}
		}
		public string RockStarCategory
		{
			get => _rockStarCategory;
			set
			{
				_rockStarCategory = value;
				RaisePropertyChanged(() => RockStarCategory);
			}
		}
		public string ExplorerLevel
		{
			get => _explorerLevel;
			set
			{
				_explorerLevel = value;
				RaisePropertyChanged(() => ExplorerLevel);
			}
		}
		public string WellbeingLevel
		{
			get => _wellbeingLevel;
			set
			{
				_wellbeingLevel = value;
				RaisePropertyChanged(() => WellbeingLevel);
			}
		}
		public string NatureLevel
		{
			get => _natureLevel;
			set
			{
				_natureLevel = value;
				RaisePropertyChanged(() => NatureLevel);
			}
		}
		public string HeritageLevel
		{
			get => _heritageLevel;
			set
			{
				_heritageLevel = value;
				RaisePropertyChanged(() => HeritageLevel);
			}
		}
		public string TrailLevel
		{
			get => _trailLevel;
			set
			{
				_trailLevel = value;
				RaisePropertyChanged(() => TrailLevel);
			}
		}
		public int Points
		{
			get => _points;
			set
			{
				_points = value;
				RaisePropertyChanged(() => Points);
			}
		}
		public int WellbeingBadgesCollected
		{
			get => _wellbeingBadgesCollected;
			set
			{
				_wellbeingBadgesCollected = value;
				RaisePropertyChanged(() => WellbeingBadgesCollected);
			}
		}
		public int NatureBadgesCollected
		{
			get => _natureBadgesCollected;
			set
			{
				_natureBadgesCollected = value;
				RaisePropertyChanged(() => NatureBadgesCollected);
			}
		}
		public int HeritageBadgesCollected
		{
			get => _heritageBadgesCollected;
			set
			{
				_heritageBadgesCollected = value;
				RaisePropertyChanged(() => HeritageBadgesCollected);
			}
		}
		public int TrailBadgesCollected
		{
			get => _trailBadgesCollected;
			set
			{
				_trailBadgesCollected = value;
				RaisePropertyChanged(() => TrailBadgesCollected);
			}
		}
		public int RatingBadgesCollected
		{
			get => _ratingBadgesCollected;
			set
			{
				_ratingBadgesCollected = value;
				RaisePropertyChanged(() => RatingBadgesCollected);
			}
		}
		public bool ShowExplorerBoard
		{
			get => _showExplorerBoard;
			set
			{
				_showExplorerBoard = value;
				RaisePropertyChanged(() => ShowExplorerBoard);
			}
		}
		#endregion
		
		public ICommand GoToFriends { get; set; }
		public ICommand GoToBoard { get; set; }
		public ICommand JoinExplorerBoard { get; set; }

		public ProfileViewModel(IUserService userService, 
			INavigationService navigationService, 
			IDialogService dialogService, 
			ISettingsService settingsService,
			ICacheService cacheService) : base(navigationService)
		{
			_settingsService = settingsService;
			_dialogService = dialogService;
			_userService = userService;
			_cacheService = cacheService;

			GoToFriends = new Command(NavigateToFriends);
			GoToBoard = new Command(NavigateToAdventureBoard);
			JoinExplorerBoard = new Command(async () => await JoinExplorerBoardAsync());
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
				var userId = await _settingsService.GetUserId();
				_prerequisiteData = await _cacheService.GetPrerequisiteData();

				User = await _userService.GetUser(userId);

				if (User == null)
				{
					WellbeingPoints = NaturePoints = HeritagePoints = TrailPoints = 0;
					WellbeingLevel = NatureLevel = HeritageLevel = TrailLevel = AppResources.Profile_ExplorerLabel;
					WellbeingComplete = NatureComplete = HeritageComplete = TrailComplete = RockStarComplete = "0%";
					ShowExplorerBoard = false;
					return;
				}

				ShowExplorerBoard = User.ExplorerBoard;
				GetBadgePoints(User);
				GetExplorerLevel(Points);
				GetBadgeTotal();
				GetTrophys();
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

		private void GetBadgePoints(User user)
		{
			WellbeingBadgesCollected = user.Badges.Count(x => x.BadgeTypeId == BadgeTypes.Wellbeing);
			WellbeingPoints = WellbeingBadgesCollected * _prerequisiteData.WellbeingBadgePoints;
			NatureBadgesCollected = user.Badges.Count(x => x.BadgeTypeId == BadgeTypes.Nature);
			NaturePoints = NatureBadgesCollected * _prerequisiteData.NatureBadgePoints;
			HeritageBadgesCollected = user.Badges.Count(x => x.BadgeTypeId == BadgeTypes.Heritage);
			HeritagePoints = HeritageBadgesCollected * _prerequisiteData.HeritageBadgePoints;
			TrailBadgesCollected = user.Badges.Count(x => x.BadgeTypeId == BadgeTypes.Trail);
			TrailPoints = TrailBadgesCollected * _prerequisiteData.TrailBadgePoints;

			Points = WellbeingPoints + NaturePoints + HeritagePoints + TrailPoints;
			GetWellbeingLevel();
			GetNatureLevel();
			GetHeritageLevel();
			GetTrailLevel();
		}
		
		private void GetExplorerLevel(int collectedPoints)
		{
			// Levels: Explorer > Adventurer > Hero > Champion
			if (collectedPoints >= _prerequisiteData.OverallChampionLevel)
			{
				ExplorerLevel = AppResources.Profile_ChampionLabel;
				return;
			}
			if (collectedPoints >= _prerequisiteData.OverallHeroLevel)
			{
				ExplorerLevel = AppResources.Profile_HeroLabel;
				return;
			}
			if (collectedPoints >= _prerequisiteData.OverallAdventureLevel)
			{
				ExplorerLevel = AppResources.Profile_AdventurerLabel;
				return;
			}

			ExplorerLevel = AppResources.Profile_ExplorerLabel;
		}
		
		private void GetWellbeingLevel()
		{
			// Levels: Explorer > Adventurer > Hero > Champion
			if (WellbeingPoints >= _prerequisiteData.WellbeingChampionLevel)
			{
				WellbeingLevel = AppResources.Profile_ChampionLabel;
				return;
			}
			if (WellbeingPoints >= _prerequisiteData.WellbeingHeroLevel)
			{
				WellbeingLevel = AppResources.Profile_HeroLabel;
				return;
			}
			if (WellbeingPoints >= _prerequisiteData.WellbeingAdventureLevel)
			{
				WellbeingLevel = AppResources.Profile_AdventurerLabel;
				return;
			}

			WellbeingLevel = AppResources.Profile_ExplorerLabel;
		}
		
		private void GetNatureLevel()
		{
			// Levels: Explorer > Adventurer > Hero > Champion
			if (NaturePoints >= _prerequisiteData.NatureChampionLevel)
			{
				NatureLevel = AppResources.Profile_ChampionLabel;
				return;
			}
			if (NaturePoints >= _prerequisiteData.NatureHeroLevel)
			{
				NatureLevel = AppResources.Profile_HeroLabel;
				return;
			}
			if (NaturePoints >= _prerequisiteData.NatureAdventureLevel)
			{
				NatureLevel = AppResources.Profile_AdventurerLabel;
				return;
			}

			NatureLevel = AppResources.Profile_ExplorerLabel;
		}
		
		private void GetHeritageLevel()
		{
			// Levels: Explorer > Adventurer > Hero > Champion
			if (HeritagePoints >= _prerequisiteData.HeritageChampionLevel)
			{
				HeritageLevel = AppResources.Profile_ChampionLabel;
				return;
			}
			if (HeritagePoints >= _prerequisiteData.HeritageHeroLevel)
			{
				HeritageLevel = AppResources.Profile_HeroLabel;
				return;
			}
			if (HeritagePoints >= _prerequisiteData.HeritageAdventureLevel)
			{
				HeritageLevel = AppResources.Profile_AdventurerLabel;
				return;
			}

			HeritageLevel = AppResources.Profile_ExplorerLabel;
		}
		
		private void GetTrailLevel()
		{
			// Levels: Explorer > Adventurer > Hero > Champion
			if (TrailPoints >= _prerequisiteData.TrailChampionLevel)
			{
				TrailLevel = AppResources.Profile_ChampionLabel;
				return;
			}
			if (TrailPoints >= _prerequisiteData.TrailHeroLevel)
			{
				TrailLevel = AppResources.Profile_HeroLabel;
				return;
			}
			if (TrailPoints >= _prerequisiteData.TrailAdventureLevel)
			{
				TrailLevel = AppResources.Profile_AdventurerLabel;
				return;
			}

			TrailLevel = AppResources.Profile_ExplorerLabel;
		}

		private void GetBadgeTotal()
		{
			WellbeingComplete = GetBadgeCompletePercentage(WellbeingBadgesCollected, _prerequisiteData.TotalWellbeingBadges);
			NatureComplete = GetBadgeCompletePercentage(NatureBadgesCollected, _prerequisiteData.TotalNatureBadges);
			HeritageComplete = GetBadgeCompletePercentage(HeritageBadgesCollected, _prerequisiteData.TotalHeritageBadges);
			TrailComplete = GetBadgeCompletePercentage(TrailBadgesCollected, _prerequisiteData.TotalTrailBadges);

			RockStarCategory = "0";
			RockStarComplete = "0%";
		}

		private string GetBadgeCompletePercentage(double current, double total)
		{
			var num = current / total * 100;

			return num > 100 ? "100%" : (int)num + "%";
		}

		private void GetTrophys()
		{
			NptTrophy = Points == 1865 ? "Gold" : "Black";
			TrailTrophy = TrailPoints >= 150 ? "Gold" : "Black";
			ActiveTrophy = "Black";
			CyclingTrophy = "Black";
			StarTrophy = RatingPoints >= 10 ? "Gold" : "Black";
			MindfulnessTrophy = WellbeingPoints >= 150 ? "Gold" : "Black";
			HeritageTrophy = HeritagePoints >= 200 ? "Gold" : "Black";
			NatureTrophy = NaturePoints >= 500 ? "Gold" : "Black";
		}

		private async void NavigateToAdventureBoard()
		{
			await NavigationService.GoToAsync("/profileAdventureBoard");
		}

		private async void NavigateToFriends()
		{
			await NavigationService.GoToAsync("/profileFriends");
		}

		private async Task JoinExplorerBoardAsync()
		{
			if (IsBusy)
			{
				return;
			}

			try
			{
				var result = await _userService.SetExplorerBoardInclusion(new ExplorerBoardRequest { UserId = User.Id, Include = true });
				if (!result)
				{
					await _dialogService.ShowAlertAsync(AppResources.Profile_JoinExplorerBoardErrorMessage,
						AppResources.Error_ErrorTitle, AppResources.Error_OkButton);
					return;
				}

				ShowExplorerBoard = true;
			}
			catch
			{
				await _dialogService.ShowAlertAsync(AppResources.Profile_JoinExplorerBoardErrorMessage,
					AppResources.Error_ErrorTitle, AppResources.Error_OkButton);
			}
		}
	}
}