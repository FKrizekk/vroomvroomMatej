using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
	Animator anim;
	
	GameControllerScript gameControl;
	
	AudioSource source;
	
	public AudioClip[] clips;
	
	bool sparking = false;
	
	void Start()
	{
		gameControl = GameObject.Find("GameController").GetComponent<GameControllerScript>();
		anim = GetComponent<Animator>();
		source = GetComponent<AudioSource>();
	}
	
	public void SpawnIgolath()
	{
		anim.SetBool("SpawnIgolath", true);
	}
	
	public void PlayThunderSound()
	{
		source.PlayOneShot(clips[0], gameControl.masterVolScale*gameControl.sfxVolScale);
	}
	
	public void SparksStart()
	{
		if(!sparking)
		{
			//StartCoroutine(Sparks());
		}
	}
	
	IEnumerator Sparks()
	{
		sparking = true;
		source.PlayOneShot(clips[1], gameControl.masterVolScale*gameControl.sfxVolScale);
		yield return new WaitForSecondsRealtime(clips[1].length);
		StartCoroutine(Sparks());
	}
}
