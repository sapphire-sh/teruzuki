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

		public string GetRequestToken()
		{
			var res = oauth.AcquireRequestToken("https://api.twitter.com/oauth/request_token", "POST");
			return "https://api.twitter.com/oauth/authenticate?oauth_token=" + res["oauth_token"];
		}

		public void GetAccessToken(string pin)
		{
			var res = oauth.AcquireAccessToken("https://api.twitter.com/oauth/access_token", "POST", pin);
			
			oauth["token"] = res["oauth_token"];
			oauth["token_secret"] = res["oauth_token_secret"];

			var url = "https://api.twitter.com/1.1/account/verify_credentials.json";
			var header = oauth.GenerateAuthzHeader(url, "GET");

			Debug.Log(Get(url, header));
		}

		public string Get(string requestUri, string authHeaders)
		{
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(requestUri);

			req.Method = "GET";
			req.ServicePoint.Expect100Continue = false;
			req.ContentType = "x-www-form-urlencoded";

			req.Headers["Authorization"] = authHeaders;



			HttpWebResponse res = (HttpWebResponse)req.GetResponse();

			using (var reader = new StreamReader(res.GetResponseStream()))
			{
				string value = reader.ReadToEnd();
				return value;
			}
		}
	}
}
