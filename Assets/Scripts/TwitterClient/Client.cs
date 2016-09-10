using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using UnityEngine;

namespace teruzuki.Twitter
{
	public class Client
	{
		private static Client instance;

		private OAuth.Manager oauth;

		public static Client Instance
		{
			get
			{
				if(instance == null)
				{
					instance = new Client();
				}
				return instance;
			}
		}

		private Client()
		{
			oauth = new OAuth.Manager();
			oauth["consumer_key"] = "OcbDuSiWrHYWU2RFgdWyV61F8";
			oauth["consumer_secret"] = "7fNW3QITGNFQAisvtkk8yaHdXkx5j7mxM2rEJShUeqxbwZEDHZ";
		}

		public static string GetRequestToken()
		{
			var res = Instance.oauth.AcquireRequestToken("https://api.twitter.com/oauth/request_token", "POST");
			return "https://api.twitter.com/oauth/authenticate?oauth_token=" + res["oauth_token"];
		}

		public static void GetAccessToken(string pin)
		{
			var res = Instance.oauth.AcquireAccessToken("https://api.twitter.com/oauth/access_token", "POST", pin);

			Instance.oauth["token"] = res["oauth_token"];
			Instance.oauth["token_secret"] = res["oauth_token_secret"];
		}

		public string Get(string url)
		{
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

			req.Method = "GET";
			req.ServicePoint.Expect100Continue = false;
			req.ContentType = "x-www-form-urlencoded";

			req.Headers["Authorization"] = oauth.GenerateAuthzHeader(url, "GET");

			HttpWebResponse res = (HttpWebResponse)req.GetResponse();

			using (var reader = new StreamReader(res.GetResponseStream()))
			{
				string value = reader.ReadToEnd();
				return value;
			}
		}
	}
}
