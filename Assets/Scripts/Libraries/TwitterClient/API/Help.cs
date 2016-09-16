using System;
using System.Collections;
using System.Collections.Generic;

using teruzuki.Twitter.Parameters.Help;
using teruzuki.Twitter.Model;

namespace teruzuki.Twitter.API
{
	public static class Help
	{
		public static IEnumerator Configuration (TwitterClient client, ConfigurationParameters parameters, Action<Configuration> callback)
		{
			yield return client.GET<Configuration> (Helper.BuildURL ("help/configuration"), parameters, callback);
		}

		public static IEnumerator Languages (TwitterClient client, LanguagesParameters parameters, Action<List<Language>> callback)
		{
			yield return client.GET<List<Language>> (Helper.BuildURL ("help/languages"), parameters, callback);
		}

		public static IEnumerator Privacy (TwitterClient client, PrivacyParameters parameters, Action<Privacy> callback)
		{
			yield return client.GET<Privacy> (Helper.BuildURL ("help/privacy"), parameters, callback);
		}

		public static IEnumerator TOS (TwitterClient client, TOSParameters parameters, Action<TOS> callback)
		{
			yield return client.GET<TOS> (Helper.BuildURL ("help/tos"), parameters, callback);
		}
	}
}
