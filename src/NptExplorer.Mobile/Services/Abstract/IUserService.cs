using System.Collections.Generic;
using NptExplorer.Core.Models;
using NptExplorer.Dto.Requests;
using System.Threading.Tasks;

namespace NptExplorer.Mobile.Services.Abstract
{
	public interface IUserService
	{
		Task AddUser(UserRequest request);
		Task<List<User>> ExplorerLevel();
		Task<User> GetUser(string request);
		Task<List<User>> GetAllUsers();
		Task AmendFollower(UserRequest request);
		Task<bool> DeleteUser();
		Task<bool> SetExplorerBoardInclusion(ExplorerBoardRequest request);
	}
}
