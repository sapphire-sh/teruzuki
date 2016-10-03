using System;
using System.Collections;
using System.Collections.Generic;

namespace teruzuki.Twitter.Parameters
{
	public abstract class ITwitterParameters
	{
		protected Dictionary<string, string> queries;
		public Dictionary<string, string> Queries {
			get {
				return queries;
			}
		}

		public ITwitterParameters() {
			queries = new Dictionary<string, string> ();
		}

		public string ComposeQueryString() {
			var queryString = Helper.ComposeQueryString(this.queries);
			return (queryString.Length > 0 ? string.Format ("?{0}", queryString) : "");
		}
	}
}
