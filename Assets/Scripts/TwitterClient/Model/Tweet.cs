using System;

namespace teruzuki.Twitter.Model
{
	public class Tweet : ITwitterModel
	{
		public long id;
		public string id_str;
		public string text;
	}
}
