using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace teruzuki
{
	public class TestManager : MonoBehaviour
	{
		public GameObject tweetPrefab;

		void Start()
		{
			Debug.Log("TwitterManager Start");
			Test();
		}

		public void Test()
		{
//			var dm = Twitter.API.DirectMessages.New("KOINICHl", "test test");
//			var obj = Instantiate(tweetPrefab);
//			var mesh = obj.GetComponent<TextMesh>();
//			mesh.text = dm.text;
//			obj.transform.position = new Vector3(0, 0, 0);

			/*
			var users = Twitter.API.Users.Search("KOINICHI");
			var i = 0;
			foreach (var user in users)
			{
				var obj = Instantiate(tweetPrefab);
				var mesh = obj.GetComponent<TextMesh>();
				mesh.text = user.name + " " + user.id_str;
				obj.transform.position = new Vector3(0, i * 2, 0);
				++i;
			}
			*/
		}
	}
}
