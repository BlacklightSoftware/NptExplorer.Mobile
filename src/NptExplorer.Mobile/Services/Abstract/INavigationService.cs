using System.Collections.Generic;
using System.Threading.Tasks;

namespace NptExplorer.Mobile.Services.Abstract
{
    public interface INavigationService
    {
        Task GoToAsync(string uri);
        Task GoToAsync(string uri, string parameterKey, string parameterValue);
        Task GoToAsync(string uri, Dictionary<string, string> parameters);
        Task GoBackAsync();
        Task GoBackModal();
    }
}