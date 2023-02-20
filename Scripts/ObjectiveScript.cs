using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveScript : MonoBehaviour
{
    public string objective = null;
    
    void Start(){

    }

    void Update(){
        if(GameControllerScript.objIndex != 400){
            objective = GameControllerScript.objectivesRemaining[GameControllerScript.objIndex];
        }else{
            objective = null;
        }
    }
}
