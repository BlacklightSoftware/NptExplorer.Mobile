using System;
using System.Threading.Tasks;

namespace NptExplorer.Mobile.Services.Abstract
{
    public interface IRequestProviderService
    {
        Task<T> AttemptAndRetry<T>(Func<Task<T>> action, int numRetries = 3);
        Task<TReturn> Get<TReturn>(string url);
        Task<TReturn> Get<TReturn>(string url, string data);
        Task<TReturn> Put<T, TReturn>(string url, T data);
        Task<TReturn> Post<T, TReturn>(string url, T data);
        Task<TReturn> Patch<T, TReturn>(string url, T data);
    }
}