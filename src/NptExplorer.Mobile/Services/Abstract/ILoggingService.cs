using System;

namespace NptExplorer.Mobile.Services.Abstract
{
    public interface ILoggingService
    {
        void Debug(string message);
        void Warning(string message);
        void Error(Exception ex);
        void Track(string message);
    }
}