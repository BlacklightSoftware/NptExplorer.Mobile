using NptExplorer.Mobile.Services.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NptExplorer.Mobile.Services.Concrete
{
    public class NavigationService : INavigationService
    {
        public async Task GoToAsync(string uri)
        {
            await Shell.Current.GoToAsync(uri);
        }

        public async Task GoToAsync(string uri, string parameterKey, string parameterValue)
        {
            await Shell.Current.GoToAsync($"{uri}?{parameterKey}={parameterValue}");
        }

        public async Task GoToAsync(string uri, Dictionary<string, string> parameters)
        {
            var fullUrl = BuildUri(uri, parameters);
            await Shell.Current.GoToAsync(fullUrl);
        }

        public async Task GoBackAsync()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        public Task GoBackModal()
        {
            return Shell.Current.Navigation.PopModalAsync();
        }

        private string BuildUri(string uri, Dictionary<string, string> parameters)
        {
            var fullUrl = new StringBuilder();
            fullUrl.Append(uri);
            fullUrl.Append("?");
            fullUrl.Append(string.Join("&", parameters.Select(kvp => $"{kvp.Key}={kvp.Value}")));
            return fullUrl.ToString();
        }
    }
}