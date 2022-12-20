using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NptExplorer.Core.Exceptions;
using NptExplorer.Mobile.Services.Abstract;
using Polly;

namespace NptExplorer.Mobile.Services.Concrete
{
	public class RequestProviderService : IRequestProviderService
	{
		private readonly IConnectionService _connectionService;

		private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
		{
			DateTimeZoneHandling = DateTimeZoneHandling.Utc,
			NullValueHandling = NullValueHandling.Ignore
		};

		public RequestProviderService(IConnectionService connectionService)
		{
			_connectionService = connectionService;
		}

		public Task<T> AttemptAndRetry<T>(Func<Task<T>> action, int numRetries = 3)
		{
			return Policy.Handle<HttpRequestException>().WaitAndRetryAsync(numRetries, PollyRetryAttempt).ExecuteAsync(action);

			static TimeSpan PollyRetryAttempt(int attemptNumber) => TimeSpan.FromMilliseconds(Math.Pow(2, attemptNumber));
		}

		public async Task<TReturn> Get<TReturn>(string url)
		{
			CheckConnected();

			using var client = CreateHttpClient(url);
			var response = await client.GetAsync(url);
			return ConsumeResponse<TReturn>(response);
		}

		public async Task<TReturn> Get<TReturn>(string url, string data)
		{
			CheckConnected();

			using var client = CreateHttpClient(url);
			var response = await client.GetAsync(url + "/" + data);
			return ConsumeResponse<TReturn>(response);
		}

		public async Task<TReturn> Put<T, TReturn>(string url, T data)
		{
			CheckConnected();

			HttpClient httpClient = CreateHttpClient(url);
			var content = new StringContent(JsonConvert.SerializeObject(data, _serializerSettings));
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			HttpResponseMessage response = await httpClient.PutAsync(url, content);
			return ConsumeResponse<TReturn>(response);
		}

		public async Task<TReturn> Post<T, TReturn>(string url, T data)
		{
			CheckConnected();

			HttpClient httpClient = CreateHttpClient(url);
			var content = new StringContent(JsonConvert.SerializeObject(data, _serializerSettings));
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			HttpResponseMessage response = await httpClient.PostAsync(url, content);
			return ConsumeResponse<TReturn>(response);
		}

		public async Task<TReturn> Patch<T, TReturn>(string url, T data)
		{
			CheckConnected();

			HttpClient httpClient = CreateHttpClient(url);

			var content = new StringContent(JsonConvert.SerializeObject(data, _serializerSettings));
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			HttpResponseMessage response = await httpClient.PatchAsync(url, content);
			return ConsumeResponse<TReturn>(response);
		}

		private void CheckConnected()
		{
			if (!_connectionService.IsConnected())
			{
				throw new NoInternetConnectionException();
			}
		}

		private HttpClient CreateHttpClient(string url)
		{
			var httpClientHandler = new HttpClientHandler();

#if DEBUG
			httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true; //no SSL check needed yet
#endif

			var httpClient = new HttpClient(httpClientHandler);
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			return httpClient;
		}

		private T ConsumeResponse<T>(HttpResponseMessage hrm)
		{
			return hrm.IsSuccessStatusCode && hrm.StatusCode != HttpStatusCode.Conflict
				? JsonConvert.DeserializeObject<T>(hrm.Content.ReadAsStringAsync().Result)
				:
			throw new RequestProviderException($"StatusCode: {hrm.StatusCode}");
		}
	}
}