using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using NptExplorer.Core.Models.SocialMedia;
using NptExplorer.Mobile.Constants;
using NptExplorer.Mobile.Services.Abstract;

namespace NptExplorer.Mobile.Services.Concrete
{
	public class TwitterService : ITwitterService
	{
		private const string ApiBase = "https://api.twitter.com";
		private readonly string _listTimelineEndpoint = $"{ApiBase}/2/lists/1592408143694266371/tweets?expansions=author_id&tweet.fields=created_at&user.fields=profile_image_url";

		private readonly HttpClient _httpClient;
		private readonly ILoggingService _loggingService;
		private readonly IMapper _mapper;

		public TwitterService(IMapper mapper, ILoggingService loggingService)
		{
			_mapper = mapper;
			_loggingService = loggingService;
			_httpClient = new HttpClient();
		}

		public async Task<IEnumerable<Tweet>> GetPostsAsync()
		{
			try
			{
				_httpClient.DefaultRequestHeaders.Clear();
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SystemConstants.TwitterApiBearerToken);
				var timelineRequest = await _httpClient.GetAsync($"{_listTimelineEndpoint}");

				var timeLinePayload = timelineRequest.Content.ReadAsStringAsync().Result;
				var tweetDtos = JsonConvert.DeserializeObject<TweetDto>(timeLinePayload);

				var tweeters = _mapper.Map<List<Tweeter>>(tweetDtos.includes.users);
				return tweetDtos.data.Where(t => t.created_at.Date >= DateTime.Now.AddDays(-10).Date).Select(x => new Tweet(x, tweeters.FirstOrDefault(u => u.Id == x.author_id))).OrderByDescending(t => t.PostedDateTime);
			}
			catch (Exception ex)
			{
				_loggingService.Error(ex);
				return new List<Tweet>();
			}
		}
	}
}