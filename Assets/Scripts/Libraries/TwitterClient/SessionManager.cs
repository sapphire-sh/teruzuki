using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

namespace teruzuki.Twitter
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

		public List<TwitterClient> SessionList { get; private set; }
		public TwitterClient CurrentSession {
			get {
				return SessionList [0];
			}
		}

		private SessionManager ()
		{
			Debug.Log (Application.persistentDataPath);
			SessionList = new List<TwitterClient> ();
		}

//		public void LoadSession ()
//		{
//			if (File.Exists (Application.persistentDataPath + "/" + Constants.Session.FILE_NAME)) {
//				BinaryFormatter binaryFormatter = new BinaryFormatter ();
//				FileStream fileStream = File.Open (Application.persistentDataPath + "/" + Constants.Session.FILE_NAME, FileMode.Open);
//				var accessTokens = (List<AccessToken>)binaryFormatter.Deserialize (fileStream);
//				foreach (var accessToken in accessTokens) {
//					SessionList.Add (new TwitterClient (accessToken));
//				}
//				fileStream.Close ();
//			}
//		}
//
//		public void SaveSession ()
//		{
//			BinaryFormatter binaryFormatter = new BinaryFormatter ();
//			FileStream fileStream = File.Create (Application.persistentDataPath + "/" + Constants.Session.FILE_NAME);
//			binaryFormatter.Serialize (fileStream, AccessTokenList);
//			fileStream.Close ();
//		}
	}
}
