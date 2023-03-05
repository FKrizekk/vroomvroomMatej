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
		
		GameControllerScript.catStatuesFound++;
	}
}
