using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using teruzuki.Twitter;
using teruzuki.Twitter.Model;

namespace teruzuki
{
	public class TwitterManager : MonoBehaviour
	{
		public GameObject tweetPrefab;

		void Start() {
			if (ClientManager.Instance.ClientList.Count == 0) {
				SceneManager.LoadScene ("login");
			}
		}

		private string id_str = "";

		void OnGUI() {
			if(GUI.Button(new Rect(10, Screen.height - 100, 200, 40), "HomeTimeline")) {
				FetchHomeTimeline ();
			}
//
//			if(GUI.Button(new Rect(10, Screen.height - 50, 200, 40), "MentionsTimeline")) {
//				FetchMentionsTimeline ();
			//			}

			if (GUI.Button (new Rect (Screen.width - 210, Screen.height - 50, 200, 40), "Compose Tweet")) {
				ComposeTweet ();
			}

			id_str = GUI.TextField (new Rect (Screen.width - 210, Screen.height - 150, 200, 40), id_str);

			if (GUI.Button (new Rect (Screen.width - 210, Screen.height - 100, 200, 40), "Show Tweet")) {
				ShowTweet (id_str);
			}
		}

		private void FetchHomeTimeline()
		{
			StartCoroutine (Twitter.API.Statuses.HomeTimeline (ClientManager.Instance.CurrentClient, new Dictionary<string, string>(), FetchHomeTimelineCallback));
//			s.HomeTimeline();
//			for (int i = 0; i < tweets.Count; ++i) {
//				var tweet = tweets [i];
//				var obj = Instantiate(tweetPrefab);
//				var mesh = obj.GetComponentInChildren<TextMesh>();
//				mesh.text = tweet.text;
//				obj.transform.position = new Vector3(0, 5 + i, 0);
//				++i;
//			}
		}

		private void FetchHomeTimelineCallback(List<Tweet> res) {
			foreach (var tweet in res) {
				Debug.Log (tweet.text);
			}
		}
//
//		private void FetchMentionsTimeline()
//		{
//			var s = new Twitter.API.Statuses();
//			s.MentionsTimeline ();
//			for (int i = 0; i < tweets.Count; ++i) {
//				var tweet = tweets [i];
//				var obj = Instantiate(tweetPrefab);
//				var mesh = obj.GetComponentInChildren<TextMesh>();
//				mesh.text = tweet.text;
//				obj.transform.position = new Vector3(0, 5 + i, 0);
//				++i;
//			}
//		}

		private void ComposeTweet() {
			StartCoroutine (Twitter.API.Statuses.Update (ClientManager.Instance.CurrentClient, new Dictionary<string, string>(), ComposeTweetCallback));
		}

		private void ComposeTweetCallback(string res) {
			Debug.Log (res);
		}

		private void ShowTweet(string id_str) {
			var queries = new Dictionary<string, string> ();
			queries.Add ("id", id_str);
			StartCoroutine (Twitter.API.Statuses.Show (ClientManager.Instance.CurrentClient, queries, ShowTweetCallback));
		}

		private void ShowTweetCallback(Tweet res) {
			var obj = Instantiate (tweetPrefab);
			var mesh = obj.GetComponentInChildren<TextMesh>();
			mesh.text = res.text;
			obj.transform.position = new Vector3(0, 5, 0);
		}
    }
}
