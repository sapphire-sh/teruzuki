namespace Fabric.Internal.Twitter.Editor.View
{
	using UnityEngine;
	using UnityEditor;
	using System;
	using System.Collections.Generic;
	using Fabric.Internal.Editor.Model;
	using Fabric.Internal.Editor.View;
	
	public class KeyEntryPage : Page
	{
		private KeyValuePair<string, Action> back;
		private KeyValuePair<string, Action> next;

		private string twitterKey = "";
		private string twitterSecret = "";

		public KeyEntryPage(Action onBackClicked, Action<List<Settings.InstalledKit.MetaTuple>> persistKeys)
		{
			back = new KeyValuePair<string, Action> ("Back", onBackClicked);
			next = new KeyValuePair<string, Action> ("Next", delegate() {
				persistKeys (new List<Settings.InstalledKit.MetaTuple>() {
					new Settings.InstalledKit.MetaTuple { Key = "TwitterKey", Value = twitterKey, Platform = "android" },
					new Settings.InstalledKit.MetaTuple { Key = "TwitterKey", Value = twitterKey, Platform = "ios" },
					new Settings.InstalledKit.MetaTuple { Key = "TwitterSecret", Value = twitterSecret, Platform = "android" },
					new Settings.InstalledKit.MetaTuple { Key = "TwitterSecret", Value = twitterSecret, Platform = "ios" }
				});
			});
		}

		#region Components
		private static class Components
		{
			private static readonly Texture2D FieldWrapperBackground;
			private static readonly Texture2D FieldBackground;

			private static readonly GUIStyle FieldWrapperStyle;
			private static readonly GUIStyle FieldLabelStyle;
			private static readonly GUIStyle FieldStyle;
			private static readonly GUIStyle MessageStyle;
			private static readonly GUIStyle TextStyle;

			private static readonly GUIContent TextContent;

			static Components()
			{
				TextContent = new GUIContent ();
				FieldWrapperBackground = Fabric.Internal.Editor.View.Render.MakeBackground (1, 1, new Color32 (255, 255, 255, 76));
				FieldBackground = Fabric.Internal.Editor.View.Render.MakeBackground (1, 1, Fabric.Internal.Editor.View.Render.LBlue);

				FieldWrapperStyle = new GUIStyle();
				FieldWrapperStyle.normal.background = FieldWrapperBackground;
				FieldWrapperStyle.margin = new RectOffset (20, 20, 10, 0);

				FieldLabelStyle = new GUIStyle (GUI.skin.label);
				FieldLabelStyle.normal.textColor = Color.white;
				FieldLabelStyle.active.textColor = Color.white;
				FieldLabelStyle.fontStyle = FontStyle.Bold;
				FieldLabelStyle.margin = new RectOffset (2, 0, 2, 2);

				FieldStyle = new GUIStyle (GUI.skin.textField);
				FieldStyle.fontSize = 15;
				FieldStyle.wordWrap = false;
				FieldStyle.normal.background = FieldBackground;
				FieldStyle.active.background = FieldBackground;
				FieldStyle.focused.background = FieldBackground;
				FieldStyle.active.textColor = Color.white;
				FieldStyle.normal.textColor = Color.white;
				FieldStyle.focused.textColor = Color.white;
				FieldStyle.margin = new RectOffset (2, 2, 0, 2);
				FieldStyle.padding = new RectOffset (2, 2, 8, 8);

				MessageStyle = new GUIStyle ();
				MessageStyle.margin = new RectOffset(20, 20, 10, 20);

				TextStyle = new GUIStyle (GUI.skin.label);
				TextStyle.normal.textColor = Color.white;
				TextStyle.fontSize = 14;
				TextStyle.wordWrap = true;
				TextStyle.padding = new RectOffset (0, 0, 0, 0);
			}

			public static void RenderMessage(string message)
			{
				GUILayout.BeginHorizontal (MessageStyle);
				GUILayout.FlexibleSpace ();
				GUILayout.Label (message, TextStyle);
				GUILayout.FlexibleSpace ();
				GUILayout.EndHorizontal ();
			}

			public static void RenderField(string title, string bound, Action<string> set)
			{
				GUILayout.BeginVertical (FieldWrapperStyle);
				GUILayout.Label (title, FieldLabelStyle);

				Color old = GUI.skin.settings.cursorColor;
				GUI.skin.settings.cursorColor = Color.white;

				TextContent.text = bound;
				Rect fieldRect = GUILayoutUtility.GetRect (TextContent, FieldStyle);
				set (EditorGUI.TextField (fieldRect, bound ?? "", FieldStyle));

				GUI.skin.settings.cursorColor = old;

				GUILayout.EndVertical ();
			}
		}
		#endregion

		public override void RenderImpl(Rect position)
		{
			RenderHeader ("Existing Twitter account");

			Components.RenderMessage ("Enter your account keys below. These keys can be obtained from apps.twitter.com.");
			Components.RenderField ("Twitter Key", twitterKey, value => twitterKey = value);
			Components.RenderField ("Twitter Secret", twitterSecret, value => twitterSecret = value);

			KeyValuePair<string, Action>? nextDisabled = null;
			RenderFooter (back, FieldsArePopulated() ? next : nextDisabled);
		}

		private bool FieldsArePopulated()
		{
			return !String.IsNullOrEmpty (twitterKey) && !String.IsNullOrEmpty (twitterSecret);
		}
	}
}
