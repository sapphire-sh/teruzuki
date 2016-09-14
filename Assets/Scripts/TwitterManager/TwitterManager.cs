using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using teruzuki.Twitter;

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

		void OnGUI() {
//			if(GUI.Button(new Rect(10, Screen.height - 100, 200, 40), "HomeTimeline")) {
//				FetchHomeTimeline ();
//			}
//
//			if(GUI.Button(new Rect(10, Screen.height - 50, 200, 40), "MentionsTimeline")) {
//				FetchMentionsTimeline ();
//			}

			if (GUI.Button (new Rect (Screen.width - 210, Screen.height - 50, 200, 40), "Compose Tweet")) {
				ComposeTweet ();
			}
		}

//		private void FetchHomeTimeline()
//		{
//			var s = new Twitter.API.Statuses();
//			StartCoroutine (Twitter.Client.Instance.GET ("https://api.twitter.com/1.1/statuses/home_timeline.json", Log));
////			s.HomeTimeline();
////			for (int i = 0; i < tweets.Count; ++i) {
////				var tweet = tweets [i];
////				var obj = Instantiate(tweetPrefab);
////				var mesh = obj.GetComponentInChildren<TextMesh>();
////				mesh.text = tweet.text;
////				obj.transform.position = new Vector3(0, 5 + i, 0);
////				++i;
////			}
//		}
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
			StartCoroutine (Twitter.API.Statuses.Update (ClientManager.Instance.CurrentClient, ComposeTweetCallback));
		}

		private void ComposeTweetCallback(string res) {
			Debug.Log (res);
		}
    }
}
