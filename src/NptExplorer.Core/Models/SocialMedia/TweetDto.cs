using System;
using System.Collections.Generic;

namespace NptExplorer.Core.Models.SocialMedia
{
	public class TweetDto
	{
		public List<Datum> data { get; set; }
		public Includes includes { get; set; }
		public Meta meta { get; set; }
	}

	public class Datum
	{
		public string author_id { get; set; }
		public string id { get; set; }
		public string text { get; set; }
		public List<string> edit_history_tweet_ids { get; set; }
		public DateTime created_at { get; set; }
	}

	public class Meta
	{
		public int result_count { get; set; }
		public string next_token { get; set; }
	}

	public class Includes
	{
		public List<User> users { get; set; }
	}

	public class User
	{
		public string username { get; set; }
		public string profile_image_url { get; set; }
		public string name { get; set; }
		public string id { get; set; }
	}
}