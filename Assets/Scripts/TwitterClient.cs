using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using UnityEngine;

namespace teruzuki
{
	public class TwitterClient
	{
		private Manager oauth;

		public TwitterClient()
		{
			oauth = new Manager();
			oauth["consumer_key"] = "OcbDuSiWrHYWU2RFgdWyV61F8";
			oauth["consumer_secret"] = "7fNW3QITGNFQAisvtkk8yaHdXkx5j7mxM2rEJShUeqxbwZEDHZ";
		}

		public void GetRequestToken()
		{
			var res = oauth.AcquireRequestToken("https://api.twitter.com/oauth/request_token", "POST");
			var url = "https://api.twitter.com/oauth/authenticate?oauth_token=" + res["oauth_token"];
			Debug.Log(url);
		}

		public void GetAccessToken(string pin)
		{
			var accessToken = oauth.AcquireAccessToken("https://api.twitter.com/oauth/access_token", "POST", pin);
			Debug.Log(accessToken.AllText);
		}
	}
}
