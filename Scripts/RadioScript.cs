using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RadioScript : MonoBehaviour
{
	//Directory of folder to be searched anywhere on the computer
	public string FileDirectory;

	//Audio source
	public AudioSource Source;

	//List of all valid directories
	List<string> Files = new List<string>();
	//List of all AudioClips
	List<AudioClip> Clips = new List<AudioClip>();
	
	bool playRadio = false;
	
	public PlayerScript playerScript;
	
	public GameControllerScript gameControl;
	
	float startTime;

	private void Start()
	{
		UpdateFiles();
	}
	
	public void UpdateFiles()
	{
		//Grabs all files from FileDirectory
		string[] files;
		files = Directory.GetFiles(FileDirectory);

		//Checks all files and stores all WAV files into the Files list.
		for (int i = 0; i < files.Length; i++)
		{
			if (files[i].EndsWith(".wav"))
			{
				Files.Add(files[i]);
				Clips.Add(new WWW(files[i]).GetAudioClip(false, true, AudioType.WAV));
			}
		}
		
		for (int i = 0; i < Clips.Count; i++) {
			var temp = Clips[i];
			int randomIndex = Random.Range(i, Clips.Count);
			Clips[i] = Clips[randomIndex];
			Clips[randomIndex] = temp;
		}
		
		startTime = Time.time;
	}
	
	IEnumerator RadioLoop()
	{
		if(Clips != null)
		{
			foreach (var clip in Clips)
			{
				Source.PlayOneShot(clip, gameControl.masterVolScale*gameControl.musicVolScale);
				yield return new WaitForSeconds(0.1f);
				Debug.Log(Time.time - startTime);
				Source.timeSamples = Source.timeSamples + (int)(Time.time - startTime);
				yield return new WaitForSecondsRealtime(clip.length);
			}
		}
		StartCoroutine(RadioLoop());
	}
	
	
	
	void Update()
	{
		if(playerScript.isInCar)
		{
			if(Input.GetKeyDown("r"))
			{
				if(playRadio)
				{
					playRadio = false;
					StopCoroutine(RadioLoop());
					Source.Stop(); 
				}else
				{
					playRadio = true;
					StartCoroutine(RadioLoop());
				}
			}
		}
	}
}