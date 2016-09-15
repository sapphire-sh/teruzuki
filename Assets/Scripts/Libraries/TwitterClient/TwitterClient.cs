using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using UnityEngine;

using LitJson;

namespace teruzuki.Twitter
{
	[System.Serializable]
	public class TwitterClient
	{
		public OAuth.Manager oauth;

		public TwitterClient()
		{
			oauth = new OAuth.Manager();
			oauth["consumer_key"] = "OcbDuSiWrHYWU2RFgdWyV61F8";
			oauth["consumer_secret"] = "7fNW3QITGNFQAisvtkk8yaHdXkx5j7mxM2rEJShUeqxbwZEDHZ";
		}

		public IEnumerator AcquireRequestToken(Action<string> callback) {
			Dictionary<string, string> headers = new Dictionary<string, string> ();
			headers.Add ("Authorization", oauth.GenerateAuthzHeader (Constants.URL.REQUEST_TOKEN, "POST"));

			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("", "");

			WWW www = new WWW (Constants.URL.REQUEST_TOKEN, wwwForm.data, headers);
			yield return www;

			var query = Helper.ParseQueryString (www.text);

			oauth._params ["token"] = query ["oauth_token"];

			callback (string.Format ("https://api.twitter.com/oauth/authenticate?oauth_token={0}", query ["oauth_token"]));
		}

		public IEnumerator AcquireAccessToken(string pin, Action callback) {
			oauth.NewRequest ();
			oauth._params ["verifier"] = pin;
			Dictionary<string, string> headers = new Dictionary<string, string> ();
			headers.Add ("Authorization", oauth.GetAuthorizationHeader (Constants.URL.ACCESS_TOKEN, "POST"));

			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField("", "");

			WWW www = new WWW (Constants.URL.ACCESS_TOKEN, wwwForm.data, headers);
			yield return www;

			var query = Helper.ParseQueryString (www.text);
			oauth ["token"] = query ["oauth_token"];
			oauth ["token_secret"] = query ["oauth_token_secret"];

			callback ();
		}

		public IEnumerator GET<T>(string url, Action<T> callback)
		{
			Dictionary<string, string> headers = new Dictionary<string, string> ();
			headers.Add("Authorization", oauth.GenerateAuthzHeader(url, "GET"));

			WWW www = new WWW (url, null, headers);
			yield return www;

			callback (JsonMapper.ToObject<T>(www.text));
		}

		public IEnumerator POST<T>(string url, Dictionary<string, string> query, Action<T> callback) {
			Dictionary<string, string> headers = new Dictionary<string, string> ();
			headers.Add ("Authorization", oauth.GenerateAuthzHeader (url, "POST"));

			WWWForm wwwForm = new WWWForm ();
			foreach (var pair in query) {
				wwwForm.AddField (pair.Key, pair.Value);
			}

			WWW www = new WWW (url, wwwForm.data, headers);
			yield return www;

			Debug.Log (www.text);

			callback (JsonMapper.ToObject<T>(www.text));
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
