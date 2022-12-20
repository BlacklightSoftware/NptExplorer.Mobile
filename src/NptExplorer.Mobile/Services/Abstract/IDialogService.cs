using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;

namespace NptExplorer.Mobile.Services.Abstract
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
        Task<bool> ConfirmAsync(string message, string title = null, string okayText = null, string cancelText = null, CancellationToken? cancelToken = null);
        Task<string> ShowActionSheetAsync(string title, string cancel, string[] buttons);
        Task<PromptResult> ShowPromptAsync(string title, string message, string okayText, string cancelText, string placeholder = "");
        void ShowToast(string title, int timeSpan = 3);
    }
}