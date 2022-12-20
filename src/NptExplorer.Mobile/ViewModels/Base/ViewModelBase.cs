using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using NptExplorer.Core.Enums;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.Views;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NptExplorer.Mobile.ViewModels.Base
{
	public class ViewModelBase : ExtendedBindableObject
	{
		protected readonly INavigationService NavigationService;

		private bool _isBusy;
		private string _screenTitle;
		private bool _hideNavigation;

		public ViewModelBase(INavigationService navigationService)
		{
			NavigationService = navigationService;
			GoToUrlCommand = new Command<string>(GoToUrl);
			OpenEmailCommand = new Command<string>(async (emailAddress) => await OpenEmail(emailAddress));
			GoBackCommand = new Command(async () => await GoBack());
			GoToSettings = new Command(async () => await GoToSettingsAsync());
		}

		public ICommand GoBackCommand { get; }
		public ICommand GoToSettings { get; }
		public ICommand GoToUrlCommand { get; }
		public ICommand OpenEmailCommand { get; }

		public string ScreenTitle
		{
			get => _screenTitle;

			set
			{
				_screenTitle = value;
				RaisePropertyChanged(() => ScreenTitle);
			}
		}
		public bool IsBusy
		{
			get => _isBusy;

			set
			{
				_isBusy = value;
				RaisePropertyChanged(() => IsBusy);
			}
		}
		public bool HideNavigation
		{
			get => _hideNavigation;

			set
			{
				_hideNavigation = value;
				RaisePropertyChanged(() => HideNavigation);
			}
		}

		public void StartBusy(string message = "", string extraMessage = "")
		{
			IsBusy = true;
		}

		public void StopBusy()
		{
			IsBusy = false;
		}

		public async void ShowBadgePopUp(BadgeTypes badge)
		{
			await Application.Current.MainPage.Navigation.ShowPopupAsync(new BadgePopUp(badge));
		}

		public async Task GoBack()
		{
			await NavigationService.GoBackAsync();
		}

		private async Task GoToSettingsAsync()
		{
			await NavigationService.GoToAsync("/settings");
		}

		public void GoToUrl(string link)
		{
			Launcher.OpenAsync(new Uri(link));
		}

		public async Task OpenEmail(string emailAddress)
		{
			var message = new EmailMessage { To = new List<string> { emailAddress } };
			await Email.ComposeAsync(message);
		}
	}
}