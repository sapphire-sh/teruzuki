using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using teruzuki.Twitter;

namespace teruzuki
{
	public class Account : MonoBehaviour
	{
		public OAuth client;
		public Token token;
		public Sprite profileImage;

		public bool isLoaded
		{
			get;
			private set;
		}

		private IEnumerator initializeCoroutine;

		public Account(Token token)
		{
			this.client = new OAuth(token);
			this.token = token;

			this.isLoaded = false;

			initializeCoroutine = Initialize();
			StartCoroutine(initializeCoroutine);
		}

		private IEnumerator Initialize()
		{
			var www = new WWW("https://pbs.twimg.com/profile_images/747596515871358976/uEb5J9WP_400x400.jpg");
			yield return www;

			profileImage = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.0f, 0.0f));

			isLoaded = true;
		}
	}
}
