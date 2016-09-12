using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace teruzuki.Twitter.Model
{
	public class Tweet
	{
		public UInt64 id { get; set; }
		public string id_str { get; set; }
		public string text { get; set; }
	}
}
