using System.Collections.Generic;
using System.Threading.Tasks;
using NptExplorer.Core.Models;
using NptExplorer.Dto.Requests;

namespace NptExplorer.Mobile.Services.Abstract
{
	public interface IAdventureService
	{
		Task<List<Trail>> GetTrails(TrailRequest request);
		Task<List<ChallengeOverview>> GetChallenges(ChallengeRequest request);
		Task<Challenge> GetChallenge(int id, string uid);
		Task<Trail> GetTrail(int id, string uid);
		Task<TrailRoute> GetTrailRoute(int trailId);
		Task<bool> LogUserBadge(UserBadgeRequest request);
	}
}