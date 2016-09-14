using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace teruzuki.Twitter
{
	public static class Helper
	{
		public static Dictionary<string, string> ParseQueryString(string queryString) {
			var dict = new Dictionary<string, string> ();

			var queries = queryString.Split('&');

			foreach (var query in queries) {
				var q = query.Split ('=');
				dict.Add (q [0], q [1]);
			}

			return dict;
		}
	}
}

