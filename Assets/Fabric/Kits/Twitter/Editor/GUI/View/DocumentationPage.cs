namespace Fabric.Internal.Twitter.Editor.View
{
	using UnityEngine;
	using UnityEditor;
	using Fabric.Internal.Editor.View;
	using System;
	using System.Collections.Generic;

	public class DocumentationPage : Page
	{
		private const string DocumentationUrl = "https://docs.fabric.io/unity/twitter/unity.html#initializing-twitter";

		private KeyValuePair<string, Action> back;
		private Action openDocumentationLink;

		public DocumentationPage(Action onBackClicked)
		{
			back = new KeyValuePair<string, Action> ("Back", onBackClicked);
			openDocumentationLink = delegate() {
				Application.OpenURL (DocumentationUrl);
			};
		}

		#region Components
		private static class Components
		{
			private static readonly GUIStyle ButtonStyle;
			private static readonly GUIStyle MessageStyle;
			private static readonly GUIStyle TextStyle;

			static Components()
			{
				MessageStyle = new GUIStyle ();
				MessageStyle.margin = new RectOffset(20, 20, 10, 20);

				TextStyle = new GUIStyle (GUI.skin.label);
				TextStyle.normal.textColor = Color.white;
				TextStyle.fontSize = 14;
				TextStyle.wordWrap = true;
				TextStyle.padding = new RectOffset (0, 0, 0, 0);

				ButtonStyle = new GUIStyle (GUI.skin.button);
				ButtonStyle.fixedWidth = 185;
				ButtonStyle.fixedHeight = 30;
			}

			public static void RenderMessage(string message)
			{
				GUILayout.BeginHorizontal (MessageStyle);
				GUILayout.FlexibleSpace ();
				GUILayout.Label (message, TextStyle);
				GUILayout.FlexibleSpace ();
				GUILayout.EndHorizontal ();
			}

			public static void RenderButton(string caption, Action onClick)
			{
				GUILayout.BeginHorizontal (MessageStyle);
				GUILayout.FlexibleSpace ();
				if (GUILayout.Button (caption, ButtonStyle)) {
					onClick ();
				}
				GUILayout.FlexibleSpace ();
				GUILayout.EndHorizontal ();
			}
		}
		#endregion

		public override void RenderImpl(Rect position)
		{
			RenderHeader ("Twitter Documentation");
			RenderFooter (back, null);

			Components.RenderMessage (
				"Congratulations, it looks like you're all set up! For more information on how to use Twitter in your app, visit our documentation."
			);

			Components.RenderButton ("View Documentation", openDocumentationLink);
		}
	}
}
