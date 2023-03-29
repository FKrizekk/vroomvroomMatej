using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControllerScript : MonoBehaviour
{
    public AudioClip[] musicClips;
    public GameControllerScript gameControl;
    public AudioSource musicSource;

    bool inAction = false;
    
    int cClip;

    void Start(){
        cClip = 0;
    }

    //pitch
    //percent change -1

    //echo
    //0.015
    //0.8
    public void ResetVolume(){
        musicSource.Stop();
        musicSource.volume = gameControl.masterVolScale;
        StopCoroutine(Jukebox());
        StartCoroutine(Jukebox());
    }

    public void StopPlaying(){
        musicSource.Stop();
        StopCoroutine(Jukebox());
    }

    public void StartAction(){
        StopPlaying();
        inAction = true;
        StartCoroutine(Jukebox());
    }

    public void EndAction(){
        StopPlaying();
        inAction = false;
        StartCoroutine(Jukebox());
    }

    IEnumerator Jukebox(){
        yield return new WaitForSeconds(0.5f);
        
        if(!inAction){
            musicSource.PlayOneShot(musicClips[cClip], gameControl.ambientVolScale);
            //Debug.Log("Played clip: " + musicClips[cClip].name + " with scale: " + (float)gameControl.musicVolScale);
            yield return new WaitForSeconds(musicClips[cClip].length);
            cClip++;
            if(cClip == musicClips.Length-1){
                cClip = 0;
            }
        }else{
            yield return new WaitForSeconds(0.5f);
            musicSource.PlayOneShot(musicClips[musicClips.Length-1], gameControl.musicVolScale);
            Debug.Log("Playing: " + musicClips[musicClips.Length-1]);
            Debug.Log("Length" + musicClips[musicClips.Length-1].length);
            yield return new WaitForSecondsRealtime(musicClips[musicClips.Length-1].length);
        }



        StartCoroutine(Jukebox());
    }
}
