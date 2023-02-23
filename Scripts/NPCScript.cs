using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCScript : MonoBehaviour
{
	
	public TMP_Text subtitlesText;
	
	public MenuControllerScript menuControl;
	
	public AudioSource source;
	
	public AudioClip[] clips;
	
	
	
	public GameControllerScript gameControl;

	//LINES
	//AC = Quest acquire
	//BF = Return before finish
	//F = Finishing

	string[] Pepa_linesAC = new string[] {
		"Čauu, hej kámo jestli mi přineseš můj kuřecí řízek, který jsem si náhodou zapomněl, tak bych ti jako možná i něco dal, hele.", 
		"Díííík."};
	string[] Pepa_linesBF = new string[] {
		"Jaktože jsi jako zpátky bez mýho ŘÍZKU??!!", 
		"Než ho budeš mít, tak se NEVRACEJ!!"};
	string[] Pepa_linesF = new string[] {
		"Kámooo dík moc, fakt jsem nevěřil že ho ještě někdy uvidím. Tady máš moje životní úspory."};
	
	public AudioClip[] Pepa_clipsAC;
	public AudioClip[] Pepa_clipsBF;
	public AudioClip[] Pepa_clipsF;

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
		var currentStatusIndex = 0;
		
		switch (gameObject.name) //S jakym NPC si povidam
		{
			case "Pepa":
				currentStatusIndex = 0; //Status Index
				name = "Pepa"; //Jmeno NPC
				
				if(objectivesStatus[currentStatusIndex] == 'E'){
					lines = Pepa_linesAC; //Vybrat titulky
					clips = Pepa_clipsAC;
					objectivesStatus[currentStatusIndex] = 'N'; //Setne pres Index assignuti objectivu do temp. listu
					GameControllerScript.objectivesStatus = new string(objectivesStatus); //setne objStatus na temp
				}else if(objectivesStatus[currentStatusIndex] == 'N')
				{
					lines = Pepa_linesBF;
					clips = Pepa_clipsBF;
				}else if(objectivesStatus[currentStatusIndex] == 'Y')
				{
					//QUEST COMPLETED
					lines = Pepa_linesF;
					clips = Pepa_clipsF;
				}
				
				break;
		}
		int i = 0;
		foreach (var line in lines)
		{
			subtitlesText.text = name + ": " + line;
			source.PlayOneShot(clips[i],gameControl.dialogVolScale*gameControl.masterVolScale);
			yield return new WaitForSecondsRealtime(clips[i].length);
			yield return new WaitUntil(() => Input.GetKeyDown("space"));
			i++;
		}
		menuControl.Save();
		StopTalking();
	}
}