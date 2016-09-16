using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using teruzuki.Twitter.Parameters.Statuses;
using teruzuki.Twitter.Model;

namespace teruzuki.Twitter.API
{
	public class Statuses
	{
		public static IEnumerator MentionsTimeline (TwitterClient client, MentionsTimelineParameters parameters, Action<List<Tweet>> callback)
		{
			yield return client.GET<List<Tweet>> (Helper.BuildURL ("statuses/mentions_timeline"), parameters, callback);
		}

		public static IEnumerator UserTimeline (TwitterClient client, UserTimelineParameters parameters, Action<List<Tweet>> callback)
		{
			yield return client.GET<List<Tweet>> (Helper.BuildURL ("statuses/user_timeline"), parameters, callback);
		}

		public static IEnumerator HomeTimeline (TwitterClient client, HomeTimelineParameters parameters, Action<List<Tweet>> callback)
		{
			yield return client.GET<List<Tweet>> (Helper.BuildURL ("statuses/home_timeline"), parameters, callback);
		}

		public static IEnumerator RetweetsOfMe (TwitterClient client, RetweetsOfMeParameters parameters, Action<List<Tweet>> callback)
		{
			yield return client.GET<List<Tweet>> (Helper.BuildURL ("statuses/retweets_of_me"), parameters, callback);
		}

		public static IEnumerator Retweets (TwitterClient client, RetweetsParameters parameters, Action<List<Tweet>> callback)
		{
			yield return client.GET<List<Tweet>> (Helper.BuildURL ("statuses/retweets"), parameters, callback);
		}

		public static IEnumerator Show (TwitterClient client, ShowParameters parameters, Action<Tweet> callback)
		{
			yield return client.GET<Tweet> (Helper.BuildURL ("statuses/show"), parameters, callback);
		}

		public static IEnumerator LookUp (TwitterClient client, LookUpParameters parameters, Action<List<Tweet>> callback)
		{
			yield return client.GET<List<Tweet>> (Helper.BuildURL ("statuses/lookup"), parameters, callback);
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
			* POST statuses/destroy/:id - require POST function */

		public static IEnumerator Update (TwitterClient client, UpdateParameters parameters, Action<Tweet> callback)
		{
			yield return client.POST<Tweet> (Helper.BuildURL ("statuses/update"), parameters, callback);
		}
		/* POST statuses/retweet/:id - require POST function
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
