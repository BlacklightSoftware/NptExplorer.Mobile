using System;
using System.Threading.Tasks;
using System.Windows.Input;
using NptExplorer.Mobile.Constants;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NptExplorer.Mobile.ViewModels
{
	public class DevSettingsViewModel : ViewModelBase
	{
		private string _version;
		private string _apiUri;
		private string _apiAccessible;
		private readonly IRequestProviderService _requestProviderService;
		private string _userId;
		private readonly ISettingsService _settingsService;
		private string _userName;

		#region Public properties
		public string Version
		{
			get => _version;
			set
			{
				_version = value;
				RaisePropertyChanged(() => Version);
			}
		}

		public string ApiUri
		{
			get => _apiUri;
			set
			{
				_apiUri = value;
				RaisePropertyChanged(() => ApiUri);
			}
		}

		public string ApiAccessible
		{
			get => _apiAccessible;
			set
			{
				_apiAccessible = value;
				RaisePropertyChanged(() => ApiAccessible);
			}
		}

		public string UserId
		{
			get => _userId;
			set
			{
				_userId = value;
				RaisePropertyChanged(() => UserId);
			}
		}

		public string UserName
		{
			get => _userName;
			set
			{
				_userName = value;
				RaisePropertyChanged(() => UserName);
			}
		}
		#endregion

		public ICommand DoneCommand { get; }
		public ICommand ClearUserCommand { get; }

		public DevSettingsViewModel(INavigationService navigationService,
			IRequestProviderService requestProviderService, ISettingsService settingsService) : base(navigationService)
		{
			_requestProviderService = requestProviderService;
			_settingsService = settingsService;
			DoneCommand = new Command(DoneAsync);
			ClearUserCommand = new Command(ClearUserAsync);
		}

		public async Task Init()
		{
			if (IsBusy)
			{
				return;
			}

			try
			{
				StartBusy();

#if DEBUG
			Version = $"DEV {VersionTracking.CurrentVersion}";
#elif STAGING
		Version = $"TEST {VersionTracking.CurrentVersion} ({VersionTracking.CurrentBuild})";
#elif UAT
            Version = $"UAT {VersionTracking.CurrentVersion} ({VersionTracking.CurrentBuild})";
#else
				Version = $"PROD {VersionTracking.CurrentVersion} ({VersionTracking.CurrentBuild})";
#endif

				ApiUri = SystemConstants.BaseUrl;

				var response = await _requestProviderService.AttemptAndRetry(() =>
					_requestProviderService.Get<bool>(ApiUrls.Ping));
				ApiAccessible = response.ToString();
				UserId = await _settingsService.GetUserId();
				UserName = await _settingsService.GetUserName();
			}
			catch (Exception ex)
			{
				var err = ex.Message;
			}
			finally
			{
				StopBusy();
			}
		}

		private async void DoneAsync()
		{
			await NavigationService.GoToAsync("landing");
		}

		private async void ClearUserAsync()
		{
			await _settingsService.ClearUserId();
			await _settingsService.ClearUserName();
			await _settingsService.SetRegistered(false);

			UserId = await _settingsService.GetUserId();
			UserName = await _settingsService.GetUserName();
		}
	}
}