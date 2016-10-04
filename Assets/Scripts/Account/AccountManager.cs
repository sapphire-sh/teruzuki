using System;
using System.Linq;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

namespace teruzuki
{
	public class AccountManager
	{
		private static AccountManager instance;
		public static AccountManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new AccountManager ();
				}
				return instance;
			}
		}
		public Account CurrentAccount;

		private AccountManager ()
		{
			Load ();
		}

		private List<Account> accountList;
		public ReadOnlyCollection<Account> AccountList
		{
			get
			{
				return accountList.AsReadOnly ();
			}
		}

		public void AddAccount(Account account)
		{
			accountList.Add (account);
			Save ();
		}

		public void RemoveAccount(Account account)
		{
			accountList.Remove (account);
			Save ();
		}

		private void Load ()
		{
			Debug.Log (Application.persistentDataPath);
			if (File.Exists (Application.persistentDataPath + "/" + Constants.Token.FILE_NAME))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter ();
				FileStream fileStream = File.Open (Application.persistentDataPath + "/" + Constants.Token.FILE_NAME, FileMode.Open);
				accountList = (List<Account>)binaryFormatter.Deserialize (fileStream);
				fileStream.Close ();
			}
			else
			{
				accountList = new List<Account> ();
			}
		}

		private void Save ()
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			FileStream fileStream = File.Create (Application.persistentDataPath + "/" + Constants.Token.FILE_NAME);
			binaryFormatter.Serialize (fileStream, accountList);
			fileStream.Close ();
		}
	}
}
