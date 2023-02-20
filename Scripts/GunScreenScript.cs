using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScreenScript : MonoBehaviour
{
	public Material mat;
	
	public Camera cam;
	
	// Take a "screenshot" of a camera's Render Texture.
	Texture2D RTImage(Camera camera)
	{
		// The Render Texture in RenderTexture.active is the one
		// that will be read by ReadPixels.
		var currentRT = RenderTexture.active;
		RenderTexture.active = camera.targetTexture;

		// Render the camera's view.
		camera.Render();

		// Make a new texture and read the active Render Texture into it.
		Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
		image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
		image.Apply();

		// Replace the original active Render Texture.
		RenderTexture.active = currentRT;
		return image;
	}
	
	void Start()
	{
		StartCoroutine(Display());
	}
	
	IEnumerator Display()
	{
		mat.mainTexture = RTImage(cam);
		yield return new WaitForSeconds(Time.deltaTime);
		StartCoroutine(Display());
	}
}