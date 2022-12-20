using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using NptExplorer.Mobile.Constants;
using NptExplorer.Mobile.Services.Abstract;

namespace NptExplorer.Mobile.Services.Concrete
{
	public class AuthService : IAuthService
	{
		private readonly IPublicClientApplication _pca;
		private readonly ISettingsService _settingsService;
		private readonly IDialogService _dialogService;

		public AuthService(ISettingsService settingsService, IDialogService dialogService)
		{
			_settingsService = settingsService;
			_dialogService = dialogService;

			_pca = PublicClientApplicationBuilder.Create(SystemConstants.ClientId)
				.WithIosKeychainSecurityGroup(SystemConstants.IosKeychainSecurityGroups)
				.WithB2CAuthority(SystemConstants.AuthoritySignIn)
				.WithRedirectUri($"msal{SystemConstants.ClientId}://auth")
				.Build();
		}

		public async Task<bool> SignInAsync()
		{
			try
			{
				var accounts = await _pca.GetAccountsAsync();
				var firstAccount = accounts.FirstOrDefault();
				var authResult = await _pca.AcquireTokenSilent(SystemConstants.Scopes, firstAccount).ExecuteAsync();

				// Store the access token securely for later use.
				await SetSecureValues(authResult);

				return true;
			}
			catch (MsalUiRequiredException)
			{
				try
				{
					// This means we need to login again through the MSAL window.
					var authResult = await _pca.AcquireTokenInteractive(SystemConstants.Scopes)
						.WithPrompt(Prompt.ForceLogin)
						.WithParentActivityOrWindow(App.ParentWindow)
						.ExecuteAsync();

					// Store the access token securely for later use.
					await SetSecureValues(authResult);

					return true;
				}
				catch (Exception ex2)
				{
#if DEBUG
					Debug.WriteLine(ex2.ToString());
#else
					_dialogService.ShowAlertAsync("There has been an issue signing into your account", "Sorry", "OK");
#endif
					return false;
				}
			}
			catch (Exception ex)
			{
#if DEBUG
				Debug.WriteLine(ex.ToString());
#else
#endif
				return false;
			}
		}

		public async Task<bool> SignOutAsync()
		{
			try
			{
				var accounts = await _pca.GetAccountsAsync();

				// Go through all accounts and remove them.
				while (accounts.Any())
				{
					await _pca.RemoveAsync(accounts.FirstOrDefault());
					accounts = await _pca.GetAccountsAsync();
				}

				// Clear our user data secure storage.
				await _settingsService.ClearUserName();
				await _settingsService.ClearUserId();
				return true;
			}
			catch (Exception ex)
			{
#if DEBUG
				Debug.WriteLine(ex.ToString());
#else
#endif
				return false;
			}
		}

		private async Task SetSecureValues(AuthenticationResult authResult)
		{
			var token = authResult.IdToken;
			if (token != null)
			{
				var handler = new JwtSecurityTokenHandler();
				var data = handler.ReadJwtToken(authResult.IdToken);
				var oid = data.Claims.FirstOrDefault(x => x.Type.Equals("oid"))?.Value;
				var user = data.Claims.FirstOrDefault(x => x.Type.Equals("name"))?.Value;
				await _settingsService.SetUserName(user);
				await _settingsService.SetUserId(oid);
				await _settingsService.SetGuest(false);
			}
		}
	}
}