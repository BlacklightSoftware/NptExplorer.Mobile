namespace NptExplorer.Mobile.Services.Abstract
{
    public interface IConnectionService
    {
        bool IsConnected();
        string CurrentConnection();
    }
}