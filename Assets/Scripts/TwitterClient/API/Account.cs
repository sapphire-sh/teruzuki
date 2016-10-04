using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using teruzuki.Twitter.Parameters.Account;
using teruzuki.Twitter.Model;

namespace teruzuki.Twitter.API
{
	public static class Account
	{
		public static IEnumerator VerifyCredentials (Client client, VerifyCredentialsParameters parameters, Action<User> callback)
		{
			yield return client.GET<User> (Helper.BuildURL ("account/verify_credentials"), parameters, callback);
		}
	}
}
