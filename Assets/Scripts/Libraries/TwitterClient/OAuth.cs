using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace teruzuki.OAuth
{
	public class Manager
	{
		public Manager ()
		{
			_random = new System.Random ();
			_params = new Dictionary<String, String> ();
			_params ["callback"] = "oob";
			_params ["consumer_key"] = "";
			_params ["consumer_secret"] = "";
			_params ["timestamp"] = GenerateTimeStamp ();
			_params ["nonce"] = GenerateNonce ();
			_params ["signature_method"] = "HMAC-SHA1";
			_params ["signature"] = "";
			_params ["token"] = "";
			_params ["token_secret"] = "";
			_params ["version"] = "1.0";
		}

		public Manager (string consumerKey, string consumerSecret, string token, string tokenSecret) : this ()
		{
			_params ["consumer_key"] = consumerKey;
			_params ["consumer_secret"] = consumerSecret;
			_params ["token"] = token;
			_params ["token_secret"] = tokenSecret;
		}

		public string this [string ix] {
			get {
				if (_params.ContainsKey (ix))
					return _params [ix];
				throw new ArgumentException (ix);
			}
			set {
				if (!_params.ContainsKey (ix))
					throw new ArgumentException (ix);
				_params [ix] = value;
			}
		}

		private string GenerateTimeStamp ()
		{
			TimeSpan ts = DateTime.UtcNow - _epoch;
			return Convert.ToInt64 (ts.TotalSeconds).ToString ();
		}

		public void NewRequest ()
		{
			_params ["nonce"] = GenerateNonce ();
			_params ["timestamp"] = GenerateTimeStamp ();
		}

		private string GenerateNonce ()
		{
			var sb = new System.Text.StringBuilder ();
			for (int i = 0; i < 8; i++) {
				int g = _random.Next (3);
				switch (g) {
				case 0:
						// lowercase alpha
					sb.Append ((char)(_random.Next (26) + 97), 1);
					break;
				default:
						// numeric digits
					sb.Append ((char)(_random.Next (10) + 48), 1);
					break;
				}
			}
			return sb.ToString ();
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

		private static string EncodeRequestParameters (ICollection<KeyValuePair<String, String>> p)
		{
			var sb = new System.Text.StringBuilder ();
			foreach (KeyValuePair<String, String> item in p.OrderBy(x => x.Key)) {
				if (!String.IsNullOrEmpty (item.Value) && !item.Key.EndsWith ("secret")) {
					sb.AppendFormat ("oauth_{0}=\"{1}\", ", item.Key, WWW.EscapeURL (item.Value));
				}
			}

			return sb.ToString ().TrimEnd (' ').TrimEnd (',');
		}

		public string GenerateCredsHeader (string uri, string method, string realm)
		{
			NewRequest ();
			var authzHeader = GetAuthorizationHeader (uri, method, realm);
			return authzHeader;
		}

		public string GenerateAuthzHeader (string uri, string method)
		{
			NewRequest ();
			var authzHeader = GetAuthorizationHeader (uri, method, null);
			return authzHeader;
		}

		public string GetAuthorizationHeader (string uri, string method)
		{
			return GetAuthorizationHeader (uri, method, null);
		}

		private string GetAuthorizationHeader (string uri, string method, string realm)
		{
			if (string.IsNullOrEmpty (this._params ["consumer_key"]))
				throw new ArgumentNullException ("consumer_key");

			if (string.IsNullOrEmpty (this._params ["signature_method"]))
				throw new ArgumentNullException ("signature_method");

			Sign (uri, method);

			var erp = EncodeRequestParameters (this._params);
			return (String.IsNullOrEmpty (realm)) ? "OAuth " + erp : String.Format ("OAuth realm=\"{0}\", ", realm) + erp;
		}

		private void Sign (string uri, string method)
		{
			var signatureBase = GetSignatureBase (uri, method);
			var hash = GetHash ();

			byte[] dataBuffer = System.Text.Encoding.ASCII.GetBytes (signatureBase);
			byte[] hashBytes = hash.ComputeHash (dataBuffer);

			this ["signature"] = Convert.ToBase64String (hashBytes);
		}

		private string GetSignatureBase (string url, string method)
		{
			// normalize the URI
			var uri = new Uri (url);
			var normUrl = string.Format ("{0}://{1}", uri.Scheme, uri.Host);
			if (!((uri.Scheme == "http" && uri.Port == 80) || (uri.Scheme == "https" && uri.Port == 443)))
				normUrl += ":" + uri.Port;

			normUrl += uri.AbsolutePath;

			// the sigbase starts with the method and the encoded URI
			var sb = new System.Text.StringBuilder ();
			sb.Append (method).Append ('&').Append (UrlEncode (normUrl)).Append ('&');

			var p = ExtractQueryParameters (uri.Query);
			foreach (var p1 in this._params) {
				if (!String.IsNullOrEmpty (this._params [p1.Key]) && !p1.Key.EndsWith ("_secret") && !p1.Key.EndsWith ("signature"))
					p.Add ("oauth_" + p1.Key, p1.Value);
			}

			var sb1 = new System.Text.StringBuilder ();
			foreach (KeyValuePair<String, String> item in p.OrderBy(x => x.Key)) {
				sb1.AppendFormat ("{0}={1}&", item.Key, item.Value);
			}

			sb.Append (UrlEncode (sb1.ToString ().TrimEnd ('&')));
			var result = sb.ToString ();
			return result;
		}

		private HashAlgorithm GetHash ()
		{
			if (this ["signature_method"] != "HMAC-SHA1")
				throw new NotImplementedException ();

			string keystring = string.Format ("{0}&{1}", WWW.EscapeURL (this ["consumer_secret"]), WWW.EscapeURL (this ["token_secret"]));
			var hmacsha1 = new HMACSHA1 {
				Key = System.Text.Encoding.ASCII.GetBytes (keystring)
			};
			return hmacsha1;
		}

		private static readonly DateTime _epoch = new DateTime (1970, 1, 1, 0, 0, 0, 0);
		public Dictionary<String, String> _params;
		private System.Random _random;
	}
}
