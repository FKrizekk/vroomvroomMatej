using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCScript : MonoBehaviour
{
	
	public TMP_Text subtitlesText;
	
	public MenuControllerScript menuControl;

	string[] Beard_man_lines = new string[] {"Čauu, hej kámo jestli mi přineseš můj kuřecí řízek, který jsem si zapomněl u skály, tak bych ti jako něco dal... možná.", "Díííík."};

	public void Talk(){
		PlayerMovement.canMove = false;
		StartCoroutine(Conversation());
	}

	public void StopTalking(){
		PlayerMovement.canMove = true;
		subtitlesText.text = "";
	}

	IEnumerator Conversation(){
		var lines = new string[] {"hello","hello"};
		var name = "Unknown";
		var objectivesStatus = GameControllerScript.objectivesStatus.ToCharArray();
		switch (gameObject.name)
		{
			case "Beard_man":
				lines = Beard_man_lines; //Vybrat titulky
				name = "Pepa"; //Jmeno NPC
				objectivesStatus[0] = 'N'; //Setne pres Index assignuti objectivu do temp. listu
				GameControllerScript.objectivesStatus = new string(objectivesStatus); //setne objStatus na temp
				break;
		}
		foreach (var line in lines)
		{
			subtitlesText.text = name + ": " + line;
			yield return new WaitForSeconds(2);
			yield return new WaitUntil(() => Input.GetKeyDown("space"));
		}
		menuControl.Save();
		StopTalking();
	}
}