using System;

namespace teruzuki.Twitter.Model
{
	public class DirectMessage : ITwitterModel
	{
		public ulong id;
		public string id_str;
		public User recipient;
		public User sender;
		public string text;
	}
}
