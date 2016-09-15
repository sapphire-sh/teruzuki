using System;
using System.Collections;
using System.Collections.Generic;

using teruzuki.Twitter.Model;

namespace teruzuki.Twitter.API
{
	public static class Help
	{
		public static IEnumerator Configuration (TwitterClient client, Dictionary<string, string> queries, Action<Configuration> callback)
		{
			yield return client.GET<Configuration> (Helper.BuildURL ("help/configuration"), queries, callback);
		}

		public static IEnumerator Languages (TwitterClient client, Dictionary<string, string> queries, Action<List<Language>> callback)
		{
			yield return client.GET<List<Language>> (Helper.BuildURL ("help/languages"), queries, callback);
		}

		public static IEnumerator Privacy (TwitterClient client, Dictionary<string, string> queries, Action<Privacy> callback)
		{
			yield return client.GET<Privacy> (Helper.BuildURL ("help/privacy"), queries, callback);
		}

		public static IEnumerator TOS (TwitterClient client, Dictionary<string, string> queries, Action<TOS> callback)
		{
			yield return client.GET<TOS> (Helper.BuildURL ("help/tos"), queries, callback);
		}
	}
}
