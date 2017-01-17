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
		public static IEnumerator MentionsTimeline (Client client, MentionsTimelineParameters parameters, Action<List<Tweet>> callback)
		{
			yield return client.GET<List<Tweet>> (Helper.BuildRESTURL ("statuses/mentions_timeline"), parameters, callback);
		}

		public static IEnumerator UserTimeline (Client client, UserTimelineParameters parameters, Action<List<Tweet>> callback)
		{
			yield return client.GET<List<Tweet>> (Helper.BuildRESTURL ("statuses/user_timeline"), parameters, callback);
		}

		public static IEnumerator HomeTimeline (Client client, HomeTimelineParameters parameters, Action<List<Tweet>> callback)
		{
			yield return client.GET<List<Tweet>> (Helper.BuildRESTURL ("statuses/home_timeline"), parameters, callback);
		}

		public static IEnumerator RetweetsOfMe (Client client, RetweetsOfMeParameters parameters, Action<List<Tweet>> callback)
		{
			yield return client.GET<List<Tweet>> (Helper.BuildRESTURL ("statuses/retweets_of_me"), parameters, callback);
		}

		public static IEnumerator Retweets (Client client, RetweetsParameters parameters, Action<List<Tweet>> callback)
		{
			yield return client.GET<List<Tweet>> (Helper.BuildRESTURL ("statuses/retweets"), parameters, callback);
		}

		public static IEnumerator Show (Client client, ShowParameters parameters, Action<Tweet> callback)
		{
			yield return client.GET<Tweet> (Helper.BuildRESTURL ("statuses/show"), parameters, callback);
		}

		public static IEnumerator LookUp (Client client, LookUpParameters parameters, Action<List<Tweet>> callback)
		{
			yield return client.GET<List<Tweet>> (Helper.BuildRESTURL ("statuses/lookup"), parameters, callback);
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

		public static IEnumerator Update (Client client, UpdateParameters parameters, Action<Tweet> callback)
		{
			yield return client.POST<Tweet> (Helper.BuildRESTURL ("statuses/update"), parameters, callback);
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
