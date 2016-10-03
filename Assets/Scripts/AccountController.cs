using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using teruzuki.Twitter;

namespace teruzuki
{
	public class AccountController : MonoBehaviour
	{
		public RectTransform content;
		public RectTransform accountItem;

		private IEnumerator initializeItemCoroutine;

		private Client client;
		private IEnumerator requestTokenCoroutine;

		public GameObject accountList;
		public GameObject newAccount;

		void Start ()
		{
			client = new Client ();

			initializeItemCoroutine = InitializeItem ();
			StartCoroutine (initializeItemCoroutine);

			newAccount.SetActive(false);
		}

		private IEnumerator InitializeItem()
		{
			foreach(var account in AccountManager.Instance.AccountList)
			{
				var item = Instantiate (accountItem);
				item.SetParent (content, false);
				item.GetComponent<UIAccountItem>().Initialize(account);
				
				yield return new WaitForSeconds (0.1f);
			}
			yield return null;
		}

		private void RequestTokenCallback (string url)
		{
			Application.OpenURL (url);
		}

		private void AccessTokenCallback ()
		{
			SceneManager.LoadScene ("main");
		}

		private float opacityLeft = 0.0f;
		private float opacitySpeed = 2.0f;

		private float translateLeft = 0.0f;
		private float translateSpeed = 250.0f;

		private RectTransform newAccountRect;

		public void UIAddNewAccount() {
			newAccountRect = newAccount.GetComponent<RectTransform>();

			translateLeft = newAccountRect.rect.height / Screen.height * 100.0f;

			newAccount.SetActive(true);

			newAccount.transform.FindChild("PINInputField").GetComponentInChildren<Text>().text = "";

			var color = newAccount.GetComponent<Image>().color;
			color.a = 0.0f;
			newAccount.GetComponent<Image>().color = color;

			opacityLeft = 1.0f;

			if(requestTokenCoroutine == null)
			{
				requestTokenCoroutine = client.AcquireRequestToken (RequestTokenCallback);
				StartCoroutine (requestTokenCoroutine);
			}
		}

		void Update() {
//			if (translateLeft > 0.0f)
//			{
//				var translate = translateSpeed * Time.deltaTime;
//				if (translateLeft < translate)
//				{
//					translate = translateLeft;
//					translateLeft = 0.0f;
//				}
//				else
//				{
//					translateLeft -= translate;
//				}
//				newAccount.GetComponent<RectTransform>().Translate(new Vector3(0, translate, 0));
//			}
			if (opacityLeft > 0.0f)
			{
				var opacity = opacitySpeed * Time.deltaTime;
				if (opacityLeft < opacity)
				{
					opacity = opacityLeft;
					opacityLeft = 0.0f;
				}
				else
				{
					opacityLeft -= opacity;
				}
				var color = newAccount.GetComponent<Image>().color;
				color.a += opacity;
				newAccount.GetComponent<Image>().color = color;
			}
		}

		public void UINewAccountSubmit() {
			newAccount.SetActive(false);
		}

		public void UINewAccountCancel() {
			newAccount.SetActive(false);
		}
	}
}
