using System;

namespace teruzuki.Twitter.Model
{
	public class Tweet : ITwitterModel
	{
		public ulong id;
		public string id_str;
		public string text;
	}
}
