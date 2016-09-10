using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace teruzuki
{
	public class TwitterManager : MonoBehaviour
	{
		Twitter.Client Client;

		void Start()
		{
			Client = Twitter.Client.Instance;
			Application.OpenURL(Client.GetRequestToken());
		}

		public void GetAccessToken(string pin)
		{
			Client.GetAccessToken(pin);
		}
	}
}
