using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using teruzuki.Twitter;

namespace teruzuki
{
	public class AccountManager : MonoBehaviour
	{
		public RectTransform content;
		public RectTransform accountItem;

		private IEnumerator initializeItemCoroutine;

		private Client client;
		private IEnumerator requestTokenCoroutine;

		void Start ()
		{
			client = new Client ();

			initializeItemCoroutine = InitializeItem ();
			StartCoroutine (initializeItemCoroutine);
		}

		private IEnumerator InitializeItem()
		{
			for(var i = 0; i < 20; ++i)
			{
				var item = Instantiate (accountItem);
				item.SetParent (content, false);
				
				yield return new WaitForSeconds (0.1f);
			}
			yield return null;
		}

		private void RequestTokenCallback (string url)
		{
			Application.OpenURL (url);
		}

		private void AccessTokenCallback ()
		{
			SceneManager.LoadScene ("main");
		}

		public void UIAddNewAccount ()
		{
			if (requestTokenCoroutine == null)
			{
				requestTokenCoroutine = client.AcquireRequestToken (RequestTokenCallback);
				StartCoroutine (requestTokenCoroutine);
			}
		}
	}
}
