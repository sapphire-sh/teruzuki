using System;
using System.Linq;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using SQLite4Unity3d;

namespace teruzuki
{
	public class AccountManager
	{
		private static AccountManager instance;

		public static AccountManager Instance {
			get {
				if (instance == null) {
					instance = new AccountManager ();
				}
				return instance;
			}
		}

		public Account CurrentAccount;

		private AccountManager ()
		{
			LoadDatabase ();
		}

		private List<Account> accountList;

		public ReadOnlyCollection<Account> AccountList {
			get {
				return accountList.AsReadOnly ();
			}
		}

		private string dbPath = Application.persistentDataPath + "/teruzuki.sqlite";
		private SQLiteConnection conn;

		private void CreateDatabase ()
		{
			try {
				Debug.Log (Application.dataPath);
				conn = new SQLiteConnection (dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

				conn.CreateTable<DBAccount> ();
			} catch (Exception e) {
				Debug.Log (e);
			}
		}

		private void LoadDatabase ()
		{
			try {
				var fileInfo = new FileInfo (dbPath);

				if (fileInfo == null || fileInfo.Exists == false) {
					CreateDatabase ();
				} else {
					conn = new SQLiteConnection (dbPath, SQLiteOpenFlags.ReadWrite);
				}

				accountList = new List<Account>();
				conn.Table<DBAccount> ().ToList ().ForEach(x => {
					var token = new Token() {
						AccessToken = x.AccessToken,
						AccessTokenSecret = x.AccessTokenSecret
					};
					var user = new Twitter.Model.User() {
						id_str = x.Id,
						screen_name = x.ScreenName,
						profile_image_url = x.ProfileImageURL
					};
					var account = new Account(token, user);
					accountList.Add(account);
				});
			} catch (Exception e) {
				Debug.Log (e);
			}

		}

		public void InsertAccount (Account account)
		{
			try {
				conn.Insert(new DBAccount(account));
				accountList.Add(account);
			} catch (Exception e) {
				Debug.Log (e);
			}
		}

		public void DeleteAccount (Account account)
		{
			try {
				accountList.Remove(account);
				conn.Delete<Account>(account.User.id_str);
			} catch (Exception e) {
				Debug.Log (e);
			}
		}
	}
}
