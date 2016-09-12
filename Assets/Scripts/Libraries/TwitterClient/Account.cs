using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

namespace teruzuki.Twitter
{
	[System.Serializable]
	public class Account
	{
		public string AccessToken;
		public string AccessTokenSecret;
	}
}
