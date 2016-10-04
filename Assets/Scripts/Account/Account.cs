using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

using teruzuki.Twitter;

namespace teruzuki
{
	public class Account
	{
		public Client Client
		{
			get; private set;
		}

		public Twitter.Model.User User
		{
			get; private set;
		}

		public string AccessToken {
			get {
				return Client.Token.AccessToken;
			}
		}
		public string AccessTokenSecret {
			get {
				return Client.Token.AccessTokenSecret;
			}
		}

		public Sprite ProfileImage
		{
			get; private set;
		}

		public bool IsLoaded
		{
			get;
			private set;
		}

		public Account (Token token, Twitter.Model.User user) : this(new Client(token), user)
		{
		}

		public Account (Client client, Twitter.Model.User user) {
			this.Client = client;
			this.User = user;

			this.IsLoaded = false;
		}

		public IEnumerator Initialize ()
		{
			var www = new WWW (User.profile_image_url);
			yield return www;

			ProfileImage = Sprite.Create (www.texture, new Rect (0, 0, www.texture.width, www.texture.height), new Vector2 (0.0f, 0.0f));

			this.IsLoaded = true;
		}

		public IEnumerator Initialize (Action<Account> callback)
		{
			yield return this.Initialize ();

			callback (this);
		}
	}
}
