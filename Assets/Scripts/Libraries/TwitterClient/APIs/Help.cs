using System;
using System.Collections;
using System.Collections.Generic;

using teruzuki.Twitter.Model;

namespace teruzuki.Twitter.API
{
	public static class Help
	{
		public static IEnumerator Configuration (TwitterClient client, Action<Configuration> callback)
		{
			yield return client.GET<Configuration> (Constants.URL.BuildURL ("help/configuration"), callback);
		}

		public static IEnumerator Languages (TwitterClient client, Action<List<Language>> callback)
		{
			yield return client.GET<List<Language>> (Constants.URL.BuildURL ("help/languages"), callback);
		}

		public static IEnumerator Privacy (TwitterClient client, Action<Privacy> callback)
		{
			yield return client.GET<Privacy> (Constants.URL.BuildURL ("help/privacy"), callback);
		}

		public static IEnumerator TOS (TwitterClient client, Action<TOS> callback)
		{
			yield return client.GET<TOS> (Constants.URL.BuildURL ("help/tos"), callback);
		}
	}
}
