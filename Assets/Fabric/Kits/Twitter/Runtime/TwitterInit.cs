namespace Fabric.Internal.Twitter
{
	using UnityEngine;
	using Fabric.Twitter;

	public class TwitterInit : MonoBehaviour
	{
		private static TwitterInit instance;

		void Awake()
		{
			// This singleton pattern ensures AwakeOnce() is only called once even when the scene
			// is reloaded (loading scenes destroy previous objects and wake up new ones)
			if (instance == null) {
				AwakeOnce ();

				instance = this;
				DontDestroyOnLoad(this);
			} else if (instance != this) {
				Destroy(this.gameObject);
			}
		}

		private void AwakeOnce ()
		{
			Twitter.Init();
		}
	}
}
