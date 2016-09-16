using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using UnityEngine;

using LitJson;

using teruzuki.Twitter.Parameters;

namespace teruzuki.Twitter
{
	public class TwitterClient
	{
		public OAuth oauth;

		public Credentials Credentials;

		public TwitterClient () : this ("", "")
		{
		}

		public TwitterClient (Credentials credentials) : this (credentials.AccessToken, credentials.AccessTokenSecret)
		{
		}

		public TwitterClient (string accessToken, string accessTokenSecret)
		{
			oauth = new OAuth (accessToken, accessTokenSecret);

			Credentials = new Credentials ();
			Credentials.AccessToken = accessToken;
			Credentials.AccessTokenSecret = accessTokenSecret;
		}

		public IEnumerator AcquireRequestToken (Action<string> callback)
		{
			Dictionary<string, string> headers = new Dictionary<string, string> ();
			headers.Add ("Authorization", oauth.GetAuthorizationHeader (Constants.URL.REQUEST_TOKEN, "POST"));

			Debug.Log (headers ["Authorization"]);

			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("", "");

			WWW www = new WWW (Constants.URL.REQUEST_TOKEN, wwwForm.data, headers);
			yield return www;

			Debug.Log (www.text);

			var query = Helper.ParseQueryString (www.text);

			oauth.OAuthToken = query ["oauth_token"];

			callback (string.Format ("https://api.twitter.com/oauth/authenticate?oauth_token={0}", query ["oauth_token"]));
		}

		public IEnumerator AcquireAccessToken (string pin, Action callback)
		{
			oauth.OAuthVerifier = pin;
			Dictionary<string, string> headers = new Dictionary<string, string> ();
			headers.Add ("Authorization", oauth.GetAuthorizationHeader (Constants.URL.ACCESS_TOKEN, "POST"));

			Debug.Log (headers ["Authorization"]);

			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("", "");

			WWW www = new WWW (Constants.URL.ACCESS_TOKEN, wwwForm.data, headers);
			yield return www;

			Debug.Log (www.text);

			var query = Helper.ParseQueryString (www.text);
			oauth.OAuthToken = Credentials.AccessToken = query ["oauth_token"];
			oauth.OAuthTokenSecret = Credentials.AccessTokenSecret = query ["oauth_token_secret"];

			callback ();
		}

		public IEnumerator GET<T> (string url, ITwitterParameters parameters, Action<T> callback)
		{
			url += parameters.ComposeQueryString ();

			Dictionary<string, string> headers = new Dictionary<string, string> ();
			headers.Add ("Authorization", oauth.GetAuthorizationHeader (url, "GET"));

			WWW www = new WWW (url, null, headers);
			yield return www;

			callback (JsonMapper.ToObject<T> (www.text));
		}

		public IEnumerator POST<T> (string url, ITwitterParameters parameters, Action<T> callback)
		{
			WWWForm wwwForm = new WWWForm ();
			parameters.Queries.ToList ().ForEach (x => {
				Debug.Log (x.Key + " " + x.Value);
				wwwForm.AddField (x.Key, x.Value);
			});

			Dictionary<string, string> headers = new Dictionary<string, string> ();
			headers.Add ("Authorization", oauth.GetAuthorizationHeader (url, "POST"));

			WWW www = new WWW (url, wwwForm.data, headers);
			yield return www;

			Debug.Log (www.text);

			callback (JsonMapper.ToObject<T> (www.text));
		}
	}
}
