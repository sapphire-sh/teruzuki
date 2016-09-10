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
        private static string getUrl(string path, NameValueCollection parameters)
        {
            string baseurl = "https://api.twitter.com/1.1/" + path + ".json";
            return Client.Instance.BuildURL(baseurl, parameters);
        }

        private static List<Tweet> GetTimeline(string path, NameValueCollection parameters)
        {
            string url = getUrl(path, parameters);
            return JsonConvert.DeserializeObject<List<Tweet>>(Client.Instance.Get(url));
        }
        private static Tweet GetTweet(string path, NameValueCollection parameters)
        {
            string url = getUrl(path, parameters);
            return JsonConvert.DeserializeObject<Tweet>(Client.Instance.Get(url));
        }

        private static NameValueCollection GetTimelineDefaultParameters(int count = 0, long since_id = 0, long max_id = 0)
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
            NameValueCollection parameters = GetTimelineDefaultParameters(count, since_id, max_id);
            return GetTimeline("statuses/mentions_timeline", parameters);
        }

        public static List<Tweet> UserTimeline(long user_id, int count = 0, long since_id = 0, long max_id = 0)
        {
            NameValueCollection parameters = GetTimelineDefaultParameters(count, since_id, max_id);
            parameters.Add("user_id", user_id.ToString());
            return GetTimeline("statuses/user_timeline", parameters);
        }

        public static List<Tweet> UserTimeline(string screen_name, int count = 0, long since_id = 0, long max_id = 0)
        {
            NameValueCollection parameters = GetTimelineDefaultParameters(count, since_id, max_id);
            parameters.Add("screen_name", screen_name);
            return GetTimeline("statuses/user_timeline", parameters);
        }

        public static List<Tweet> HomeTimeline(int count = 0, long since_id = 0, long max_id = 0)
        {
            NameValueCollection parameters = GetTimelineDefaultParameters(count, since_id, max_id);
            return GetTimeline("statuses/home_timeline", parameters);
        }

        public static List<Tweet> RetweetsOfMe(int count = 0, long since_id = 0, long max_id = 0)
        {
            NameValueCollection parameters = GetTimelineDefaultParameters(count, since_id, max_id);
            return GetTimeline("statuses/retweets_of_me", parameters);
        }

        public static List<Tweet> Retweets(long id, int count = 0)
        {
            NameValueCollection parameters = GetTimelineDefaultParameters(count, 0, 0);
            parameters.Add("id", id.ToString());
            return GetTimeline("statuses/retweets", parameters);
        }

        public static Tweet Show(long id)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("id", id.ToString());
            return GetTweet("statuses/show", parameters);
        }


        /*
         * Not yet implemeted
         * 
         * GET statuses/retweeters/ids - require User model
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
         * GET statuses/lookup - Getting 401 for some reason
         */
    }
}
