using System;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using NptExplorer.Mobile.Services.Abstract;

namespace NptExplorer.Mobile.Services.Concrete
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public Task<bool> ConfirmAsync(string message, string title = null, string okayText = null, string cancelText = null, CancellationToken? cancelToken = null)
        {
            return UserDialogs.Instance.ConfirmAsync(message, title, okayText, cancelText, cancelToken);
        }

        public Task<string> ShowActionSheetAsync(string title, string cancel, string[] buttons)
        {
            return UserDialogs.Instance.ActionSheetAsync(title, cancel, null, null, buttons);
        }

        public Task<PromptResult> ShowPromptAsync(string title, string message, string okayText, string cancelText, string placeholder = "")
        {
            return UserDialogs.Instance.PromptAsync(message, title, okayText, cancelText, placeholder);
        }

        public void ShowToast(string title, int timeSpan = 3)
        {
	        UserDialogs.Instance.Toast(title, TimeSpan.FromSeconds(timeSpan));
        }
    }
}