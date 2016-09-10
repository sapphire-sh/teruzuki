using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace teruzuki.Twitter
{
	public static class Help
	{
		public static Configuration Configuration()
		{
			return JsonConvert.DeserializeObject<Configuration>(
				Client.Instance.Get(
					"https://api.twitter.com/1.1/help/configuration.json"
				)
			);
		}

		public static List<Language> Languages()
		{
			return JsonConvert.DeserializeObject<List<Language>>(
				Client.Instance.Get(
					"https://api.twitter.com/1.1/help/languages.json"
				)
			);
		}

		public static Privacy Privacy()
		{
			return JsonConvert.DeserializeObject<Privacy>(
				Client.Instance.Get(
					"https://api.twitter.com/1.1/help/privacy.json"
				)
			);
		}

		public static TOS TOS()
		{
			return JsonConvert.DeserializeObject<TOS>(
				Client.Instance.Get(
					"https://api.twitter.com/1.1/help/tos.json"
				)
			);
		}
	}
}
