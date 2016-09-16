using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using teruzuki.Twitter;
using teruzuki.Twitter.Parameters.Statuses;
using teruzuki.Twitter.Model;

namespace teruzuki
{
	public class TwitterManager : MonoBehaviour
	{
		public GameObject tweetPrefab;

		void Start ()
		{
			if (ClientManager.Instance.ClientList.Count == 0) {
				SceneManager.LoadScene ("login");
			}
		}

		private string id_str = "";

		void OnGUI ()
		{
			if (GUI.Button (new Rect (10, Screen.height - 100, 200, 40), "HomeTimeline")) {
				FetchHomeTimeline ();
			}

			if (GUI.Button (new Rect (10, Screen.height - 50, 200, 40), "MentionsTimeline")) {
				FetchMentionsTimeline ();
			}

			if (GUI.Button (new Rect (Screen.width - 210, Screen.height - 50, 200, 40), "Compose Tweet")) {
				ComposeTweet ();
			}

			id_str = GUI.TextField (new Rect (Screen.width - 210, Screen.height - 150, 200, 40), id_str);

			if (GUI.Button (new Rect (Screen.width - 210, Screen.height - 100, 200, 40), "Show Tweet")) {
				ShowTweet (UInt64.Parse (id_str));
			}
		}

		private void FetchHomeTimeline ()
		{
			StartCoroutine (Twitter.API.Statuses.HomeTimeline (ClientManager.Instance.CurrentClient, new HomeTimelineParameters (), FetchHomeTimelineCallback));
		}

		private void FetchHomeTimelineCallback (List<Tweet> res)
		{
			foreach (var tweet in res) {
				InstantiateTweet (tweet);
			}
		}

		private void FetchMentionsTimeline ()
		{
			StartCoroutine (Twitter.API.Statuses.MentionsTimeline (ClientManager.Instance.CurrentClient, new MentionsTimelineParameters (), FetchMentionsTimelineCallback));
		}

		private void ComposeTweet ()
		{
			StartCoroutine (Twitter.API.Statuses.Update (ClientManager.Instance.CurrentClient, new UpdateParameters (), ComposeTweetCallback));
		}

		private void ComposeTweetCallback (string res)
		{
			Debug.Log (res);
		}

		private void ShowTweet (UInt64 id)
		{
			var parameters = new ShowParameters (id);
			StartCoroutine (Twitter.API.Statuses.Show (ClientManager.Instance.CurrentClient, parameters, ShowTweetCallback));
		}

		private void ShowTweetCallback (Tweet res)
		{
			InstantiateTweet (res);
		}

		private void FetchMentionsTimelineCallback(List<Tweet> res) {
			res.ForEach (x => InstantiateTweet (x));
		}

		private void InstantiateTweet (Tweet tweet)
		{
			var obj = Instantiate (tweetPrefab);
			var mesh = obj.GetComponentInChildren<TextMesh> ();
			mesh.text = tweet.text;
			obj.transform.position = new Vector3 (0, 5, 0);
		}
	}
}
