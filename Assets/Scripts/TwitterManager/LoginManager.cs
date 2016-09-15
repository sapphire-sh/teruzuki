using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using teruzuki.Twitter;

namespace teruzuki
{
	public class LoginManager : MonoBehaviour
	{
		private string pin = "";

		private TwitterClient test;

		void Start ()
		{
			test = new TwitterClient ();
			if (ClientManager.Instance.ClientList.Count > 0) {
				SceneManager.LoadScene ("main");
			}
			else {
//				var res = test.oauth.AcquireRequestToken (Constants.URL.REQUEST_TOKEN, "POST");
//				Debug.Log ("https://api.twitter.com/oauth/authenticate?oauth_token=" + res["oauth_token"]);
//				Application.OpenURL ("https://api.twitter.com/oauth/authenticate?oauth_token=" + res["oauth_token"]);
				var client = new TwitterClient ();
				ClientManager.Instance.ClientList.Add (client);
				StartCoroutine (client.AcquireRequestToken (RequestTokenCallback));
			}
		}

		void OnGUI ()
		{
			pin = GUI.TextField (new Rect (10, 10, 400, 40), pin);

			if (GUI.Button (new Rect (10, 60, 400, 40), "Submit")) {
//				var res = test.oauth.AcquireAccessToken (Constants.URL.ACCESS_TOKEN, "POST", pin);
//				Debug.Log (res.AllText);
				StartCoroutine (ClientManager.Instance.CurrentClient.AcquireAccessToken (pin, AccessTokenCallback));
			}
		}

		public void Configuration(Twitter.Model.Configuration res) {
			Debug.Log (res);
		}

		public void RequestTokenCallback(string url) {
			Application.OpenURL (url);
		}

		public void AccessTokenCallback() {
			SceneManager.LoadScene ("main");
		}
	}
}
