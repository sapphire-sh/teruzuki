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

		void Start ()
		{
			//SessionManager.Instance.LoadSession ();
			if (ClientManager.Instance.ClientList.Count > 0) {
				SceneManager.LoadScene ("main");
			}
			else {
				var client = new TwitterClient ();
				ClientManager.Instance.ClientList.Add (client);
				StartCoroutine (client.AcquireRequestToken (RequestTokenCallback));
			}
		}

		void OnGUI ()
		{
			pin = GUI.TextField (new Rect (10, 10, 400, 40), pin);

			if (GUI.Button (new Rect (10, 60, 400, 40), "Submit")) {
				StartCoroutine (ClientManager.Instance.CurrentClient.AcquireAccessToken (pin, AccessTokenCallback));
//				SessionManager.Instance.SessionList.Add (account);
//				SessionManager.Instance.SaveSession ();
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
