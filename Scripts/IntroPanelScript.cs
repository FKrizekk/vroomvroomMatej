using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPanelScript : MonoBehaviour
{
    public Animator anim;
    public Animator anim2;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")){
            anim.SetBool("HidePanel", true);
            anim2.SetBool("Start", true);
        }
    }
}