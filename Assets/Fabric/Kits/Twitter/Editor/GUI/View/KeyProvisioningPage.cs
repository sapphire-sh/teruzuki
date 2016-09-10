namespace Fabric.Internal.Twitter.Editor.View
{
	using UnityEngine;
	using UnityEditor;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using Fabric.Internal.Editor.Model;
	using Fabric.Internal.Editor.View;
	
	public class KeyProvisioningPage : Page
	{
		private string email;
		private KeyValuePair<string, Action> back;
		private Action onAlreadyHaveAccountClicked;
		private Action onTwitterAgreementLinkClicked;
		private Action onDeveloperAgreementLinkClicked;
		private Action onCreateAccountClicked;
		private bool agreed = false;

		public KeyProvisioningPage(
			string email,
			Action onBackClicked,
			Action onAlreadyHaveAccountClicked,
			Action onTwitterAgreementLinkClicked,
			Action onDeveloperAgreementLinkClicked,
			Action onCreateAccountClicked
		)
		{
			this.email = email;
			this.back = new KeyValuePair<string, Action> ("Back", onBackClicked);
			this.onAlreadyHaveAccountClicked = onAlreadyHaveAccountClicked;
			this.onTwitterAgreementLinkClicked = onTwitterAgreementLinkClicked;
			this.onDeveloperAgreementLinkClicked = onDeveloperAgreementLinkClicked;
			this.onCreateAccountClicked = onCreateAccountClicked;
		}

		#region Components
		private static class Components
		{
			private static readonly GUIStyle SectionStyle;
			private static readonly GUIStyle TextStyle;
			private static readonly GUIStyle ToggleStyle;
			private static readonly GUIStyle ButtonStyle;
			private static readonly GUIStyle LinkStyle;
			private static readonly GUIStyle SeparatorStyle;

			private static readonly Color32 LinkNormalColor = Fabric.Internal.Editor.View.Render.FromHex (0x2B6591);

			static Components()
			{
				SectionStyle = new GUIStyle ();
				SectionStyle.margin = new RectOffset(20, 20, 10, 0);

				TextStyle = new GUIStyle (GUI.skin.label);
				TextStyle.normal.textColor = Color.white;
				TextStyle.fontSize = 14;
				TextStyle.wordWrap = true;
				TextStyle.padding = new RectOffset(0, 0, 0, 0);

				ToggleStyle = new GUIStyle (GUI.skin.toggle);
				ToggleStyle.normal.textColor = Color.white;
				ToggleStyle.active.textColor = Color.white;
				ToggleStyle.onActive.textColor = Color.white;
				ToggleStyle.onNormal.textColor = Color.white;
				ToggleStyle.fontSize = 14;
				ToggleStyle.wordWrap = true;
				ToggleStyle.contentOffset = new Vector2(10, 0);

				ButtonStyle = new GUIStyle (GUI.skin.button);
				ButtonStyle.fixedHeight = 29;
				ButtonStyle.fixedWidth = 142;

				LinkStyle = new GUIStyle (GUI.skin.label);
				LinkStyle.normal.textColor = LinkNormalColor;
				LinkStyle.hover.textColor = Color.white;
				LinkStyle.fontSize = 14;
				LinkStyle.wordWrap = true;

				LinkStyle.normal.background = Fabric.Internal.Editor.View.Render.MakeBackground (
					50, 20, Fabric.Internal.Editor.View.Render.Lerped
				);
				LinkStyle.hover.background = Fabric.Internal.Editor.View.Render.MakeBackground (
					50, 20, Fabric.Internal.Editor.View.Render.LBlue
				);

				SeparatorStyle = new GUIStyle ();
				SeparatorStyle.fixedWidth = 142;
				SeparatorStyle.fixedHeight = 1;
				SeparatorStyle.margin = new RectOffset(0, 0, 10, 10);
				SeparatorStyle.normal.background = Fabric.Internal.Editor.View.Render.MakeBackground (1, 1, new Color32 (255, 255, 255, 76));
			}

			public static void RenderMessage(string message)
			{
				GUILayout.BeginHorizontal (SectionStyle);
				GUILayout.FlexibleSpace ();
				GUILayout.Label (message, TextStyle);
				GUILayout.FlexibleSpace ();
				GUILayout.EndHorizontal ();
			}

			public static void RenderCheckbox(ref bool agreed)
			{
				GUILayout.BeginHorizontal (SectionStyle);
				GUILayout.FlexibleSpace ();

				bool toggle = false;
				toggle = GUILayout.Toggle (agreed, "I agree to the Twitter Kit Agreement and Developer Agreement", ToggleStyle);
				EditorGUIUtility.AddCursorRect (GUILayoutUtility.GetLastRect (), MouseCursor.Link);

				if (toggle != agreed) {
					agreed = toggle;
				}

				GUILayout.FlexibleSpace ();
				GUILayout.EndHorizontal ();
			}

			public static void RenderButton(bool enabled, string caption, Action onClick)
			{
				GUILayout.BeginHorizontal (SectionStyle);
				GUILayout.FlexibleSpace ();

				bool previous = GUI.enabled;

				GUI.enabled = enabled;
				if (GUILayout.Button (caption, ButtonStyle)) {
					onClick ();
				}
				GUI.enabled = previous;

				GUILayout.FlexibleSpace ();
				GUILayout.EndHorizontal ();
			}

			public static void RenderLinks(Action onTwitterAgreementLinkClicked, Action onDeveloperAgreementLinkClicked)
			{
				GUILayout.BeginHorizontal (SectionStyle);
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				Components.RenderLink ("Twitter Kit Agreement", onTwitterAgreementLinkClicked);
				Components.RenderLink ("Developer Agreement", onDeveloperAgreementLinkClicked);
				GUILayout.EndVertical ();
				GUILayout.FlexibleSpace ();
				GUILayout.EndHorizontal ();
			}

			public static void RenderSeparator ()
			{
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				GUILayout.Label ("", SeparatorStyle);
				GUILayout.FlexibleSpace ();
				GUILayout.EndHorizontal ();
			}

			private static void RenderLink(string caption, Action onClick)
			{
				if (GUILayout.Button (caption, LinkStyle)) {
					onClick ();
				}
				EditorGUIUtility.AddCursorRect (GUILayoutUtility.GetLastRect (), MouseCursor.Link);
			}
		}
		#endregion

		public override void RenderImpl(Rect position)
		{
			RenderHeader ("Twitter Account Creation");
			RenderFooter (back, null);
			Components.RenderMessage (
				"You'll need an account with Twitter before we get started. We can create one for you " +
				"automatically using your Fabric address, " + email + "."
			);
			Components.RenderCheckbox (ref agreed);
			Components.RenderLinks (onTwitterAgreementLinkClicked, onDeveloperAgreementLinkClicked);
			Components.RenderButton (agreed, "Create Account", onCreateAccountClicked);
			Components.RenderSeparator ();
			Components.RenderButton (true, "Use Existing Account", onAlreadyHaveAccountClicked);
		}
	}
}