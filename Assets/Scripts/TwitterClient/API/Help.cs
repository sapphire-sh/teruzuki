using System;
using System.Collections;
using System.Collections.Generic;

using teruzuki.Twitter.Parameters.Help;
using teruzuki.Twitter.Model;

namespace teruzuki.Twitter.API
{
	public static class Help
	{
		public static IEnumerator Configuration (Client client, ConfigurationParameters parameters, Action<Configuration> callback)
		{
			yield return client.GET<Configuration> (Helper.BuildRESTURL ("help/configuration"), parameters, callback);
		}

		public static IEnumerator Languages (Client client, LanguagesParameters parameters, Action<List<Language>> callback)
		{
			yield return client.GET<List<Language>> (Helper.BuildRESTURL ("help/languages"), parameters, callback);
		}

		public static IEnumerator Privacy (Client client, PrivacyParameters parameters, Action<Privacy> callback)
		{
			yield return client.GET<Privacy> (Helper.BuildRESTURL ("help/privacy"), parameters, callback);
		}

		public static IEnumerator TOS (Client client, TOSParameters parameters, Action<TOS> callback)
		{
			yield return client.GET<TOS> (Helper.BuildRESTURL ("help/tos"), parameters, callback);
		}
	}
}
