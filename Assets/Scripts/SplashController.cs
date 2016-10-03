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

		private Transform logoTransform;

		public void Initialize(Transform transform)
		{
			logoTransform = transform;

			initializeCoroutine = Initialize();
			StartCoroutine(initializeCoroutine);
		}

		void Update()
		{
			if (logoTransform != null)
			{
				logoTransform.Rotate(Vector3.up, Time.deltaTime * 100.0f, Space.World);
			}
		}

		private IEnumerator Initialize()
		{
			TokenManager.Instance.TokenList.ForEach(x => AccountManager.Instance.AccountList.Add(new Account(x)));
			Debug.Log(AccountManager.Instance.AccountList.Count(x => x.isLoaded));
			while (AccountManager.Instance.AccountList.Count(x => x.isLoaded) < TokenManager.Instance.TokenList.Count)
			{
				yield return new WaitForSeconds(0.1f);
			}
			SceneManager.LoadScene("account");
			yield return null;
		}
	}
}
