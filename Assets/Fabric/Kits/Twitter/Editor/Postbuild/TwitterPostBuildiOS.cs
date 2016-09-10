namespace Fabric.Internal.Twitter.Editor.Postbuild
{	
	using UnityEngine;
	using UnityEditor;
	using UnityEditor.Callbacks;
	using System.Collections;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;
	using System.Linq;
	using Fabric.Internal.Editor.Model;
	using Fabric.Internal.Editor.Postbuild;
	using Fabric.Internal.Editor.ThirdParty.xcodeapi;
	
	public class TwitterPostBuildiOS : PostBuildiOS
	{
		private static string consumerKey;
		private static string consumerSecret;

		private static string[] platformFrameworks = new string[] {
			"Accounts.framework",
			"CoreData.framework",
			"CoreGraphics.framework",
			"Foundation.framework",
			"Security.framework",
			"Social.framework",
			"UIKit.framework"
		};
		
		private static string[] frameworks = new string[] {
			"Fabric.framework",
			"TwitterCore.framework",
			"TwitterKit.framework",
			"TwitterKitResources.bundle"
		};

		private static string[] libs = new string[] {
			"Fabric-Init/libFabriciOSInit.a",
			"TwitterKit-Wrapper/libTwitterKitiOSWrapper.a"
		};

		[PostProcessBuild(100)]
		public static void OnPostprocessBuild(BuildTarget buildTarget, string buildPath)
		{
			if (!IsKitOnboarded ("Twitter")) {
				Fabric.Internal.Editor.Utils.Warn ("Twitter not yet onboarded, skipping post-build steps.");
				return;
			}

			// BuiltTarget.iOS is not defined in Unity 4, so we just use strings here
			if (buildTarget.ToString () == "iOS" || buildTarget.ToString () == "iPhone") {
				CheckiOSVersion ();
				GetConsumerKeyAndSecret ();

				PrepareProject (buildPath);
				PreparePlist (buildPath);
			}
		}

		private static void CheckiOSVersion()
		{
			iOSTargetOSVersion[] oldiOSVersions = {
				iOSTargetOSVersion.iOS_4_0,
				iOSTargetOSVersion.iOS_4_1,
				iOSTargetOSVersion.iOS_4_2,
				iOSTargetOSVersion.iOS_4_3,
				iOSTargetOSVersion.iOS_5_0,
				iOSTargetOSVersion.iOS_5_1,
				iOSTargetOSVersion.iOS_6_0
			};
			var isOldiOSVersion = oldiOSVersions.Contains (PlayerSettings.iOS.targetOSVersion);
			
			if (isOldiOSVersion) {
				Fabric.Internal.Editor.Utils.Error ("Twitter requires iOS 7+. Please change the Target iOS Version in Player Settings to iOS 7 or higher.");
			}
		}

		private static void GetConsumerKeyAndSecret() 
		{
			List<Settings.InstalledKit> installedKits = Settings.Instance.InstalledKits;
			Settings.InstalledKit twitter = installedKits.Find (k => k.Name.Equals ("Twitter"));

			if (twitter != null) {
				List<Settings.InstalledKit.MetaTuple> metaPairs = twitter.Meta;
				List<Settings.InstalledKit.MetaTuple> ios = metaPairs.FindAll (tuple => tuple.Platform.Equals ("ios"));

				if (ios.Count != 0) {
					consumerKey = ios.Find (tuple => tuple.Key.Equals ("TwitterKey")).Value;
					consumerSecret = ios.Find (tuple => tuple.Key.Equals ("TwitterSecret")).Value;
				}
			}

			if (string.IsNullOrEmpty (consumerKey) || string.IsNullOrEmpty (consumerSecret)) {
				Fabric.Internal.Editor.Utils.Error ("Could not find consumer key/secret for TwitterKit. Please run the Fabric installer to get set up.");
			}
		} 

		private static void PrepareProject(string buildPath)
		{
			string projPath = Path.Combine (buildPath, "Unity-iPhone.xcodeproj/project.pbxproj");
			PBXProject project = new PBXProject ();
			project.ReadFromString (File.ReadAllText(projPath));		
			string target = project.TargetGuidByName ("Unity-iPhone");
			
			AddPlatformFrameworksToProject (platformFrameworks, project, target);		
			AddFrameworksToProject (frameworks, buildPath, project, target);		
			AddLibsToProject (libs, project, target, buildPath);

			File.WriteAllText (projPath, project.WriteToString());			
		}

		private static void PreparePlist(string buildPath)
		{
			Dictionary<string, PlistElementDict> kitsDict = new Dictionary<string, PlistElementDict>();			
			PlistElementDict twitterDict = new PlistElementDict ();

			twitterDict.SetString("consumerKey", consumerKey);
			twitterDict.SetString("consumerSecret", consumerSecret);
			kitsDict.Add("Twitter", twitterDict);
			AddFabricKitsToPlist(buildPath, kitsDict);
		}
	}
}
