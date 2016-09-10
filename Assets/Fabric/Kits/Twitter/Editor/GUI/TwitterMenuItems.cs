namespace Fabric.Internal.Twitter.Editor 
{
	using UnityEngine;
	using UnityEditor;
	
	public static class TwitterMenuItems
	{	
		[MenuItem("Fabric/Twitter/Documentation", false, 1)]
		public static void OpenTwitterDocs ()
		{
			Application.OpenURL ("https://docs.fabric.io/unity/twitter/index.html");
		}
	}
	
}
