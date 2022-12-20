using NptExplorer.Mobile.Services.Abstract;
using Xamarin.Essentials;

namespace NptExplorer.Mobile.Services.Concrete
{
    public class ConnectionService : IConnectionService
    {
        public bool IsConnected()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string CurrentConnection()
        {
            var current = Connectivity.NetworkAccess;
            return current.ToString();
        }
    }
}