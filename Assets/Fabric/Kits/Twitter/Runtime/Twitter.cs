/*
 * Copyright (C) 2015 Twitter, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
namespace Fabric.Twitter
{	
	using UnityEngine;
	using System;
	using System.Collections.Generic;
	using Fabric.Internal.Twitter;
	
	public sealed class Twitter : ScriptableObject
	{
		private static ITwitter twitter;
		private static GameObject twitterGameObject;

		public static void Init ()
		{
			twitterGameObject = new GameObject ("TwitterGameObject");
			twitterGameObject.AddComponent<TwitterComponent> ();

#if UNITY_IOS && !UNITY_EDITOR
			twitter = new IOSTwitterImpl();
#elif UNITY_ANDROID && !UNITY_EDITOR
			twitter = new AndroidTwitterImpl();
#else
			twitter = new EditorTwitterImpl();
#endif
		}
		/// <summary>
		/// Show login with Twitter
		/// <param name="successCallback">Callback to call on success</param>
		/// <param name="failureCallback">Callback to call on failure</param>
		/// </summary>
		public static void LogIn (Action<TwitterSession> successCallback = null, Action<ApiError> failureCallback = null)
		{
			twitterGameObject.GetComponent<TwitterComponent> ().loginSuccessAction = successCallback;
			twitterGameObject.GetComponent<TwitterComponent> ().loginFailureAction = failureCallback;
			twitter.LogIn ();
		}
		/// <summary>
		/// Logout of current session
		/// </summary>
		public static void LogOut ()
		{
			twitter.LogOut ();
		}
		/// <summary>
		/// Returns the active session.
		/// </summary>
		public static TwitterSession Session { get { return twitter.Session (); } }

		/// <summary>
		/// Request Twitter users email address
		/// <param name="session">User's session from Login</param>
		/// <param name="successCallback">Callback to call on success</param>
		/// <param name="failureCallback">Callback to call on failure</param>
		/// </summary>
		public static void RequestEmail (TwitterSession session, Action<string> successCallback = null, Action<ApiError> failureCallback = null)
		{
			twitterGameObject.GetComponent<TwitterComponent> ().emailSuccessAction = successCallback;
			twitterGameObject.GetComponent<TwitterComponent> ().emailFailureAction = failureCallback;
			twitter.RequestEmail (session);
		}
		/// <summary>
		/// Show Twitter composer
		/// <param name="session">User’s session from Login</param>
		/// <param name="card">Card for composer</param>
		/// </summary>
		public static void Compose (TwitterSession session, Card card)
		{
			twitter.Compose (session, card);
		}
	}

	public sealed class AppCardBuilder
	{
		private string cardType;
		private string appIPhoneId;
		private string appIPadId;
		private string appGooglePlayId;
		private string imageUri;
		
		/// <summary>
		/// Sets the Apple App Store id for the promoted iOS app shown on iOS displays.
		/// <param name="appIPhoneId">Apple App Store id (e.g. Twitter App is 333903271). The id 
		/// must  correspond to a published iPhone app for Card Tweets to link correctly. </param>
		/// </summary>
		public AppCardBuilder IPhoneId (string appIPhoneId)
		{
			this.appIPhoneId = appIPhoneId;
			return this;
		}
		/// <summary>
		/// Sets the Apple App Store id for the promoted iPad app shown on iOS displays.
		/// <param name="appIPadId">Apple App Store id (e.g. Twitter App is 333903271). The id 
		/// must correspond to a published iPhone app for Card Tweets to link correctly. </param>
		/// </summary>
		public AppCardBuilder IPadId (string appIPadId)
		{
			this.appIPadId = appIPadId;
			return this;
		}
		/// <summary>
		/// Sets the Google Play Store package name for the promoted Android app shown on
		/// Android displays.
		/// <param name="appGooglePlayId">Google Play Store package (e.g. "com.twitter.android").
		/// The package must correspond to a published app on Google Play for Card Tweets to link 
		/// correctly.</param>
		/// </summary>
		public AppCardBuilder GooglePlayId (string appGooglePlayId)
		{
			this.appGooglePlayId = appGooglePlayId;
			return this;
		}
		/// <summary>
		/// Sets the App Card image Uri of an image to show in the Card.
		/// <param name="imageUri">A Uri to a local file.</param>
		/// </summary>
		public AppCardBuilder ImageUri (string imageUri)
		{
			this.imageUri = imageUri;
			return this;
		}
		// Implicit conversion operator
		public static implicit operator Card (AppCardBuilder builder)
		{
			return new Card (
				"promo_image_app",
				builder.appIPhoneId,
				builder.appIPadId,
				builder.appGooglePlayId,
				builder.imageUri);
		}
	}
}
