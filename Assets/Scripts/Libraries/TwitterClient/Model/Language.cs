using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace teruzuki.Twitter.Model
{
	public class Language : IModel
	{
		public string code { get; set; }
		public string status { get; set; }
		public string name { get; set; }
	}
}
