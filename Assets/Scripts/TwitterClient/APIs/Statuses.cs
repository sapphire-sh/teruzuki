using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace teruzuki.Twitter
{
	public static class Statuses
	{
        private static List<Tweet> Timeline(string type, int count, string since_id, string max_id)
        {
            NameValueCollection parameters = new NameValueCollection();
            if (count > 0)
                parameters.Add("count", count.ToString());
            if (since_id.Length > 0)
                parameters.Add("since_id", since_id.ToString());
            if (max_id.Length > 0)
                parameters.Add("max_id", max_id.ToString());
            string baseurl = "https://api.twitter.com/1.1/statuses/" + type + ".json";
            string url = Client.Instance.BuildURL(baseurl, parameters);
            return JsonConvert.DeserializeObject<List<Tweet>>(Client.Instance.Get(url));
        }

		public static List<Tweet> MentionsTimeline(int count = -1, string since_id = "", string max_id = "")
        {
            return Timeline("mentions_timeline", count, since_id, max_id);
        }

		public static List<Tweet> HomeTimeline(int count = -1, string since_id = "", string max_id = "")
		{
            return Timeline("home_timeline", count, since_id, max_id);
        }
        
    }
}
