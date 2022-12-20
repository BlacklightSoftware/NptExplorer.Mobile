using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using NptExplorer.Core.Models;
using NptExplorer.Dto.Models;
using NptExplorer.Dto.Requests;
using NptExplorer.Mobile.Constants;
using NptExplorer.Mobile.Services.Abstract;

namespace NptExplorer.Mobile.Services.Concrete
{
	public class AdventureService : IAdventureService
	{
		private readonly IMapper _mapper;
		private readonly IRequestProviderService _requestProviderService;
		private readonly ILoggingService _loggingService;

		public AdventureService(IMapper mapper, ILoggingService loggingService,
			IRequestProviderService requestProviderService)
		{
			_mapper = mapper;
			_loggingService = loggingService;
			_requestProviderService = requestProviderService;
		}

		public async Task<List<Trail>> GetTrails(TrailRequest request)
		{
			var response = await this._requestProviderService.AttemptAndRetry(() =>
				this._requestProviderService.Post<TrailRequest, List<TrailDto>>(ApiUrls.GetTrails, request));
			var trails = _mapper.Map<List<Trail>>(response);
			return trails;
		}

		public async Task<List<ChallengeOverview>> GetChallenges(ChallengeRequest request)
		{
			var response = await _requestProviderService.AttemptAndRetry(() =>
				_requestProviderService.Post<ChallengeRequest, List<ChallengeOverviewDto>>(ApiUrls.GetChallenges,
					request));
			var challenges = _mapper.Map<List<ChallengeOverview>>(response);
			return challenges;
		}

		public async Task<Challenge> GetChallenge(int id, string uid)
		{
			var response = await this._requestProviderService.AttemptAndRetry(() =>
				this._requestProviderService.Get<ChallengeDto>($"{ApiUrls.GetChallenge}?challengeId={id}&userId={uid}"));
			var challenge = _mapper.Map<Challenge>(response);
			return challenge;
		}

		public async Task<Trail> GetTrail(int id, string uid)
		{
			var response = await this._requestProviderService.AttemptAndRetry(() =>
				this._requestProviderService.Get<TrailDto>($"{ApiUrls.GetTrail}?trailId={id}&userId={uid}"));
			var trail = _mapper.Map<Trail>(response);
			return trail;
		}

		public async Task<TrailRoute> GetTrailRoute(int trailId)
		{
			var response = await this._requestProviderService.AttemptAndRetry(() =>
				this._requestProviderService.Get<TrailRouteDto>($"{ApiUrls.GetTrailRoute}?trailId={trailId}"));
			var trail = _mapper.Map<TrailRoute>(response);
			return trail;
		}

		public async Task<bool> LogUserBadge(UserBadgeRequest request)
		{
			try
			{
				await _requestProviderService.AttemptAndRetry(() =>
					_requestProviderService.Post<UserBadgeRequest, string>(ApiUrls.LogUserBadge, request));
				return true;
			}
			catch (Exception ex)
			{
				_loggingService.Error(ex);
				return false;
			}
		}
	}
}