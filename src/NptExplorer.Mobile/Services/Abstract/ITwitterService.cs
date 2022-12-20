using System.Collections.Generic;
using System.Threading.Tasks;
using NptExplorer.Core.Models.SocialMedia;

namespace NptExplorer.Mobile.Services.Abstract
{

	public interface ITwitterService
	{
		Task<IEnumerable<Tweet>> GetPostsAsync();
	}
}