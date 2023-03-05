using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarScript : MonoBehaviour
{
	AudioSource altarSource;
	
	public AudioClip[] clips;
	
	GameControllerScript gameControl;
	
	void Start()
	{
		gameControl = GameObject.Find("GameController").GetComponent<GameControllerScript>();
		altarSource = GetComponent<AudioSource>();
	}
	
	public void BangSound()
	{
		altarSource.PlayOneShot(clips[1], gameControl.masterVolScale*gameControl.sfxVolScale);
	}
	
	public void LightUpStart()
	{
		altarSource.PlayOneShot(clips[0], gameControl.masterVolScale*gameControl.sfxVolScale);
	}
	
	public void IdleHum()
	{
		StartCoroutine(Hum());
	}
	
	IEnumerator Hum()
	{
		Debug.Log("HUM START");
		altarSource.PlayOneShot(clips[2], gameControl.masterVolScale*gameControl.sfxVolScale);
		yield return new WaitForSecondsRealtime(clips[2].length);
		StartCoroutine(Hum());
	}
}
