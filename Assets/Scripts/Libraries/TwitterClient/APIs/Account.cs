﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace teruzuki.Twitter.API
{
	public static class Account
	{
		public static string VerifyCredentials()
		{
			return "";
//			return Client.Instance.Get("https://api.twitter.com/1.1/account/verify_credentials.json");
		}
	}
}
