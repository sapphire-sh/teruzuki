using System.Collections;
using System.Collections.Generic;

namespace teruzuki
{
public class AccountManager
{
	private static AccountManager instance;
	public static AccountManager Instance {
		get {
			if(instance == null) {
				instance = new AccountManager();
			}
			return instance;
		}
	}

	private AccountManager() {
		AccountList = new List<Account>();
	}

    public List<Account> AccountList;
}
}
