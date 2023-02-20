using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carEngineScript : MonoBehaviour
{
	public AudioSource source;
	
	public AudioSource voiceSource;
	
	public GameControllerScript gameControl;

	public float pitch;
	
	public GameObject player;
	
	public AudioClip[] clips;

	void Start()
	{
		StartCoroutine(engine());
	}
	
	void Update()
	{
		source.pitch = Mathf.SmoothStep(source.pitch, pitch, Time.deltaTime*10);
		source.volume = gameControl.sfxVolScale * gameControl.masterVolScale;	
	}
	
	IEnumerator engine()
	{
		yield return new WaitUntil(() => Vector3.Distance(player.transform.position,transform.position) > 20);
		//Shutting down
		pitch = 0;
		
		
		yield return new WaitUntil(() => Vector3.Distance(player.transform.position,transform.position) < 20);
		//Powering up
		source.pitch = 1;
		pitch = 1;
		source.Stop();
		source.PlayOneShot(clips[1],gameControl.sfxVolScale * gameControl.masterVolScale + 0.5f);
		voiceSource.PlayOneShot(clips[0],gameControl.sfxVolScale * gameControl.masterVolScale);
		yield return new WaitForSecondsRealtime(clips[1].length);
		source.pitch = 0;
		source.Play();
		
		
		
		StartCoroutine(engine());
	}
	
	public void RadarContact()
	{
		voiceSource.PlayOneShot(clips[2],gameControl.sfxVolScale * gameControl.masterVolScale);
	}

	public void setPitch(float wantedPitch){
		pitch = wantedPitch;
	}
}
