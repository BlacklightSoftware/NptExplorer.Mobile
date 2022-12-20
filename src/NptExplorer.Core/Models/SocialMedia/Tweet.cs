using System;
using System.Web;

namespace NptExplorer.Core.Models.SocialMedia
{
	public class Tweet
	{
		public string Id { get; set; }
		public string Content { get; set; }
		public DateTime PostedDateTime { get; set; }
		public string DisplayPostedDate { get; set; }
		public string? UserProfileImageUrl { get; set; }
		public string? UserName { get; set; }
		public string? UserTwitterHandle { get; set; }
		public string PermalinkUrl { get; set; }

		public Tweet(Datum dto, Tweeter? user)
		{
			Id = dto.id;
			Content = HttpUtility.HtmlDecode(dto.text);
			PostedDateTime = dto.created_at;
			UserProfileImageUrl = user?.ProfileImageUrl;
			UserName = user?.Name;
			UserTwitterHandle = user == null ? null : $"@{user.Username}";
			PermalinkUrl = $"https://twitter.com/i/web/status/{Id}";

			if (PostedDateTime.Date == DateTime.Now.Date)
			{
				var ts = DateTime.Now.Subtract(PostedDateTime);
				DisplayPostedDate = ts.Hours > 0 ? $"{ts.Hours}h" : $"{ts.Minutes}m";
			}
			else
			{
				DisplayPostedDate = PostedDateTime.ToString("MMM d");
			}
		}
	}
}