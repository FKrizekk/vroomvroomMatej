using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetodejuvBarakScript : MonoBehaviour
{
	public AudioClip[] explosionSounds;
	
	public AudioClip scream;
	
	public AudioSource source;
	
	public GameControllerScript gameControl;
	
	
	void Start()
	{
		gameControl = GameObject.Find("GameController").GetComponent<GameControllerScript>();
	}
	
	public void PlayExplosionSound()
	{
		source.PlayOneShot(explosionSounds[Random.Range(0,3)], gameControl.masterVolScale*gameControl.sfxVolScale);
	}
	
	public void PlayMetodejScream()
	{
		source.PlayOneShot(scream, gameControl.masterVolScale*gameControl.sfxVolScale);
	}
}
