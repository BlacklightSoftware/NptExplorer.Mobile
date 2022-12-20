using System;
using NptExplorer.Mobile.Services.Abstract;

namespace NptExplorer.Mobile.Services.Concrete
{
    public class DebugLoggingService : ILoggingService
    {
        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Warning(string message)
        {
            Debug($"# {nameof(Warning)}");
            Debug(message);
        }

        public void Error(Exception exception)
        {
            Debug($"# {nameof(Error)}");
            Debug(exception.ToString());
        }

        public void Track(string message)
        {
            Debug($"# Track");
            Debug(message);
        }
    }
}