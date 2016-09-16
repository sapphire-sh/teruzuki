using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace teruzuki.Twitter.Model
{
	public class DirectMessage : IModel
	{
		public long id { get; set; }
		public string id_str { get; set; }
		public User recipient { get; set; }
		public User sender { get; set; }
		public string text { get; set; }
	}
}
