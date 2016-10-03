using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

namespace teruzuki.Twitter
{
	[Serializable]
	public class TokenManager
	{
		[NonSerialized]
		private static TokenManager instance;
		
		public static TokenManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new TokenManager ();
				}
				return instance;
			}
		}

		public List<Token> TokenList
		{
			get; private set;
		}
		
		private TokenManager ()
		{
			Load ();
		}

		public void AddToken(Token token)
		{
			TokenList.Add (token);
			Save ();
		}

		public void RemoveToken(Token token)
		{
			TokenList.Remove (token);
			Save ();
		}
		 
		private void Load ()
		{
			Debug.Log (Application.persistentDataPath);
			if (File.Exists (Application.persistentDataPath + "/" + Constants.Token.FILE_NAME))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter ();
				FileStream fileStream = File.Open (Application.persistentDataPath + "/" + Constants.Token.FILE_NAME, FileMode.Open);
				TokenList = (List<Token>)binaryFormatter.Deserialize (fileStream);
				fileStream.Close ();
			}
			else
			{
				TokenList = new List<Token> ();
			}
		}

		private void Save ()
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			FileStream fileStream = File.Create (Application.persistentDataPath + "/" + Constants.Token.FILE_NAME);
			binaryFormatter.Serialize (fileStream, TokenList);
			fileStream.Close ();
		}
	}
}
