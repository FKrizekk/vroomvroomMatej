using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveScript : MonoBehaviour
{
	public string objective = null;
	
	public TMP_Text cyrilQuestText;
	
	public GameObject cyrilProgressPanel;
	
	void Start(){
	}
	

	void Update(){
		if(GameControllerScript.objIndex != 400){
			objective = GameControllerScript.objectivesRemaining[GameControllerScript.objIndex];
		}else{
			objective = null;
		}
		var objectivesStatus = GameControllerScript.objectivesStatus.ToCharArray();
		if(objectivesStatus[2] == 'N')
		{
			cyrilProgressPanel.GetComponent<Animator>().SetBool("Shown", true);
			cyrilQuestText.text = "Dynamites placed: " + GameControllerScript.dynPlaced + "/4";
		}else{
			cyrilProgressPanel.GetComponent<Animator>().SetBool("Shown", false);
		}
	}
}
