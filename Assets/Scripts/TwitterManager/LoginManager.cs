using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using teruzuki.Twitter;

namespace teruzuki
{
	public class LoginManager : MonoBehaviour
	{
		private string PIN = "";

		void Start ()
		{
			//SessionManager.Instance.LoadSession ();
			if (SessionManager.Instance.SessionList.Count > 0) {
				SceneManager.LoadScene ("main");
			}
			else {
				Debug.Log (1);
				var client = new TwitterClient ();
				var s = client.GetRequestToken ();
				Debug.Log (s);
				StartCoroutine (Twitter.API.OAuth.RequestToken (new TwitterClient(), RequestToken));
				//StartCoroutine (Twitter.API.Help.Configuration (client, Configuration));
			}
		}

		void OnGUI ()
		{
			PIN = GUI.TextField (new Rect (10, 10, 400, 40), PIN);

//			if (GUI.Button (new Rect (10, 60, 400, 40), "Submit")) {
//				var account = Twitter.Client.Instance.GetAccessToken(PIN);
//				SessionManager.Instance.SessionList.Add (account);
//				SessionManager.Instance.SaveSession ();
//				SceneManager.LoadScene ("timeline");
//			}
		}

		public void Configuration(Twitter.Model.Configuration res) {
			Debug.Log (res);
		}

		public void RequestToken(string res) {
//			Application.OpenURL ();
			Debug.Log (res);
		}
	}
}
