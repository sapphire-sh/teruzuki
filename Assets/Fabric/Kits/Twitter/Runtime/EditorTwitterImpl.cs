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

#if UNITY_EDITOR
namespace Fabric.Internal.Twitter
{
	using Fabric.Twitter;
	using Fabric.Internal.Runtime;

	internal class EditorTwitterImpl : ITwitter
	{
		protected const string KitName = "Twitter";

		private TwitterSession EditorSession;

		public EditorTwitterImpl ()
		{
			AuthToken editorAuthToken = new AuthToken ("editorToken", "editorSecret");
			EditorSession = new TwitterSession (0, "Editor", editorAuthToken);
		}

		public void LogIn ()
		{
			Utils.Log (KitName, "Would call Twitter login on a physical device.");
		}

		public void LogOut ()
		{
			Utils.Log (KitName, "Would call Twitter logout on a physical device.");
		}

		public TwitterSession Session ()
		{
			return EditorSession;
		}

		public void RequestEmail (TwitterSession session)
		{
			Utils.Log (KitName, "Would call Twitter RequestEmail on a physical device for user " + session.userName);
		}

		public void Compose (TwitterSession session, Card card)
		{
			Utils.Log (KitName, "Would call Twitter Compose on a physical device for user " + session.userName + " with Image URI " + card.imageUri);
		}
	}
}
#endif
