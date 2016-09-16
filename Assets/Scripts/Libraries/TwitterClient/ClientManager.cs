using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

namespace teruzuki.Twitter
{
	public class ClientManager
	{
		private static ClientManager instance;

		public static ClientManager Instance {
			get {
				if (instance == null) {
					instance = new ClientManager ();
				}
				return instance;
			}
		}

		public List<TwitterClient> ClientList { get; private set; }
		public TwitterClient CurrentClient {
			get {
				return ClientList [0];
			}
		}
		private List<Credentials> CredentialsList {
			get {
				return ClientList.Select (x => x.Credentials).ToList();
			}
		}

		private ClientManager ()
		{
			Debug.Log (Application.persistentDataPath);
			ClientList = new List<TwitterClient> ();
		}

		public void LoadClient ()
		{
			if (File.Exists (Application.persistentDataPath + "/" + Constants.Credentials.FILE_NAME)) {
				BinaryFormatter binaryFormatter = new BinaryFormatter ();
				FileStream fileStream = File.Open (Application.persistentDataPath + "/" + Constants.Credentials.FILE_NAME, FileMode.Open);
				((List<Credentials>)binaryFormatter.Deserialize (fileStream)).ForEach (x => ClientList.Add (new TwitterClient (x)));
				fileStream.Close ();
			}
		}

		public void SaveClient ()
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			FileStream fileStream = File.Create (Application.persistentDataPath + "/" + Constants.Credentials.FILE_NAME);
			binaryFormatter.Serialize (fileStream, CredentialsList);
			fileStream.Close ();
		}
	}
}
