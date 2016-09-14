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
			if (SessionManager.Instance.SessionList.Count == 0) {
				SceneManager.LoadScene ("login");
			}
		}

		public void SendTweet(string status) {
			
		}

		void OnGUI() {
//			if(GUI.Button(new Rect(10, Screen.height - 100, 200, 40), "HomeTimeline")) {
//				FetchHomeTimeline ();
//			}
//
//			if(GUI.Button(new Rect(10, Screen.height - 50, 200, 40), "MentionsTimeline")) {
//				FetchMentionsTimeline ();
//			}
//
//			if (GUI.Button (new Rect (Screen.width - 210, Screen.height - 50, 200, 40), "Compose Tweet")) {
//				ComposeTweet ();
//			}
		}

		private void Log(string str) {
			Debug.Log (str);
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

//		private void ComposeTweet() {
//			var s = new Twitter.API.Statuses ();
//			StartCoroutine (Twitter.Client.Instance.POST ("https://api.twitter.com/1.1/statuses/update.json", Log));
//		}
    }
}
