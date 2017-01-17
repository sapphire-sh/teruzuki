using System;

namespace teruzuki.Twitter
{
	public static class Constants
	{
		public static class URL {
			private static readonly string PROTOCOL = "https";
			private static readonly string REST_HOSTNAME = "api.twitter.com";
			private static readonly string USERSTREAM_HOSTNAME = "userstream.twitter.com";
			private static readonly string VERSION = "1.1";

			public static readonly string REST_BASE_URL = string.Format("{0}://{1}/{2}/", PROTOCOL, REST_HOSTNAME, VERSION);
			public static readonly string USERSTREAM_BASE_URL = string.Format("{0}://{1}/{2}/", PROTOCOL, USERSTREAM_HOSTNAME, VERSION);

			public static readonly string REQUEST_TOKEN = string.Format("{0}://{1}/oauth/request_token", PROTOCOL, REST_HOSTNAME);
			public static readonly string ACCESS_TOKEN = string.Format("{0}://{1}/oauth/access_token", PROTOCOL, REST_HOSTNAME);
		}
		
		public static class Token
		{
			// public static readonly string CONSUMER_KEY = "OcbDuSiWrHYWU2RFgdWyV61F8";
			// public static readonly string CONSUMER_SECRET = "7fNW3QITGNFQAisvtkk8yaHdXkx5j7mxM2rEJShUeqxbwZEDHZ";
			
			public static readonly string CONSUMER_KEY = "IQKbtAYlXLripLGPWd0HUA";
			public static readonly string CONSUMER_SECRET = "GgDYlkSvaPxGxC4X8liwpUoqKwwr3lCADbz8A7ADU";
		}
	}
}
