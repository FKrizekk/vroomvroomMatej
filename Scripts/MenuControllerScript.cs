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

	//Sliders
	public Slider masterSlider;
	public Slider sfxSlider;
	public Slider musicSlider;
	public Slider sensSlider;

	public bool menuOpened = false;

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
		Cursor.visible = false;
		UnityEngine.Cursor.lockState = CursorLockMode.Locked;
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
		sensSlider.GetComponent<Slider>().value = gameController.sensitivity / 100;
	}

	public void CloseSettings(){
		PlayPressSound();
		canvasAnim.SetBool("OpenSettings", false);
	}

	public void ApplySettings(){
		PlayPressSound();
		PlayerPrefs.SetFloat("sfxVolScale", sfxSlider.GetComponent<Slider>().value);
		PlayerPrefs.SetFloat("musicVolScale", musicSlider.GetComponent<Slider>().value);
		PlayerPrefs.SetFloat("masterVolScale", masterSlider.GetComponent<Slider>().value);
		PlayerPrefs.SetFloat("sensitivity", sensSlider.GetComponent<Slider>().value);
		gameController.UpdateVars();
	}
	
	public void Save()
	{
		PlayerPrefs.SetFloat("sfxVolScale", sfxSlider.GetComponent<Slider>().value);
		PlayerPrefs.SetFloat("musicVolScale", musicSlider.GetComponent<Slider>().value);
		PlayerPrefs.SetFloat("masterVolScale", masterSlider.GetComponent<Slider>().value);
		PlayerPrefs.SetFloat("sensitivity", sensSlider.GetComponent<Slider>().value);
		PlayerPrefs.SetString("objectivesStatus", GameControllerScript.objectivesStatus);
	}
}