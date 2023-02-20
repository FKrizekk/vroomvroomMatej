using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerScript : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource source;

    AudioClip chosenClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(int index, float volumeScale){
        chosenClip = clips[index];
        source.PlayOneShot(chosenClip, volumeScale);
    }
}
