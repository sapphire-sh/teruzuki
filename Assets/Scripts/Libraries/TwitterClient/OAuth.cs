using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

using teruzuki.Twitter.Parameters;

namespace teruzuki.Twitter
{
	public class OAuth
	{
		private Dictionary<String, String> parameters;

		public string OAuthCallback { get { return parameters ["oauth_callback"]; } set { parameters ["oauth_callback"] = value; } }

		public string OAuthConsumerKey { get { return parameters ["oauth_consumer_key"]; } set { parameters ["oauth_consumer_key"] = value; } }

		public string OAuthConsumerSecret { get { return parameters ["oauth_consumer_secret"]; } set { parameters ["oauth_consumer_secret"] = value; } }

		public string OAuthTimestamp { get { return parameters ["oauth_timestamp"]; } set { parameters ["oauth_timestamp"] = value; } }

		public string OAuthNonce { get { return parameters ["oauth_nonce"]; } set { parameters ["oauth_nonce"] = value; } }

		public string OAuthSignatureMethod { get { return parameters ["oauth_signature_method"]; } set { parameters ["oauth_signature_method"] = value; } }

		public string OAuthSignature { get { return parameters ["oauth_signature"]; } set { parameters ["oauth_signature"] = value; } }

		public string OAuthToken { get { return parameters ["oauth_token"]; } set { parameters ["oauth_token"] = value; } }

		public string OAuthTokenSecret { get { return parameters ["oauth_token_secret"]; } set { parameters ["oauth_token_secret"] = value; } }

		public string OAuthVersion { get { return parameters ["oauth_version"]; } set { parameters ["oauth_version"] = value; } }

		public string OAuthVerifier { get { return parameters ["oauth_verifier"]; } set { parameters ["oauth_verifier"] = value; } }

		public OAuth () : this ("", "")
		{
		}

		public OAuth (string accessToken, string accessTokenSecret)
		{
			this.parameters = new Dictionary<string, string> ();
			this.OAuthCallback = "oob";
			this.OAuthConsumerKey = Constants.Key.CONSUMER_KEY;
			this.OAuthConsumerSecret = Constants.Key.CONSUMER_SECRET;
			this.OAuthTimestamp = GenerateTimestamp ();
			this.OAuthNonce = GenerateNonce ();
			this.OAuthSignatureMethod = "HMAC-SHA1";
			this.OAuthSignature = "";
			this.OAuthToken = accessToken;
			this.OAuthTokenSecret = accessTokenSecret;
			this.OAuthVersion = "1.0";
			this.OAuthVerifier = "";
		}

		private string GenerateTimestamp ()
		{
			return Math.Floor ((DateTime.UtcNow - new DateTime (1970, 1, 1)).TotalSeconds).ToString ();
		}

		private static SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider ();

		private string GenerateNonce ()
		{
			return BitConverter.ToString (sha1.ComputeHash (Encoding.ASCII.GetBytes (GenerateTimestamp ()))).Replace ("-", "").ToLower ();
		}

		private Dictionary<String, String> ExtractQueryParameters (string queryString)
		{
			if (queryString.StartsWith ("?"))
				queryString = queryString.Remove (0, 1);

			var result = new Dictionary<String, String> ();

			if (string.IsNullOrEmpty (queryString))
				return result;

			foreach (string s in queryString.Split('&')) {
				if (!string.IsNullOrEmpty (s) && !s.StartsWith ("oauth_")) {
					if (s.IndexOf ('=') > -1) {
						string[] temp = s.Split ('=');
						result.Add (temp [0], temp [1]);
					} else
						result.Add (s, string.Empty);
				}
			}

			return result;
		}

		public static string UrlEncode (string value)
		{
			var result = new System.Text.StringBuilder ();
			foreach (char symbol in value) {
				if (unreservedChars.IndexOf (symbol) != -1)
					result.Append (symbol);
				else
					result.Append ('%' + String.Format ("{0:X2}", (int)symbol));
			}
			return result.ToString ();
		}

		private static string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

		private static string EncodeRequestParameters (Dictionary<string, string> parameters)
		{
			var stringBuilder = new StringBuilder ();
			parameters.ToList ().FindAll (x => {
				return (!x.Key.EndsWith ("secret") && !string.IsNullOrEmpty (x.Value));
			}).ForEach (x => {
				stringBuilder.Append (string.Format ("{0}=\"{1}\", ", x.Key, WWW.EscapeURL (x.Value)));
			});
			return stringBuilder.ToString ().TrimEnd (' ').TrimEnd (',');
		}

		public string GetAuthorizationHeader (string uri, string method, ITwitterParameters parameters)
		{
			this.OAuthTimestamp = GenerateTimestamp ();
			this.OAuthNonce = GenerateNonce ();
			this.OAuthSignature = CalculateSignature (uri, method, parameters);

			return string.Format ("OAuth {0}", EncodeRequestParameters (this.parameters));
		}

		private string CalculateSignature (string uri, string method, ITwitterParameters parameters)
		{
			return Convert.ToBase64String (GetSigningKey ().ComputeHash (System.Text.Encoding.ASCII.GetBytes (GetSignatureBase (uri, method, parameters))));
		}

		private string GetSignatureBase (string url, string method, ITwitterParameters parameters)
		{
			var p = new Dictionary<string, string>();
			this.parameters.ToList ().ForEach (x => {
				if(!String.IsNullOrEmpty(x.Value) && !x.Key.EndsWith("_secret") && !x.Key.EndsWith("_signature")) {
					p.Add(x.Key, x.Value);
				}
			});
			if (parameters != null) {
				parameters.Queries.ToList ().ForEach (x => p.Add (x.Key, x.Value));
			}

			var stringBuilder = new StringBuilder ();
			p.OrderBy (x => x.Key).ToList ().ForEach (x => stringBuilder.AppendFormat ("{0}={1}&", UrlEncode (x.Key), UrlEncode (x.Value)));

			var result = string.Format ("{0}&{1}&{2}", method, UrlEncode (url), UrlEncode (stringBuilder.ToString ().TrimEnd ('&')));
			Debug.Log (result);
			return result;
		}

		private HashAlgorithm GetSigningKey ()
		{
			return new HMACSHA1 {
				Key = System.Text.Encoding.ASCII.GetBytes (string.Format ("{0}&{1}", this.OAuthConsumerSecret, this.OAuthTokenSecret))
			};
		}
	}
}
