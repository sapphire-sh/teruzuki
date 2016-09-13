using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using teruzuki.Twitter;

namespace teruzuki
{
	public class SessionManager
	{
		private static SessionManager instance;

		public static SessionManager Instance {
			get {
				if (instance == null) {
					instance = new SessionManager ();
				}
				return instance;
			}
		}

		private static readonly string FILE_NAME = "accounts.teruzuki";

		public List<Account> SessionList { get; private set; }
		public int CurrentSession { get; private set; }

		private SessionManager ()
		{
			Debug.Log (Application.persistentDataPath);
			SessionList = new List<Account> ();
			CurrentSession = 0;
		}

		public void LoadSession ()
		{
			if (File.Exists (Application.persistentDataPath + "/" + FILE_NAME)) {
				BinaryFormatter binaryFormatter = new BinaryFormatter ();
				FileStream fileStream = File.Open (Application.persistentDataPath + "/" + FILE_NAME, FileMode.Open);
				SessionList = (List<Account>)binaryFormatter.Deserialize (fileStream);
				fileStream.Close ();

				Client.Instance.SetAccessToken (SessionList [0]);
			}
		}

		public void SaveSession ()
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			FileStream fileStream = File.Create (Application.persistentDataPath + "/" + FILE_NAME);
			binaryFormatter.Serialize (fileStream, SessionList);
			fileStream.Close ();
		}
	}
}
