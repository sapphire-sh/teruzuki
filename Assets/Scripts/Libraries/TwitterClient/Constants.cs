using System;

namespace teruzuki.Twitter
{
	public static class Constants
	{
		public static class URL {
			private static readonly string PROTOCOL = "https";
			private static readonly string HOSTNAME = "api.twitter.com";
			private static readonly string VERSION = "1.1";

			private static string BASE_URL { get { return string.Format("{0}://{1}/", PROTOCOL, HOSTNAME); } }

			public static string OAUTH_API { get { return string.Format("{0}{1}/", BASE_URL, "oauth"); } }

			public static string BuildURL(string endpoint) {
				return string.Format("{0}{1}/{2}.json", BASE_URL, VERSION, endpoint);
			}
		}

		public static class Session {
			public static readonly string FILE_NAME = "accounts.teruzuki";
		}
	}
}
	