using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

using teruzuki.Twitter;
using teruzuki.Twitter.Parameters.Statuses;
using teruzuki.Twitter.Model;

namespace teruzuki
{
	public class MainController : MonoBehaviour
	{
		public RectTransform content;
		public RectTransform tweetItem;
		
		private IEnumerator fetchHomeTimelineCoroutine;
		private UInt64 lastTweetId = 0;

		void Start ()
		{
			if (AccountManager.Instance.CurrentAccount == null)
			{
				SceneManager.LoadScene ("account");
			}
			else
			{
				if(fetchHomeTimelineCoroutine == null)
				{
					fetchHomeTimelineCoroutine = FetchHomeTimeline ();
					StartCoroutine (fetchHomeTimelineCoroutine);
				}
			}
		}

		private IEnumerator FetchHomeTimeline ()
		{
			while (true)
			{
				StartCoroutine (Twitter.API.Statuses.HomeTimeline (AccountManager.Instance.CurrentAccount.Client, new HomeTimelineParameters (), FetchHomeTimelineCallback));
				yield return new WaitForSeconds (60.0f);
			}
		}

		private void FetchHomeTimelineCallback (List<Tweet> tweets)
		{
			tweets.Reverse ();
			tweets.ToList().ForEach (x =>
			{
					var tweetId = UInt64.Parse(x.id_str);
					if(lastTweetId < tweetId) {
						var item = Instantiate (tweetItem);
						item.SetParent(content, false);
						item.SetAsFirstSibling();
						item.GetComponent<UITweetItem> ().Initialize (x);

						lastTweetId = tweetId;
					}
			});
		}

/*		private void FetchMentionsTimeline ()
		{
			StartCoroutine (Twitter.API.Statuses.MentionsTimeline (AccountManager.Instance.CurrentAccount.Client, new MentionsTimelineParameters (), FetchMentionsTimelineCallback));
		}

		private void ComposeTweet (string text)
		{
			var parameters = new UpdateParameters (text);
			StartCoroutine (Twitter.API.Statuses.Update (AccountManager.Instance.CurrentAccount.Client, parameters, ComposeTweetCallback));
		}

		private void ComposeTweetCallback (Tweet res)
		{
			InstantiateTweet (res);
			Debug.Log (res.text);
		}

		private void ShowTweet (UInt64 id)
		{
			var parameters = new ShowParameters (id);
			StartCoroutine (Twitter.API.Statuses.Show (AccountManager.Instance.CurrentAccount.Client, parameters, ShowTweetCallback));
		}

		private void ShowTweetCallback (Tweet res)
		{
			InstantiateTweet (res);
		}

		private void FetchMentionsTimelineCallback (List<Tweet> res)
		{
			res.ForEach (x => InstantiateTweet (x));
		}

		private void InstantiateTweet (Tweet tweet)
		{
			var obj = Instantiate (tweetPrefab);
			var mesh = obj.GetComponentInChildren<TextMesh> ();
			mesh.text = tweet.text;
			var bounds = obj.GetComponentInChildren<Renderer> ().bounds;
			obj.transform.position = new Vector3 (0, 5, 0);
		}

		private void SignOut ()
		{
			AccountManager.Instance.CurrentAccount = null;

			SceneManager.LoadScene ("login");
		}*/
	}
}
