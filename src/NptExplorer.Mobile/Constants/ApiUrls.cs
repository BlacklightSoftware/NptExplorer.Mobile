namespace NptExplorer.Mobile.Constants
{

	public class ApiUrls
	{
		public static readonly string Ping = SystemConstants.BaseUrl + "/api/Ping";
		public static readonly string GetPrerequisiteData = SystemConstants.BaseUrl + "/api/GetPrerequisiteData";

		#region Adventure

		public static readonly string GetChallenges = SystemConstants.BaseUrl + "/api/GetChallenges";
		public static readonly string GetChallenge = SystemConstants.BaseUrl + "/api/GetChallenge";
		public static readonly string GetTrails = SystemConstants.BaseUrl + "/api/GetTrails";
		public static readonly string GetTrail = SystemConstants.BaseUrl + "/api/GetTrail";
		public static readonly string GetTrailRoute = SystemConstants.BaseUrl + "/api/GetTrailRoute";
		public static readonly string LogUserBadge = SystemConstants.BaseUrl + "/api/PutUserBadge";

		#endregion

		#region Explore

		public static readonly string GetLocations = SystemConstants.BaseUrl + "/api/GetLocationsForApp";
		public static readonly string GetLocationOverviews = SystemConstants.BaseUrl + "/api/GetLocationsOverview";
		public static readonly string GetSearchedLocation = SystemConstants.BaseUrl + "/api/GetSearchedLocation?loc=";
		public static readonly string GetLocation = SystemConstants.BaseUrl + "/api/GetLocation?id=";

		#endregion

		#region Profile

		public static readonly string AddUser = SystemConstants.BaseUrl + "/api/AddUser";
		public static readonly string DeleteUser = SystemConstants.BaseUrl + "/api/DeleteUser";
		public static readonly string GetAllUsers = SystemConstants.BaseUrl + "/api/GetAllUsers";
		public static readonly string ExplorerLevel = SystemConstants.BaseUrl + "/api/ExplorerLevel";
		public static readonly string GetUser = SystemConstants.BaseUrl + "/api/GetUser";
		public static readonly string AmendFollower = SystemConstants.BaseUrl + "/api/AmendFollower";
		public static readonly string PostExplorerBoardInclusion = SystemConstants.BaseUrl + "/api/PostExplorerBoardInclusion";
		
		#endregion
	}
}