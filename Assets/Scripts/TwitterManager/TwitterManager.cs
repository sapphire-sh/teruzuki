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

		private string text = "";

		void OnGUI ()
		{
			if (GUI.Button (new Rect (10, Screen.height - 100, 200, 40), "HomeTimeline")) {
				FetchHomeTimeline ();
			}

			if (GUI.Button (new Rect (10, Screen.height - 50, 200, 40), "MentionsTimeline")) {
				FetchMentionsTimeline ();
			}

			text = GUI.TextField (new Rect (Screen.width - 210, Screen.height - 150, 200, 40), text);

			if (GUI.Button (new Rect (Screen.width - 210, Screen.height - 50, 200, 40), "Compose Tweet")) {
				ComposeTweet (text);
			}

			if (GUI.Button (new Rect (Screen.width - 210, Screen.height - 100, 200, 40), "Show Tweet")) {
				ShowTweet (UInt64.Parse (text));
			}

			if (GUI.Button (new Rect (Screen.width - 210, 10, 200, 40), "Sign Out")) {
				SignOut ();
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

		private void ComposeTweet (string text)
		{
			var parameters = new UpdateParameters (text);
			StartCoroutine (Twitter.API.Statuses.Update (ClientManager.Instance.CurrentClient, parameters, ComposeTweetCallback));
		}

		private void ComposeTweetCallback (Tweet res)
		{
			InstantiateTweet (res);
			Debug.Log (res.text);
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
			var bounds = obj.GetComponentInChildren<Renderer> ().bounds;
			Debug.Log (bounds);
			obj.transform.position = new Vector3 (0, 5, 0);
		}

		private void SignOut() {
			ClientManager.Instance.ClientList.Remove (ClientManager.Instance.CurrentClient);
			ClientManager.Instance.SaveClient ();
			SceneManager.LoadScene ("login");
		}
	}
}
