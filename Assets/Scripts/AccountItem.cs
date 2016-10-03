using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.UI;using UnityEngine.SceneManagement;public class AccountItem : MonoBehaviour
{	private float rotationLeft = 180.0f;	private float rotationSpeed = 200.0f;	private float zoomLeft = 10.0f;	private float zoomSpeed = 50.0f;	private bool isLoaded = false;	private RectTransform rectTransform;

	public GameObject profileImage;	public GameObject screenName;	void Start ()
	{		rectTransform = GetComponent<RectTransform> ();		rectTransform.Rotate (Vector3.right, 180.0f);		rectTransform.Translate (new Vector3 (0.0f, 0.0f, -zoomLeft));		StartCoroutine (downloadImage ());	}	void Update ()
	{		if (rotationLeft > 0.0f)
		{
			var rotation = Time.deltaTime * rotationSpeed;
			if (rotationLeft > rotation)
			{
				rotationLeft -= rotation;
			}
			else
			{
				rotation = rotationLeft;
				rotationLeft = 0.0f;
			}
			rectTransform.Rotate (Vector3.right, rotation);
		}		else if (zoomLeft > 0.0f)
		{
			var zoom = Time.deltaTime * zoomSpeed;
			if (zoomLeft > zoom)
			{
				zoomLeft -= zoom;
			}
			else
			{
				zoom = zoomLeft;
				zoomLeft = 0.0f;
			}
			rectTransform.Translate (0.0f, 0.0f, -zoom);
		}	}	private IEnumerator downloadImage ()
	{
		while (!Caching.ready)
		{
			yield return null;
		}

		var www = new WWW ("https://pbs.twimg.com/profile_images/747596515871358976/uEb5J9WP_400x400.jpg");
		yield return www;

		if (!string.IsNullOrEmpty (www.error))
		{
			Debug.Log (www.error);
			yield return null;
		}

		profileImage.GetComponent<Image>().sprite = Sprite.Create (www.texture, new Rect (0, 0, www.texture.width, www.texture.height), new Vector2 (0, 0));
		screenName.GetComponent<Text> ().text = "@sapphire_dev";

		isLoaded = true;
	}	public void OnClick()
	{
		if (isLoaded)
		{
			SceneManager.LoadScene ("main");
		}
	}}