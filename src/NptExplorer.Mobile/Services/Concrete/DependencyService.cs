using NptExplorer.Mobile.Services.Abstract;

namespace NptExplorer.Mobile.Services.Concrete
{
    public class DependencyService : IDependencyService
    {
        public T Get<T>() where T : class
        {
            //return Xamarin.Forms.DependencyService.Get<T>();
            return null;
        }
    }
}
