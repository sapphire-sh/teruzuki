using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using teruzuki.Twitter.Model;

using Newtonsoft.Json;

namespace teruzuki.Twitter.API
{
	public static class Statuses
	{

		private static NameValueCollection GetTimelineCommonParameters(int count, long since_id, long max_id)
		{
			NameValueCollection parameters = new NameValueCollection();
			if (count > 0)
				parameters.Add("count", count.ToString());
			if (since_id > 0)
				parameters.Add("since_id", since_id.ToString());
			if (max_id > 0)
				parameters.Add("max_id", max_id.ToString());
			return parameters;
		}


		public static List<Tweet> MentionsTimeline(int count = 0, long since_id = 0, long max_id = 0)
		{
			NameValueCollection parameters = GetTimelineCommonParameters(count, since_id, max_id);
			return Client.GetTweets("statuses/mentions_timeline", parameters);
		}

		public static List<Tweet> UserTimeline(long user_id, int count = 0, long since_id = 0, long max_id = 0)
		{
			NameValueCollection parameters = GetTimelineCommonParameters(count, since_id, max_id);
			parameters.Add("user_id", user_id.ToString());
			return Client.GetTweets("statuses/user_timeline", parameters);
		}

		public static List<Tweet> UserTimeline(string screen_name, int count = 0, long since_id = 0, long max_id = 0)
		{
			NameValueCollection parameters = GetTimelineCommonParameters(count, since_id, max_id);
			parameters.Add("screen_name", screen_name);
			return Client.GetTweets("statuses/user_timeline", parameters);
		}

		public static List<Tweet> HomeTimeline(int count = 0, long since_id = 0, long max_id = 0)
		{
			NameValueCollection parameters = GetTimelineCommonParameters(count, since_id, max_id);
			return Client.GetTweets("statuses/home_timeline", parameters);
		}

		public static List<Tweet> RetweetsOfMe(int count = 0, long since_id = 0, long max_id = 0)
		{
			NameValueCollection parameters = GetTimelineCommonParameters(count, since_id, max_id);
			return Client.GetTweets("statuses/retweets_of_me", parameters);
		}

		public static List<Tweet> Retweets(long id, int count = 0)
		{
			NameValueCollection parameters = GetTimelineCommonParameters(count, 0, 0);
			parameters.Add("id", id.ToString());
			return Client.GetTweets("statuses/retweets", parameters);
		}

		public static Tweet Show(long id)
		{
			NameValueCollection parameters = new NameValueCollection();
			parameters.Add("id", id.ToString());
			return Client.GetTweet("statuses/show", parameters);
		}

		public static List<Tweet> Lookup(ICollection<long> id)
		{
			NameValueCollection parameters = new NameValueCollection();
			parameters.Add("id", String.Join("%2C", id.ToArray().Select(i => i.ToString()).ToArray()));
			return Client.GetTweets("statuses/lookup", parameters);
		}


		/*
			* Is this what we want?
			* 
		public static List<User> Retweeters(long id, long cursor = -1)
		{
			NameValueCollection parameters = new NameValueCollection();
			parameters.Add("id", id.ToString());
			parameters.Add("cursor", cursor.ToString());
			parameters.Add("stringify_ids", "true");

			string url = Client.GetApiUrl("statuses/retweeters", parameters);
			// JsonConvert.DeserializeObject<User>(Client.Instance.Get(url));
		}
		*/

		/*
			* Not yet implemeted
			* 
			* POST statuses/destroy/:id - require POST function
			* POST statuses/update - require POST function
			* POST statuses/retweet/:id - require POST function
			* POST statuses/unretweet/:id - require POST function
			* POST statuses/update_with_media - require POST function
			*/

		/*
			* Probably won't implement
			* 
			* GET statuses/oembed - Do we need this for real...?
			*/
	}
}
