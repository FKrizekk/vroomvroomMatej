using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
	//Settings
	public float sfxVolScale;
	public float musicVolScale;
	public float masterVolScale;
	public float sensitivity;

	//Progress
	public ObjectiveScript objScript;
	public static int objIndex = 400;//400 = its like a null
	public static string[] objectivesRemaining = new string[] {"Najdi <color=blue> kuřecí řízek","Poptej se o <color=blue> matějovi </color> ve vesnici", "AMONGYS", "ATGfaAGG", "HUGA CHUGA","AMOGOS AMOGOS", "BUNGA junhga"};
	//Tasked and Completed? No = N, Yes = Y
	//Not tasked = E
	public static string objectivesStatus = "";

	public MusicControllerScript musicControl;

	void Start()
	{
		UpdateVars();
		Debug.Log(objectivesStatus);
	}

	void Update(){
		
	}

	public void UpdateVars(){
		sfxVolScale = PlayerPrefs.GetFloat("sfxVolScale", 0.4f);
		musicVolScale = PlayerPrefs.GetFloat("musicVolScale", 0.4f);
		masterVolScale = PlayerPrefs.GetFloat("masterVolScale", 0.4f);
		sensitivity = PlayerPrefs.GetFloat("sensitivity", 0.4f) * 100;
		objIndex = PlayerPrefs.GetInt("objIndex", 400);
		objectivesStatus = PlayerPrefs.GetString("objectivesStatus", new string('E',objectivesRemaining.Length));
		if(objectivesStatus.Length < objectivesRemaining.Length)
		{
			objectivesStatus = objectivesStatus + new string('E',objectivesRemaining.Length-objectivesStatus.Length);
		}
		musicControl.ResetVolume();
	}
}