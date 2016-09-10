﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace teruzuki
{
	public class TwitterManager : MonoBehaviour
	{
		public GameObject tweetPrefab;

		void Start()
		{
			Application.OpenURL(Twitter.Client.GetRequestToken());
		}

		public void GetAccessToken(string pin)
		{
			Twitter.Client.GetAccessToken(pin);
			
			Debug.Log(Twitter.Account.VerifyCredentials());

            /*
             * Test for Twitter.Statuses
             * 
            var tweets = Twitter.Statuses.HomeTimeline();
            var i = 0;
			foreach(var tweet in tweets)
			{
				var obj = Instantiate(tweetPrefab);
				var mesh = obj.GetComponent<TextMesh>();
				mesh.text = tweet.text;
				obj.transform.position = new Vector3(0, i * 2, 0);
				++i;
			}
            */

        }
	}
}
