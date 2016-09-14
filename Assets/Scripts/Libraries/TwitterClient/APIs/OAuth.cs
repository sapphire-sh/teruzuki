using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using teruzuki.Twitter.Model;

namespace teruzuki.Twitter.API
{
	public static class OAuth
	{
		public static IEnumerator RequestToken(TwitterClient client, Action<string> callback) {
			yield return client.POST<string> (Constants.URL.OAUTH_API + "request_token", callback);
		}
	}
}
