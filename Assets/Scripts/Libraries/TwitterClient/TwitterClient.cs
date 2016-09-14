using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using UnityEngine;

using LitJson;

namespace teruzuki.Twitter
{
	[System.Serializable]
	public class TwitterClient
	{
		private OAuth.Manager oauth;

		public TwitterClient()
		{
			oauth = new OAuth.Manager();
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
		}

		public IEnumerator GET<T>(string url, Action<T> callback)
		{
			Dictionary<string, string> headers = new Dictionary<string, string> ();
			headers.Add("Authorization", oauth.GenerateAuthzHeader(url, "GET"));

			WWW www = new WWW ("http://sapphire.sh:3000/", null, headers);
			yield return www;

			Debug.Log (www.text);
			callback (JsonMapper.ToObject<T>(www.text));
		}

		public IEnumerator POST<T>(string url, Action<T> callback) {
			Dictionary<string, string> headers = new Dictionary<string, string> ();
			headers.Add ("Authorization", oauth.GetAuthorizationHeader (url, "POST"));//oauth.GenerateAuthzHeader (url, "POST"));
			Debug.Log (headers ["Authorization"]);

			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("status", "test");
			WWW www = new WWW (url, wwwForm.data, headers);
			//WWW www = new WWW ("http://sapphire.sh:3000/", wwwForm.data, headers);
			yield return www;

			Debug.Log (www.text);
			Debug.Log (www.url);

			callback ((T)(object)www.text);
			//callback (JsonMapper.ToObject<T>(www.text));
		}

		private static string BuildUrl(string baseurl, string key, string value)
		{
			NameValueCollection parameter = new NameValueCollection();
			parameter.Add(key, value);
			return BuildUrl(baseurl, parameter);
		}

		private static string BuildUrl(string baseurl, NameValueCollection parameters)
		{
			StringBuilder q = new StringBuilder();

			foreach (string key in parameters)
			{
				q.Append((q.Length == 0) ? '?' : '&');
				q.Append(key);
				q.Append('=');
				q.Append(WWW.EscapeURL(parameters[key]));
			}

			return baseurl + q.ToString();
		}

		public static string GetApiUrl(string path, NameValueCollection parameters)
		{
			string baseurl = "https://api.twitter.com/1.1/" + path + ".json";
			return BuildUrl(baseurl, parameters);
		}
	}
}
