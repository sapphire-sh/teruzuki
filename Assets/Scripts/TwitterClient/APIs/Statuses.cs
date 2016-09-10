using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JsonFx.Json;

namespace teruzuki.Twitter
{
	public static class Statuses
	{
		public static List<Tweet> MentionsTimeline()
		{
			var reader = new JsonReader();
			return reader.Read<List<Tweet>>(Client.Instance.Get("https://api.twitter.com/1.1/statuses/mentions_timeline.json"));
		}

		public static List<Tweet> HomeTimeline()
		{
			var reader = new JsonReader();
			return reader.Read<List<Tweet>>(Client.Instance.Get("https://api.twitter.com/1.1/statuses/home_timeline.json"));
		}
	}
}
