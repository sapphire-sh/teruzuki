namespace Fabric.Internal.Twitter.Editor
{
	using UnityEngine;
	using System.Collections;
	using Fabric.Internal.Editor;
	using Fabric.Internal.Editor.Model;
	
	public class TwitterSetup : FabricSetup
	{
		public static void EnableTwitter (string twitterKey, string twitterSecret)
		{
			EnableTwitterAndroid (twitterKey, twitterSecret);
			// iOS is covered in the post-build script.
		}

		private static void EnableTwitterAndroid(string twitterKey, string twitterSecret)
		{
			string unityManifestPath = FindUnityAndroidManifest ();

			if (unityManifestPath == null) {
				Utils.Warn ("Could not find Unity's AndroidManifest.xml file, cannot initialize Twitter for Android.");
				return;
			}

			BootstrapTopLevelManifest (unityManifestPath);
			ToggleApplicationInTopLevelManifest (enableFabric: true);
			SetKitScriptExecutionOrder (typeof(Fabric.Internal.Twitter.TwitterInit));

			InjectMetadataIntoFabricManifest ("io.fabric.ApiKey", Settings.Instance.Organization.ApiKey);

			InjectMetadataIntoFabricManifest ("io.fabric.twittercore.key", twitterKey);
			InjectMetadataIntoFabricManifest ("io.fabric.twittercore.secret", twitterSecret);
			InjectMetadataIntoFabricManifest ("io.fabric.twittercore.qualified", "com.twitter.sdk.android.core.TwitterCore");
			InjectMetadataIntoFabricManifest ("io.fabric.twittercore.unqualified", "TwitterCore");
			InjectMetadataIntoFabricManifest ("io.fabric.kits", "twittercore", true);

			InjectMetadataIntoFabricManifest ("io.fabric.tweetcomposer.qualified", "com.twitter.sdk.android.tweetcomposer.TweetComposer");
			InjectMetadataIntoFabricManifest ("io.fabric.tweetcomposer.unqualified", "TweetComposer");
			InjectMetadataIntoFabricManifest ("io.fabric.kits", "tweetcomposer", true);
		}

	}
}
