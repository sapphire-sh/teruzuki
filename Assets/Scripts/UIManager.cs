using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace teruzuki
{
	public class UIManager : MonoBehaviour
	{
		public string pin = "";

		void OnGUI()
		{
			pin = GUI.TextField(new Rect(10, 10, 200, 20), pin);

			if(GUI.Button(new Rect(10, 70, 50, 30), "Submit"))
			{
				var manager = FindObjectOfType<TwitterManager>();
				manager.GetAccessToken(pin);
			}
		}
	}
}
