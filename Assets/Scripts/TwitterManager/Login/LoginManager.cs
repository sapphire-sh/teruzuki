using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace teruzuki
{
	public class LoginManager : MonoBehaviour
	{
		void Start ()
		{
			SessionManager.Instance.LoadSession ();
			Debug.Log (SessionManager.Instance.SessionList.Count);
			if (SessionManager.Instance.SessionList.Count > 0) {
				SceneManager.LoadScene ("timeline");
			}
			else {
				Application.OpenURL(Twitter.Client.Instance.GetRequestToken());
			}
		}

		private string pin = "";

		void OnGUI ()
		{
			pin = GUI.TextField (new Rect (10, 10, 400, 40), pin);

			if (GUI.Button (new Rect (10, 60, 400, 40), "Submit")) {
				var account = Twitter.Client.Instance.GetAccessToken(pin);
				SessionManager.Instance.SessionList.Add (account);
				SessionManager.Instance.SaveSession ();
				SceneManager.LoadScene ("timeline");
			}
		}
	}
}
