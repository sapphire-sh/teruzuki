namespace Fabric.Internal.Twitter.Editor.Controller
{
	using UnityEngine;
	using System;
	using System.Collections.Generic;
	using Fabric.Internal.Editor.Controller;
	using Fabric.Internal.Editor.Model;
	using Fabric.Internal.Editor.View;
	using Fabric.Internal.Editor.View.Templates;
	using Fabric.Internal.ThirdParty.MiniJSON;
	using MetaTuple = Fabric.Internal.Editor.Model.Settings.InstalledKit.MetaTuple;
	
	internal class Controller : KitController
	{
		private const string TwitterKey = "TwitterKey";
		private const string TwitterSecret = "TwitterSecret";
		private const string Twitter = "Twitter";
		private const string PrefabName = "TwitterInit";

		#region Pages
		private Page provision = null;
		private Page Provision
		{
			get {
				if (provision == null) {
					provision = new View.KeyProvisioningPage (
						Settings.Instance.Email,
						BackToKitSelector (),
						AlreadyHaveAccount (),
						TwitterAgreementLink (),
						DeveloperAgreementLink (),
						ProvisionKeys ()
					);

					Fabric.Internal.Editor.Update.PeriodicPinger.Enqueue (new Fabric.Internal.Editor.Analytics.Events.PageViewEvent {
						ScreenName = "KeyProvisioningPage (Twitter)",
					});
				}
				return provision;
			}
		}

		private Page entry = null;
		private Page Entry
		{
			get {
				if (entry == null) {
					entry = new View.KeyEntryPage (BackToKeyProvisioning (), SaveTwitterSecrets ());

					Fabric.Internal.Editor.Update.PeriodicPinger.Enqueue (new Fabric.Internal.Editor.Analytics.Events.PageViewEvent {
						ScreenName = "KeyEntryPage (Twitter)",
					});
				}
				return entry;
			}
		}

		private Page instructions = null;
		private Page Instructions
		{
			get {
				if (instructions == null) {
					instructions = new InstructionsPage (ApplyKitChanges (), BackToKitSelector (), new List<string> () {
						"◈ Set execution order of Fabric scripts",
						"◈ Replace application class in top-level AndroidManifest.xml",
						"◈ Inject metadata in Fabric's AndroidManifest.xml"
					});

					Fabric.Internal.Editor.Update.PeriodicPinger.Enqueue (new Fabric.Internal.Editor.Analytics.Events.PageViewEvent {
						ScreenName = "InstructionsPage (Twitter)",
					});
				}
				return instructions;
			}
		}

		private Page prefab = null;
		private Page Prefab
		{
			get {
				if (prefab == null) {
					prefab = new PrefabPage (AdvanceToValidationPage (), PrefabName, typeof (Fabric.Internal.Twitter.TwitterInit));

					Fabric.Internal.Editor.Update.PeriodicPinger.Enqueue (new Fabric.Internal.Editor.Analytics.Events.PageViewEvent {
						ScreenName = "PrefabPage (Twitter)",
					});
				}
				return prefab;
			}
		}

		private Page documentation = null;
		private Page Documentation
		{
			get {
				if (documentation == null) {
					documentation = new View.DocumentationPage (BackToKitSelector ());

					Fabric.Internal.Editor.Update.PeriodicPinger.Enqueue (new Fabric.Internal.Editor.Analytics.Events.PageViewEvent {
						ScreenName = "DocumentationPage (Twitter)",
					});
				}
				return documentation;
			}
		}
		#endregion

		private List<MetaTuple> keys = new List<MetaTuple> ();

		public Controller(Fabric.Internal.Editor.API.V1 api)
		{
		}

		public Version Version()
		{
			return Fabric.Internal.Twitter.Editor.Info.Version;
		}

		public KitControllerStatus PageFromState(out Page page)
		{
			List<Settings.InstalledKit> installedKits = Settings.Instance.InstalledKits;
			Settings.InstalledKit twitterKit = installedKits.Find (kit => kit.Name.Equals (Twitter, StringComparison.OrdinalIgnoreCase));

			switch (twitterKit.InstallationStatus) {
			case Settings.KitInstallationStatus.Installed:
				return ShowInstalledPage (out page);
			case Settings.KitInstallationStatus.Imported:
				return ShowInstallationFlowPage (Settings.Instance.FlowSequence, out page);
			case Settings.KitInstallationStatus.Configured:
			default:
				return ShowConfiguredPage (out page);
			}
		}

		private KitControllerStatus ShowInstallationFlowPage(int flowSequence, out Page page)
		{
			switch (flowSequence) {
			case 0:
				page = Provision;
				return KitControllerStatus.NextPage;
			case 1:
				page = Entry;
				return KitControllerStatus.NextPage;
			case 2:
				page = Instructions;
				return KitControllerStatus.NextPage;
			case 3:
				page = Prefab;
				return KitControllerStatus.NextPage;
			default:
				return ShowConfiguredPage (out page);
			}
		}

		private KitControllerStatus ShowConfiguredPage(out Page page)
		{
			page = null;
			return KitControllerStatus.LastPage;
		}

		private KitControllerStatus ShowInstalledPage(out Page page)
		{
			page = Documentation;
			return KitControllerStatus.NextPage;
		}

		public string DisplayName()
		{
			return Twitter;
		}

		private static Action TwitterAgreementLink()
		{
			return delegate() {
				Application.OpenURL ("https://fabric.io/terms/twitter");
			};
		}

		private static Action DeveloperAgreementLink()
		{
			return delegate() {
				Application.OpenURL ("https://dev.twitter.com/overview/terms/agreement");
			};
		}

		private static Action BackToKitSelector()
		{
			return delegate() {
				Settings.Instance.Kit = "";
			};
		}

		private static Action BackToKeyProvisioning()
		{
			return delegate() {
				Settings.Instance.FlowSequence = 0;
			};
		}

		private static Action AlreadyHaveAccount()
		{
			return delegate() {
				Settings.Instance.FlowSequence = 1;
			};
		}

		private static Action AdvanceToValidationPage()
		{
			return delegate() {
				Settings.Instance.FlowSequence = 4;

				Fabric.Internal.Editor.Update.PeriodicPinger.Enqueue (new Fabric.Internal.Editor.Analytics.Events.PageViewEvent {
					ScreenName = "ValidationPage (Twitter)",
				});
			};
		}

		private Action ApplyKitChanges()
		{
			return delegate () {
				List<MetaTuple> android = keys.FindAll (tuple => tuple.Platform.Equals ("android"));

				// Only persist Android twitter keys to the manifest.

				string twitterKey = android.Find (tuple => tuple.Key.Equals (TwitterKey)).Value;
				string twitterSecret = android.Find (tuple => tuple.Key.Equals (TwitterSecret)).Value;

				TwitterSetup.EnableTwitter (twitterKey, twitterSecret);
				Settings.Instance.FlowSequence = 3;
			};
		}

		private Action<List<MetaTuple>> SaveTwitterSecrets()
		{
			return delegate(List<MetaTuple> keys) {
				if (keys.Count == 0) {
					return;
				}

				List<Settings.InstalledKit> installedKits = Settings.Instance.InstalledKits;
				List<MetaTuple> meta = new List<MetaTuple> (keys);

				Settings.InstalledKit twitterKit = installedKits.Find (
					installed => installed.Name.Equals (Twitter, StringComparison.OrdinalIgnoreCase)
				);

				twitterKit.Meta = meta;
				this.keys = keys;
				Settings.Instance.FlowSequence = 2;
			};
		}

		// This is called on a non-main thread.
		private static List<MetaTuple> ProvisionKeysForPlatform(string platform, string orgId, Fabric.Internal.Editor.API.V1 api)
		{
			string responseJson = api.HttpPost ("/api/v3/organizations/" + orgId + "/twitter/tokens?platform=" + platform);

			Dictionary<string, object> response = Json.Deserialize (responseJson) as Dictionary<string,object>;
			string twitterKey = response ["consumer_key"] as string;
			string twitterSecret = response ["secret"] as string;

			return new List<MetaTuple> () {
				new MetaTuple { Key = TwitterKey, Value = twitterKey, Platform = platform },
				new MetaTuple { Key = TwitterSecret, Value = twitterSecret, Platform = platform }
			};
		}

		private static List<MetaTuple> ProvisionKeys(string orgId, Fabric.Internal.Editor.API.V1 api)
		{
			List<MetaTuple> keys = new List<MetaTuple> ();

			keys.AddRange (ProvisionKeysForPlatform ("android", orgId, api));
			keys.AddRange (ProvisionKeysForPlatform ("ios", orgId, api));

			return keys;
		}

		private Action ProvisionKeys()
		{
			string orgId = Settings.Instance.Organization.Id;
			return delegate {
				Fabric.Internal.Editor.API.AsyncV1.Fetch<List<MetaTuple>> (
					(List<MetaTuple> keys) => {
						SaveTwitterSecrets ()(keys);
					},
					(string message) => {
						Fabric.Internal.Editor.Utils.Warn ("Couldn't provision Twitter Keys; {0}", message);
					},
					(Fabric.Internal.Editor.API.V1 api) => {
						return ProvisionKeys (orgId, api);
					}
				);
			};
		}
	}
}
