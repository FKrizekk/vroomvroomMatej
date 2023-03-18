using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarScript : MonoBehaviour
{
	AudioSource altarSource;
	
	public AudioClip[] clips;
	
	GameControllerScript gameControl;
	
	Animator anim;
	
	PortalScript portal;
	
	void Start()
	{
		gameControl = GameObject.Find("GameController").GetComponent<GameControllerScript>();
		altarSource = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		portal = GameObject.Find("Portal").GetComponent<PortalScript>();
		
		if(GameObject.Find("Arnost") == null)
		{
			anim.SetBool("Exist", true);
			anim.SetBool("Lit", true);
		}
	}
	
	void Update()
	{
		
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
	
	public void SpawnIgolath()
	{
		portal.SpawnIgolath();
	}
	
	IEnumerator Hum()
	{
		Debug.Log("HUM START");
		altarSource.PlayOneShot(clips[2], gameControl.masterVolScale*gameControl.sfxVolScale);
		yield return new WaitForSecondsRealtime(clips[2].length);
		StartCoroutine(Hum());
	}
}
