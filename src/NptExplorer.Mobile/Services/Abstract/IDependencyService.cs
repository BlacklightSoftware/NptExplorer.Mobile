namespace NptExplorer.Mobile.Services.Abstract
{
    public interface IDependencyService
    {
        T Get<T>() where T : class;
    }
}