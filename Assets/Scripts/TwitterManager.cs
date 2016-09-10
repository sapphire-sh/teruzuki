using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace teruzuki
{
    public class TwitterManager : MonoBehaviour
    {
		TwitterClient Client;

        void Start()
        {
			Client = new TwitterClient();
			Application.OpenURL(Client.GetRequestToken());
        }

		public void GetAccessToken(string pin)
		{
			Client.GetAccessToken(pin);
		}
    }
}
