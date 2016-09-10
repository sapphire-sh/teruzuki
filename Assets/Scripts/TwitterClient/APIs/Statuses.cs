using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace teruzuki.Twitter
{
	public static class Statuses
	{
		public static string MentionsTimeline()
		{
			return Client.Instance.Get("https://api.twitter.com/1.1/statuses/mentions_timeline.json");
		}
	}
}
