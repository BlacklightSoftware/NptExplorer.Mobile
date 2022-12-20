using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using NptExplorer.Core.Exceptions;
using NptExplorer.Mobile.Helpers;
using NptExplorer.Mobile.Resources;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NptExplorer.Mobile.ViewModels.Explore
{
	public class ExploreSearchedItemViewModel : ViewModelBase
	{

		private Core.Models.Location _selectedItem;
		private string _address; 

		private readonly ILocationService _locationService;
		private readonly IDialogService _dialogService;
		private readonly ISettingsService _settingsService;
		private PermissionStatus _status;

		public ICommand ExecuteOpenMaps { get; set; }
		public ICommand ExecuteShareLocation { get; set; }

		#region public properties
		public Core.Models.Location SelectedItem {
			get => _selectedItem;
			set
			{
				_selectedItem = value;
				RaisePropertyChanged(()=>SelectedItem);
			}
		}

		public string Address
		{
			get => _address;
			set
			{
				_address = value;
				RaisePropertyChanged(() => Address);
			}
		}

		#endregion

		public ExploreSearchedItemViewModel(ISettingsService settingsService,IDialogService dialogService,ILocationService locationService,INavigationService navigationService): base(navigationService)
		{
			_locationService = locationService;
			_dialogService = dialogService;	
			_settingsService = settingsService;

			ExecuteOpenMaps = new Command(OpenMaps);
			ExecuteShareLocation = new Command(async() => await ShareLocation());
		}

		public async void Init(string id)
		{
			if (IsBusy)
			{
				return;
			}

			try
			{
				StartBusy();
				if (int.TryParse(id, out var locationId))
				{
					_status = await PermissionHelper.CheckAndRequestLocationPermission();
					SelectedItem = await _locationService.GetLocation(id);
					SelectedItem.HighlightsList = SelectedItem.HighlightsList.OrderBy(x => x.Sequence).ToList();
					ScreenTitle = Labels.GetHelloLabel(await _settingsService.GetUserName());
					Address = SelectedItem.Address;
				}
				else
				{
					await _dialogService.ShowAlertAsync(AppResources.Error_MapDetail, AppResources.Error_MapTitle, AppResources.Error_OkButton);
					await NavigationService.GoBackAsync();
				}
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

		public async void OpenMaps()
		{
			if(_status == PermissionStatus.Granted)
			{ 
				var location = new Placemark
				{
					Thoroughfare = Address
				};
				var options = new MapLaunchOptions
				{
					Name = Address,
					NavigationMode = NavigationMode.None
				};
				await Map.OpenAsync(location, options);
			} 
			else
			{
				await _dialogService.ShowAlertAsync(
					AppResources.Error_LoadingLabel,
					AppResources.Error_ErrorTitle, AppResources.Error_OkButton);
			}
			
		}

		public async Task ShareLocation()
		{
			var locationUrl = Device.RuntimePlatform == Device.iOS ?
			  $"maps://http://maps.apple.com/?q={Address}" :
			  $"http://maps.google.com/?q={Address}";

			await Share.RequestAsync(new ShareTextRequest
			{
				Uri = locationUrl,
				Title = AppResources.Explore_SharedLocation
			});
		}
	}
}