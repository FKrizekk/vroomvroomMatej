using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneScript : MonoBehaviour
{

    public AudioControllerScript audioC;
    public AudioControllerScript matejAudio;
    public carEngineScript engine;
    public Animator fadePanel;
    public GameControllerScript gameController;

    void Start(){
        Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    public void playMATEJ(){
        matejAudio.PlaySound(0, gameController.sfxVolScale);
    }

    public void EnginePitch(float pitch){
        engine.setPitch(pitch);
    }

    public void StartMenu(){
        StartCoroutine(LoadMenu());
    }

    IEnumerator LoadMenu(){
        fadePanel.SetBool("LoadStart",true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
    }
}