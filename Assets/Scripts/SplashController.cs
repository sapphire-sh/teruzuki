using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

using teruzuki.Twitter;

namespace teruzuki
{
	public class SplashController : MonoBehaviour
	{
		private IEnumerator initializeCoroutine;

		public Transform logo;

		void Start ()
		{
			initializeCoroutine = Initialize ();
			StartCoroutine (initializeCoroutine);
		}

		void Update ()
		{
			logo.Rotate (Vector3.up, Time.deltaTime * 100.0f, Space.World);
		}

		private IEnumerator Initialize ()
		{
			AccountManager.Instance.AccountList.ToList ().ForEach (x => {
				StartCoroutine(x.Initialize());
			});
			while (AccountManager.Instance.AccountList.All (x => x.IsLoaded) == false) {
				yield return new WaitForSeconds (0.1f);
			}
			SceneManager.LoadScene ("account");
		}
	}
}
