using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace teruzuki
{
	public class UIManager : MonoBehaviour
	{
		private string pin = "";

		void OnGUI()
		{
			pin = GUI.TextField(new Rect(10, 10, 400, 40), pin);

			if(GUI.Button(new Rect(10, 60, 400, 40), "Submit"))
			{
				var manager = FindObjectOfType<TwitterManager>();
				manager.GetAccessToken(pin);
			}
		}
	}
}
