using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
	
	//Settings
	public float sfxVolScale;
	public float musicVolScale;
	public float masterVolScale;
	public float sensitivity;
	public float dialogVolScale;
	
	public bool vsync;
	public int antiAliasing;
	public bool anisotropicFiltering;

	//Progress
	public ObjectiveScript objScript;
	public static int objIndex = 400;//400 = its like a null
	public static string[] objectivesRemaining = new string[] {"Najdi <color=blue> kuřecí řízek","Poptej se o <color=blue> matějovi </color> ve vesnici", "AMONGYS", "ATGfaAGG", "HUGA CHUGA","AMOGOS AMOGOS", "BUNGA junhga"};
	//Tasked and Completed? No = N, Yes = Y
	//Not tasked = E
	public static string objectivesStatus = "";
	
	//INVENTORY
	//ITEMNAME-IMGINDEX-AMOUNT,ITEMNAME-IMGINDEX-AMOUNT...
	public static string inventory = "Gold bars-0-1";

	public MusicControllerScript musicControl;
	
	public InventoryScript inventoryScript;

	void Start()
	{
		UpdateVars();
		Debug.Log(objectivesStatus);
	}

	void Update(){
		if(Input.GetKeyDown("g"))
		{
			var temp = objectivesStatus.ToCharArray();
			temp[0] = 'E';
			objectivesStatus = new string(temp);
		}
		if(Input.GetKeyDown("h"))
		{
			var temp = objectivesStatus.ToCharArray();
			temp[0] = 'Y';
			objectivesStatus = new string(temp);
		}
	}

	public void UpdateVars(){
		sfxVolScale = PlayerPrefs.GetFloat("sfxVolScale", 0.4f);
		musicVolScale = PlayerPrefs.GetFloat("musicVolScale", 0.4f);
		masterVolScale = PlayerPrefs.GetFloat("masterVolScale", 0.4f);
		sensitivity = PlayerPrefs.GetFloat("sensitivity", 0.4f) * 100;
		objIndex = PlayerPrefs.GetInt("objIndex", 400);
		objectivesStatus = PlayerPrefs.GetString("objectivesStatus", new string('E',objectivesRemaining.Length));
		MatejController.health = PlayerPrefs.GetInt("matejHealth", 10000);
		PlayerScript.health = PlayerPrefs.GetInt("playerHealth", 100);
		PlayerScript.carHealth = PlayerPrefs.GetInt("playerCarHealth", 1000);
		dialogVolScale = PlayerPrefs.GetFloat("dialogVolScale", 0.4f);
		inventory = PlayerPrefs.GetString("inventory", "");
		if(SceneManager.GetActiveScene().name == "MainScene")
		{
			inventoryScript.UpdateInventory();
		}
		if(PlayerPrefs.GetInt("vsync", 0) == 0)
		{
			vsync = false;
			QualitySettings.vSyncCount = 0;
		}else
		{
			vsync = true;
			QualitySettings.vSyncCount = 1;
		}
		if(PlayerPrefs.GetInt("anisotropicFiltering", 0) == 0)
		{
			anisotropicFiltering = false;
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
		}else
		{
			anisotropicFiltering = true;
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
		}
		
		antiAliasing = PlayerPrefs.GetInt("antiAliasing", 8);
		QualitySettings.antiAliasing = antiAliasing;
		
		
		
		Debug.Log("Set vsync to: " + vsync);
		Debug.Log("Set Anti-aliasing: " + antiAliasing);
		
		
		Debug.Log("---------------LOADED---------------\n" + "sfxVolScale: " + sfxVolScale + "\nmusicVolScale: " + musicVolScale + "\nmasterVolScale: " + masterVolScale + "\ndialogVolScale: " + dialogVolScale + "\nsensitivity: " + sensitivity + "\nobjIndex: " + objIndex + "\nobjectivesStatus: " + objectivesStatus + "\nMatejHealth: " + MatejController.health + "\nPlayerHealth: " + PlayerScript.health  + "\nPlayerCarHealth: " + PlayerScript.carHealth);
		
		if(objectivesStatus.Length < objectivesRemaining.Length)
		{
			objectivesStatus = objectivesStatus + new string('E',objectivesRemaining.Length-objectivesStatus.Length);
		}
		musicControl.ResetVolume();
	}
}