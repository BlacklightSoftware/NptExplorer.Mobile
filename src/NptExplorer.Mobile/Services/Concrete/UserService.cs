using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using NptExplorer.Dto.Models;
using NptExplorer.Dto.Requests;
using NptExplorer.Mobile.Constants;
using NptExplorer.Mobile.Services.Abstract;
using User = NptExplorer.Core.Models.User;

namespace NptExplorer.Mobile.Services.Concrete
{
	public class UserService : IUserService
	{
		private readonly IMapper _mapper;
		private readonly IRequestProviderService _requestProviderService;
		private readonly IAuthService _authService;
		private readonly ISettingsService _settingsService;

		public UserService(IMapper mapper, 
			IAuthService authService,
			IRequestProviderService requestProviderService,
			ISettingsService settingsService)
		{
			_mapper = mapper;
			_requestProviderService = requestProviderService;
			_authService = authService;
			_settingsService = settingsService;
		}

		public async Task AddUser(UserRequest request)
		{
			await _requestProviderService.AttemptAndRetry(() => _requestProviderService.Post<UserRequest, bool>(ApiUrls.AddUser, request));
		}

		public async Task<List<User>> ExplorerLevel()
		{
			var response = await _requestProviderService.AttemptAndRetry(() => _requestProviderService.Get<List<UserDto>>(ApiUrls.ExplorerLevel));

			var users = _mapper.Map<List<User>>(response);
			return users;
		}

		public async Task<User> GetUser(string request)
		{
			var response = await _requestProviderService.AttemptAndRetry(() => _requestProviderService.Get<UserDto>($"{ApiUrls.GetUser}?UserId={request}"));

			var users = _mapper.Map<User>(response);
			return users;
		}

		public async Task<List<User>> GetAllUsers()
		{
			var response = await _requestProviderService.AttemptAndRetry(() => _requestProviderService.Get<List<UserDto>>(ApiUrls.GetAllUsers));

			var users = _mapper.Map<List<User>>(response);
			return users;
		}

		public async Task AmendFollower(UserRequest request)
		{
			await _requestProviderService.AttemptAndRetry(() => _requestProviderService.Post<UserRequest,bool>(ApiUrls.AmendFollower, request));

		}

		public async Task<bool> DeleteUser()
		{
			var userId = await _settingsService.GetUserId();
			var response = await _requestProviderService.AttemptAndRetry(() => _requestProviderService.Get<string>($"{ApiUrls.DeleteUser}?userId={userId}"));
			return response == "OK";
		}

		public async Task<bool> SetExplorerBoardInclusion(ExplorerBoardRequest request)
		{
			var response = await _requestProviderService.AttemptAndRetry(() => _requestProviderService.Post<ExplorerBoardRequest, bool>(ApiUrls.PostExplorerBoardInclusion, request));
			return response;
		}
	}
}