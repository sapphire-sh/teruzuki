using System;using System.Text;using System.Collections;using System.Collections.Generic;using UnityEngine;namespace teruzuki.Twitter{	public static class Helper	{		public static string BuildURL(string endpoint) {			return string.Format("{0}{1}.json", Constants.URL.BASE_URL, endpoint);		}		public static Dictionary<string, string> ParseQueryString(string queryString) {			var dict = new Dictionary<string, string> ();			var queries = queryString.Split('&');			foreach (var query in queries)			{
				var q = query.Split ('=');
				if (q.Length == 2)
				{
					dict.Add (q[0], q[1]);
				}			}			return dict;		}		public static string ComposeQueryString(Dictionary<string, string> queries) {			var stringBuilder = new StringBuilder ();			foreach (var query in queries) {				stringBuilder.Append (string.Format ("{0}={1}&", query.Key, query.Value));			}			return stringBuilder.ToString ().TrimEnd('&');		}	}}