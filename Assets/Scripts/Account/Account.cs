using System;using System.Collections;using System.Collections.Generic;using UnityEngine;using teruzuki.Twitter;namespace teruzuki{	public class Account	{		public Client Client
		{
			get; private set;
		}		public Twitter.Model.User User
		{
			get; private set;
		}		public Sprite ProfileImage
		{
			get; private set;
		}
		public bool IsLoaded		{			get;			private set;		}		public Account (Token token)		{			this.Client = new Client (token);			this.IsLoaded = false;		}		public IEnumerator Initialize ()
		{
			var www = new WWW (User.profile_image_url);			yield return www;			ProfileImage = Sprite.Create (www.texture, new Rect (0, 0, www.texture.width, www.texture.height), new Vector2 (0.0f, 0.0f));
		}	}}