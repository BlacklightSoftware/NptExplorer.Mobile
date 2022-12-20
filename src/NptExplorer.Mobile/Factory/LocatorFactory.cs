namespace NptExplorer.Mobile.Factory
{
    public static class LocatorFactory
    {
        public static T Resolve<T>() where T : class
        {
            return (T) Startup.ServiceProvider.GetService(typeof(T));
        }
    }
}