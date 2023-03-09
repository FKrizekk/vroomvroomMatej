using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catStatueScript : MonoBehaviour
{
	public AudioClip[] clips;
	
	AudioSource cameraSource;
	
	GameControllerScript gameControl;
	
	void Start()
	{
		gameControl = GameObject.Find("GameController").GetComponent<GameControllerScript>();
		cameraSource = GameObject.Find("Main Camera/RadioPARENT").GetComponent<AudioSource>();
	}
	
	public void Get()
	{
		cameraSource.PlayOneShot(clips[GameControllerScript.catStatuesFound], gameControl.dialogVolScale*gameControl.masterVolScale);
		
		switch (int.Parse(gameObject.name.Split("soska")[1]))
		{
			case 1:
				PlayerPrefs.SetInt("cat1", 1);
				break;
			case 2:
				PlayerPrefs.SetInt("cat2", 1);
				break;
			case 3:
				PlayerPrefs.SetInt("cat3", 1);
				break;
			case 4:
				PlayerPrefs.SetInt("cat4", 1);
				break;
			case 5:
				PlayerPrefs.SetInt("cat5", 1);
				break;
		}
		GameControllerScript.catStatuesFound++;
	}
}
