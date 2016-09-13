using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace teruzuki
{
	public class TimelineManager : MonoBehaviour
	{
		public GameObject tweetPrefab;

		void Start()
		{
			Debug.Log (Twitter.Client.Instance);
			var tweets = Twitter.API.Statuses.HomeTimeline ();
			for (int i = 0; i < tweets.Count; ++i) {
				var tweet = tweets [i];
				var obj = Instantiate(tweetPrefab);
				var mesh = obj.GetComponent<TextMesh>();
				mesh.text = tweet.text;
				obj.transform.position = new Vector3(0, i * 2, 0);
				++i;
			}
		}
    }
}
