using System;
using UnityEngine;

namespace teruzuki
{
	public class DBAccount
	{
		public string Id { get; set; }
		public string AccessToken { get; set; }
		public string AccessTokenSecret { get; set; }
		public string ScreenName { get; set; }
		public string ProfileImageURL { get; set; }

		public DBAccount() {
			this.Id = "";
			this.AccessToken = "";
			this.AccessTokenSecret = "";
			this.ScreenName = "";
			this.ProfileImageURL = "";
		}

		public DBAccount(Account account) {
			this.Id = account.User.id_str;
			this.AccessToken = account.AccessToken;
			this.AccessTokenSecret = account.AccessTokenSecret;
			this.ScreenName = account.User.screen_name;
			this.ProfileImageURL = account.User.profile_image_url;
		}
	}
}

