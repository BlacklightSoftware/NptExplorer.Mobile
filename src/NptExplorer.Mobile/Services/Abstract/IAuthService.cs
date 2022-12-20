using System.Threading.Tasks;

namespace NptExplorer.Mobile.Services.Abstract
{
	public interface IAuthService
	{
		Task<bool> SignInAsync();
		Task<bool> SignOutAsync();
	}
}