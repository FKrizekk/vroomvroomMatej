using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetodejScript : MonoBehaviour
{
	public AudioClip[] clips;
	
	public AudioSource source;
	
	public GameControllerScript gameControl;
	
	public void PlayScream()
	{
		source.PlayOneShot(clips[2], gameControl.masterVolScale*gameControl.dialogVolScale);
	}
}
