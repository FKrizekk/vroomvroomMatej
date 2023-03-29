using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControllerScript : MonoBehaviour
{
	public Animator fadePanel;

	public AudioControllerScript audio;

	public GameControllerScript gameController;

	public Animator canvasAnim;
	
	public GameObject SavingParent;
	
	public InventoryScript inventoryScript;

	//Sliders
	public Slider masterSlider;
	public Slider sfxSlider;
	public Slider musicSlider;
	public Slider sensSlider;
	public Slider dialogSlider;
	public Slider ambientSlider;
	
	public Slider antiAliasingSlider;
	
	//Toggles
	public Toggle vsyncToggle;
	public Toggle anisotropicFilteringToggle;

	public bool menuOpened = false;
	
	bool cursorWasVisible = false;

	void Update(){
		if(SceneManager.GetActiveScene().name == "MainScene"){
			if(Input.GetKeyDown("escape")){
				if(menuOpened){
					CloseMenu();
				}else{
					OpenMenu();
				}
			}
		}
		
		if(Input.GetKeyDown("t")){
			Save();
		}
		
	}

	void Start(){
		if(SceneManager.GetActiveScene().name == "MainMenu"){
			Cursor.visible = true;
			UnityEngine.Cursor.lockState = CursorLockMode.None;
		}else if(SceneManager.GetActiveScene().name == "MainScene"){
			Cursor.visible = false;
			UnityEngine.Cursor.lockState = CursorLockMode.Locked;
		}else if(SceneManager.GetActiveScene().name == "IntroScene"){
			Cursor.visible = false;
			UnityEngine.Cursor.lockState = CursorLockMode.Locked;
		}
		
		StartCoroutine(Saver());
	}
	
	IEnumerator Saver()
	{
		yield return new WaitForSeconds(4*60);
		Save();
		
		
		StartCoroutine(Saver());
	}

	public void StartMainMenu(){
		StartCoroutine(LoadMainMenu());
	}

	IEnumerator LoadMainMenu(){
		fadePanel.SetBool("LoadStart",true);
		yield return new WaitForSecondsRealtime(1.5f);
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainMenu");
		
	}

	public void OpenMenu(){
		//Debug.Log(UnityEngine.Cursor.lockState == CursorLockMode.Locked);
		if(UnityEngine.Cursor.lockState == CursorLockMode.Locked)
		{
			cursorWasVisible = false;
		}else
		{
			cursorWasVisible = true;
		}
		menuOpened = true;
		PlayPressSound();
		canvasAnim.SetBool("OpenMenu", true);
		Cursor.visible = true;
		UnityEngine.Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 0f;
	}

	public void CloseMenu(){
		menuOpened = false;
		Time.timeScale = 1f;
		PlayPressSound();
		canvasAnim.SetBool("OpenMenu", false);
		canvasAnim.SetBool("OpenSettings", false);
		if(!cursorWasVisible)
		{
			Cursor.visible = false;
			UnityEngine.Cursor.lockState = CursorLockMode.Locked;
		}
	}

	public void Quit(){
		PlayPressSound();
		StartMainMenu();
	}

	public void PlayHighlightSound(){
		audio.PlaySound(0, gameController.sfxVolScale);
	}

	public void PlayPressSound(){
		audio.PlaySound(1, gameController.sfxVolScale);
	}

	public void StartIntro(){
		StartCoroutine(LoadIntro());
		PlayPressSound();
	}

	IEnumerator LoadIntro(){
		fadePanel.SetBool("LoadStart",true);
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene("IntroScene");
	}

	public void StartPlay(){
		StartCoroutine(LoadPlay());
		PlayPressSound();
	}

	IEnumerator LoadPlay(){
		fadePanel.SetBool("LoadStart",true);
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene("MainScene");
	}

	public void OpenSettings(){
		PlayPressSound();
		canvasAnim.SetBool("OpenSettings", true);
		sfxSlider.GetComponent<Slider>().value = gameController.sfxVolScale;
		musicSlider.GetComponent<Slider>().value = gameController.musicVolScale;
		masterSlider.GetComponent<Slider>().value = gameController.masterVolScale;
		dialogSlider.GetComponent<Slider>().value = gameController.dialogVolScale;
		ambientSlider.GetComponent<Slider>().value = gameController.ambientVolScale;
		sensSlider.GetComponent<Slider>().value = gameController.sensitivity / 100;
		vsyncToggle.isOn = gameController.vsync;
		antiAliasingSlider.GetComponent<Slider>().value = gameController.antiAliasing;
		anisotropicFilteringToggle.isOn = gameController.anisotropicFiltering;
	}

	public void CloseSettings(){
		PlayPressSound();
		canvasAnim.SetBool("OpenSettings", false);
	}

	public void ApplySettings(){
		PlayPressSound();
		Save();
		gameController.UpdateVars();
	}
	
	public void QuitGame()
	{
		Save();
		Invoke("Close", 1);
	}
	
	public void RESETALLPROGRESS()
	{
		PlayerPrefs.SetInt("cat1", 0);
		PlayerPrefs.SetInt("cat2", 0);
		PlayerPrefs.SetInt("cat3", 0);
		PlayerPrefs.SetInt("cat4", 0);
		PlayerPrefs.SetInt("cat5", 0);
		
		PlayerPrefs.SetInt("catStatuesFound", 0);
		PlayerPrefs.SetString("objectivesStatus", "");
		PlayerPrefs.SetInt("matejHealth", 10000);
		PlayerPrefs.SetInt("playerHealth", 100);
		PlayerPrefs.SetInt("playerCarHealth", 1000);
		PlayerPrefs.SetInt("playerLevel", 1);
		PlayerPrefs.SetFloat("carFuel", 60);
		
		inventoryScript.EraseInventory();
		
		//PLAYERPOSITION//Vector3(1076.27002,79.0299988,82.1399994)
		PlayerPrefs.SetString("PlayerPos",
			(1076.27002).ToString()+","+
			(79.0299988).ToString()+","+
			(82.1399994).ToString());
			
		PlayerPrefs.SetString("PlayerEuler",
			(0).ToString()+","+
			(0).ToString()+","+
			(0).ToString());
			
			
			//Vector3(1073.37,78.6900024,81.8799973)
		PlayerPrefs.SetString("PlayerCarPos",
			(1073.37).ToString()+","+
			(78.6900024).ToString()+","+
			(81.8799973).ToString());
			
		PlayerPrefs.SetString("PlayerCarEuler",
			(0).ToString()+","+
			(0).ToString()+","+
			(0).ToString());
		//PLAYERPOSITION//
	}
	
	void Close()
	{
		Application.Quit();
	}
	
	public void Save()
	{
		var player = GameObject.Find("Player");
		var playerCar = player;
		
		var objects = FindObjectsOfType<GameObject>();
		var i = 0;
		foreach(var obj in objects)
		{
			if(obj.name.Contains("PlayerCar"))
			{
				playerCar = objects[i];
			}
			i++;
		}
		
		if(SceneManager.GetActiveScene().name == "MainScene")
		{
			SavingParent.SetActive(true);
		}
		PlayerPrefs.SetInt("catStatuesFound", GameControllerScript.catStatuesFound);
		PlayerPrefs.SetFloat("sfxVolScale", sfxSlider.GetComponent<Slider>().value);
		PlayerPrefs.SetFloat("musicVolScale", musicSlider.GetComponent<Slider>().value);
		PlayerPrefs.SetFloat("masterVolScale", masterSlider.GetComponent<Slider>().value);
		PlayerPrefs.SetFloat("sensitivity", sensSlider.GetComponent<Slider>().value);
		PlayerPrefs.SetFloat("dialogVolScale", dialogSlider.GetComponent<Slider>().value);
		PlayerPrefs.SetFloat("ambientVolScale", ambientSlider.GetComponent<Slider>().value);
		PlayerPrefs.SetString("objectivesStatus", GameControllerScript.objectivesStatus);
		PlayerPrefs.SetInt("matejHealth", MatejController.health);
		PlayerPrefs.SetInt("playerHealth", PlayerScript.health);
		PlayerPrefs.SetInt("playerCarHealth", PlayerScript.carHealth);
		PlayerPrefs.SetInt("playerLevel", PlayerScript.playerLevel);
		PlayerPrefs.SetFloat("carFuel", PlayerScript.carFuel);
		
		//PLAYERPOSITION//
		PlayerPrefs.SetString("PlayerPos",
			player.transform.position.x.ToString()+","+
			player.transform.position.y.ToString()+","+
			player.transform.position.z.ToString());
			
		PlayerPrefs.SetString("PlayerEuler",
			player.transform.eulerAngles.x.ToString()+","+
			player.transform.eulerAngles.y.ToString()+","+
			player.transform.eulerAngles.z.ToString());
			
			
			
		PlayerPrefs.SetString("PlayerCarPos",
			playerCar.transform.position.x.ToString()+","+
			playerCar.transform.position.y.ToString()+","+
			playerCar.transform.position.z.ToString());
			
		PlayerPrefs.SetString("PlayerCarEuler",
			playerCar.transform.eulerAngles.x.ToString()+","+
			playerCar.transform.eulerAngles.y.ToString()+","+
			playerCar.transform.eulerAngles.z.ToString());
		//PLAYERPOSITION//
		
		if(SceneManager.GetActiveScene().name == "MainScene")
		{
			inventoryScript.SaveInventory();
		}
		if(vsyncToggle.isOn)
		{
			PlayerPrefs.SetInt("vsync", 1);
		}else
		{
			PlayerPrefs.SetInt("vsync", 0);
		}
		
		if(antiAliasingSlider.GetComponent<Slider>().value != 3)
		{
			PlayerPrefs.SetInt("antiAliasing", (int)antiAliasingSlider.GetComponent<Slider>().value*2);
		}else
		{
			PlayerPrefs.SetInt("antiAliasing", 8);
		}
		if(anisotropicFilteringToggle.isOn)
		{
			PlayerPrefs.SetInt("anisotropicFiltering", 1);
		}else
		{
			PlayerPrefs.SetInt("anisotropicFiltering", 0);
		}
	}
}