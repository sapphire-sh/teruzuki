using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using teruzuki.Twitter.Model;

namespace teruzuki
{
	public class UITweetItem : MonoBehaviour
	{
		private float rotationLeft = 180.0f;
		private float rotationSpeed = 200.0f;

		private float zoomLeft = 10.0f;
		private float zoomSpeed = 50.0f;

		private RectTransform rectTransform;

		public GameObject profileImage;
		public GameObject screenName;
		public GameObject text;

		public Tweet tweet;

		void Start ()
		{
			rectTransform = GetComponent<RectTransform> ();
			rectTransform.Rotate (Vector3.right, 180.0f);
			rectTransform.Translate (new Vector3 (0.0f, 0.0f, -zoomLeft));
		}

		public void Initialize (Tweet tweet)
		{
			this.tweet = tweet;

//			profileImage.GetComponent<Image> ().sprite = account.ProfileImage;
			screenName.GetComponent<Text> ().text = "@" + tweet.user.screen_name;
			text.GetComponent<Text> ().text = tweet.text;
		}

		void Update ()
		{
			if (rotationLeft > 0.0f)
			{
				var rotation = Time.deltaTime * rotationSpeed;
				if (rotationLeft > rotation)
				{
					rotationLeft -= rotation;
				}
				else
				{
					rotation = rotationLeft;
					rotationLeft = 0.0f;
				}
				rectTransform.Rotate (Vector3.right, rotation);
			}
			else if (zoomLeft > 0.0f)
			{
				var zoom = Time.deltaTime * zoomSpeed;
				if (zoomLeft > zoom)
				{
					zoomLeft -= zoom;
				}
				else
				{
					zoom = zoomLeft;
					zoomLeft = 0.0f;
				}
				rectTransform.Translate (0.0f, 0.0f, -zoom);
			}
		}

		public void OnClick ()
		{
//			AccountManager.Instance.CurrentAccount = this.account;
//			SceneManager.LoadScene ("main");
		}
	}
}
