using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Cars
{
	public GameObject carlvl1;
	public GameObject carlvl2;
	public GameObject carlvl3;
	public GameObject carlvl4;
	public GameObject carlvl5;
}

public class GameControllerScript : MonoBehaviour
{
	
	//Settings
	public float sfxVolScale;
	public float musicVolScale;
	public float masterVolScale;
	public float sensitivity;
	public float dialogVolScale;
	public float ambientVolScale;
	
	public bool vsync;
	public int antiAliasing;
	public bool anisotropicFiltering;

	//Progress
	public ObjectiveScript objScript;
	public static int objIndex = 400;//400 = its like a null
	public static string[] objectivesRemaining = new string[] {"Najdi <color=blue> kuřecí řízek","Poptej se o <color=blue> matějovi </color> ve vesnici", "Polož na metodějův dům dynamit", "ATGfaAGG", "HUGA CHUGA","AMOGOS AMOGOS", "BUNGA junhga"};
	//Tasked and Completed? No = N, Yes = Y
	//Not tasked = E
	public static string objectivesStatus = "";
	public static int catStatuesFound = 0;
	public static int dynPlaced = 0;
	
	public static int cat1 = 0;
	public static int cat2 = 0;
	public static int cat3 = 0;
	public static int cat4 = 0;
	public static int cat5 = 0;
	
	//INVENTORY
	//ITEMNAME-IMGINDEX-AMOUNT,ITEMNAME-IMGINDEX-AMOUNT...
	public static string inventory = "Gold bars-0-1";

	public MusicControllerScript musicControl;
	
	public InventoryScript inventoryScript;

	public Cars cars;

	void Start()
	{
		UpdateVars();
		
		
		
		
		
		
		if(SceneManager.GetActiveScene().name == "MainScene")
		{
			Debug.Log(objectivesStatus);
			
			var player = GameObject.Find("Player");
			
			Debug.Log("PlayerLevel: " + PlayerScript.playerLevel);
			foreach(var car in new List<GameObject>{cars.carlvl1,cars.carlvl2,cars.carlvl3,cars.carlvl4,cars.carlvl5})
			{
				if(car.name.Split("lvl")[1] != (PlayerScript.playerLevel).ToString())
				{
					car.SetActive(false);
				}else
				{
					car.SetActive(true);
				}
			}
			
			var playerCar = GameObject.Find("PlayerCarlvl"+PlayerScript.playerLevel);
			
			//SET PLAYER POS Vector3(1077.19995,79.4599991,83.5)
			player.transform.position = new Vector3(
				float.Parse(PlayerPrefs.GetString("PlayerPos", "1077.19995,79.4599991,83.5").Split(",")[0]), 
				float.Parse(PlayerPrefs.GetString("PlayerPos", "1077.19995,79.4599991,83.5").Split(",")[1]), 
				float.Parse(PlayerPrefs.GetString("PlayerPos", "1077.19995,79.4599991,83.5").Split(",")[2])
			);
			
			//SET PLAYER ROT Vector3(0,139.199997,0)
			player.transform.eulerAngles = new Vector3(
				float.Parse(PlayerPrefs.GetString("PlayerEuler", "0,139.199997,0").Split(",")[0]),
				float.Parse(PlayerPrefs.GetString("PlayerEuler", "0,139.199997,0").Split(",")[1]),
				float.Parse(PlayerPrefs.GetString("PlayerEuler", "0,139.199997,0").Split(",")[2])
			);
			
			//SET CAR POS Vector3(1073.03271,79.5800018,118.739777)
			playerCar.transform.position = new Vector3(
				float.Parse(PlayerPrefs.GetString("PlayerCarPos", "1073.03271,79.5800018,118.739777").Split(",")[0]),
				float.Parse(PlayerPrefs.GetString("PlayerCarPos", "1073.03271,79.5800018,118.739777").Split(",")[1]),
				float.Parse(PlayerPrefs.GetString("PlayerCarPos", "1073.03271,79.5800018,118.739777").Split(",")[2])
			);
			
			//SET CAR ROT Vector3(0,330.17215,0)
			playerCar.transform.eulerAngles = new Vector3(
				float.Parse(PlayerPrefs.GetString("PlayerCarEuler", "0,330.17215,0").Split(",")[0]),
				float.Parse(PlayerPrefs.GetString("PlayerCarEuler", "0,330.17215,0").Split(",")[1]),
				float.Parse(PlayerPrefs.GetString("PlayerCarEuler", "0,330.17215,0").Split(",")[2])
			);
			
			if(Vector3.Distance(player.transform.position,playerCar.transform.position) < 5)
			{
				player.GetComponent<PlayerScript>().SpawnGetIn(playerCar);
			}
			
			StartCoroutine(CatCheck());
			if(objectivesStatus[2] == 'Y')
			{
				NPCScript.hideArnost = true;
				GameObject.Find("Altar").GetComponent<Animator>().SetBool("Lit",true);
				GameObject.Find("Altar").GetComponent<Animator>().SetBool("Exist",true);
			}
		}
	}
	
	IEnumerator CatCheck()
	{
		yield return new WaitUntil(() => catStatuesFound >= 5);
		var temp = objectivesStatus.ToCharArray();
		temp[1] = 'Y';
		objectivesStatus = new string(temp);
		
	}

	void Update(){
		
	}

	public void UpdateVars(){
		cat1 = PlayerPrefs.GetInt("cat1", 0);
		cat2 = PlayerPrefs.GetInt("cat2", 0);
		cat3 = PlayerPrefs.GetInt("cat3", 0);
		cat4 = PlayerPrefs.GetInt("cat4", 0);
		cat5 = PlayerPrefs.GetInt("cat5", 0);
		
		var objects = FindObjectsOfType<GameObject>();
		var i = 0;
		foreach(var obj in objects)
		{
			if(obj.name.Contains("soska"))
			{
				switch (int.Parse(obj.name.Split("soska")[1]))
				{
					case 1:
						if(cat1 == 1)
						{
							objects[i].SetActive(false);
						}
						break;
					case 2:
						if(cat2 == 1)
						{
							objects[i].SetActive(false);
						}
						break;
					case 3:
						if(cat3 == 1)
						{
							objects[i].SetActive(false);
						}
						break;
					case 4:
						if(cat4 == 1)
						{
							objects[i].SetActive(false);
						}
						break;
					case 5:
						if(cat5 == 1)
						{
							objects[i].SetActive(false);
						}
						break;
				}
			}
			i++;
		}
		
		catStatuesFound = PlayerPrefs.GetInt("catStatuesFound", 0);
		sfxVolScale = PlayerPrefs.GetFloat("sfxVolScale", 0.4f);
		musicVolScale = PlayerPrefs.GetFloat("musicVolScale", 0.2f);
		masterVolScale = PlayerPrefs.GetFloat("masterVolScale", 0.5f);
		ambientVolScale = PlayerPrefs.GetFloat("ambientVolScale", 0.3f);
		sensitivity = PlayerPrefs.GetFloat("sensitivity", 0.4f) * 100;
		objIndex = PlayerPrefs.GetInt("objIndex", 400);
		objectivesStatus = PlayerPrefs.GetString("objectivesStatus", new string('E',objectivesRemaining.Length));
		MatejController.health = PlayerPrefs.GetInt("matejHealth", 10000);
		PlayerScript.health = PlayerPrefs.GetInt("playerHealth", 100);
		PlayerScript.carHealth = PlayerPrefs.GetInt("playerCarHealth", 1000);
		dialogVolScale = PlayerPrefs.GetFloat("dialogVolScale", 0.9f);
		inventory = PlayerPrefs.GetString("inventory", "");
		PlayerScript.playerLevel = PlayerPrefs.GetInt("playerLevel", 1);
		PlayerScript.carFuel = PlayerPrefs.GetFloat("carFuel", 60f);
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