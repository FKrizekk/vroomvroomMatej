using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Pepa {
	public string[] linesAC = new string[] {
		"Čauu, hej kámo jestli mi přineseš můj kuřecí řízek, který jsem si náhodou zapomněl, tak bych ti jako možná i něco dal, hele.", 
		"Díííík."};
	public string[] linesBF = new string[] {
		"Jaktože jsi jako zpátky bez mýho ŘÍZKU??!!", 
		"Než ho budeš mít, tak se NEVRACEJ!!"};
	public string[] linesF = new string[] {
		"Kámooo dík moc, fakt jsem nevěřil že ho ještě někdy uvidím. Tady máš moje životní úspory."};
	public string[] linesC = new string[] {
		"Děkuji ti ale už nic nepotřebuju."};
	
	public AudioClip[] clipsAC;
	public AudioClip[] clipsBF;
	public AudioClip[] clipsF;
	public AudioClip[] clipsC;
}

[System.Serializable]
public class Karel 
{
	public Animator garageMenuAnim;
	
	public string[] linesAC = new string[] {
		"Čauu, hej kámo jestli mi přineseš můj kuřecí řízek, který jsem si náhodou zapomněl, tak bych ti jako možná i něco dal, hele.", 
		"Díííík."};
	public string[] linesBF = new string[] {
		"Jaktože jsi jako zpátky bez mýho ŘÍZKU??!!", 
		"Než ho budeš mít, tak se NEVRACEJ!!"};
	public string[] linesF = new string[] {
		"Kámooo dík moc, fakt jsem nevěřil že ho ještě někdy uvidím. Tady máš moje životní úspory."};
	public string[] linesC = new string[] {
		"Děkuji ti ale už nic nepotřebuju."};
	
	public AudioClip[] clipsAC;
	public AudioClip[] clipsBF;
	public AudioClip[] clipsF;
	public AudioClip[] clipsC;
	
	public AudioClip[] clipsArrive;
	public AudioClip[] clipsBuyUpgrade;
	public AudioClip[] clipsLeave;
	public AudioClip[] clipsLeaveNoBuy;
	
	public GarageSFX garageSFX;
	
	[System.Serializable]
	public class GarageSFX
	{
		public AudioSource garageSFXSource;
		
		public GameObject carlvl1;
		public GameObject carlvl2;
		public GameObject carlvl3;
		public GameObject carlvl4;
		public GameObject carlvl5;
		
		public AudioClip Repair;
		public AudioClip Upgrade;
		public AudioClip Refuel;
	}
}

public class NPCScript : MonoBehaviour
{
	
	public TMP_Text subtitlesText;
	
	public MenuControllerScript menuControl;
	
	public AudioSource source;
	
	AudioClip[] clips;
	
	public InventoryScript invScript;
	
	
	int karelChosen = 0;
	
	
	public GameControllerScript gameControl;
	
	public MatejController matejControl;
	
	GameObject car;
	
	bool didSomething = false;
	

	//LINES
	//AC = Quest acquire
	//BF = Return before finish
	//F = Finishing
	//C = DONE
	
	public Pepa Pepa;
	public Karel Karel;
	
	
	
	public void ChooseKarel(int x)
	{
		karelChosen = x;
	}
	
	
	void Start()
	{
		subtitlesText = GameObject.Find("/Canvas/TalkPanel/NPCSubtitles/").GetComponent<TMP_Text>();
		menuControl = GameObject.Find("MenuController").GetComponent<MenuControllerScript>();
		source = GetComponent<AudioSource>();
		invScript = GameObject.Find("/Canvas/InventoryPanel/").GetComponent<InventoryScript>();
		gameControl = GameObject.Find("GameController").GetComponent<GameControllerScript>();
		matejControl = GameObject.Find("MatejPARENT").GetComponent<MatejController>();
	}

	public void Talk(){
		PlayerMovement.canMove = false;
		if(gameObject.name != "Karel")
		{
			StartCoroutine(Conversation());
		}else
		{
			StartCoroutine(GarageTalk());
			didSomething = false;
		}
	}
	
	IEnumerator GarageTalk()
	{
		//FIND CAR
		var objects = FindObjectsOfType<GameObject>();
		for (int i = 0; i < objects.Length; i++)
		{
			if(objects[i].name.Contains("PlayerCar"))
			{
				car = objects[i];
			}
			
		}
		
		//LOCK PLAYER MOVEMENT
		PlayerMovement.canMove = false;
		
		//OPEN GARAGE MENU
		Karel.garageMenuAnim.SetBool("Show", true);
		
		//SHOW MOUSE
		Cursor.visible = true;
		UnityEngine.Cursor.lockState = CursorLockMode.None;
		
		//Set SFX source position to car's position
		Karel.garageSFX.garageSFXSource.gameObject.transform.position = car.transform.position;
		
		//0 - nic
		//1 - Repair
		//2 - Upgrade
		//3 - Refuel
		//4 - Leave
		yield return new WaitUntil(() => karelChosen != 0);
		//CHECK IF PLAYER HAS ENOUGH MONEY IS DONE IN BUTTON FUNCTION
		switch(karelChosen)
		{
			case 0:
				break;
			case 1:
				didSomething = true;
				//REPAIR//
				
				if(invScript.PlayerHas("Gold bars", 10) && PlayerScript.carHealth < 1000){
					//PLAY REPAIR SOUND
					
					source.PlayOneShot(Karel.clipsBuyUpgrade[Random.Range(0,4)], gameControl.masterVolScale*gameControl.dialogVolScale);
					
					
					PlayerScript.carHealth += 100;
					invScript.RemoveItem("Gold bars", 10);
				}else
				{
					//PLAY ERROR SOUND
					
				}
				
				
				break;
			case 2:
				didSomething = true;
				//UPGRADE//
				if(invScript.PlayerHas("Gold bars", 25) && PlayerScript.playerLevel < 5){
					if(PlayerScript.playerLevel != 5)
					{
						List<GameObject> list = new List<GameObject>{Karel.garageSFX.carlvl1,Karel.garageSFX.carlvl2,Karel.garageSFX.carlvl3,Karel.garageSFX.carlvl4,Karel.garageSFX.carlvl5};
						foreach(var car in list)
						{
							car.transform.position = list[PlayerScript.playerLevel-1].transform.position;
							car.transform.rotation = list[PlayerScript.playerLevel-1].transform.rotation;
							if(car.name.Split("lvl")[1] != (PlayerScript.playerLevel+1).ToString())
							{
								car.SetActive(false);
							}else
							{
								car.SetActive(true);
							}
						}
						PlayerScript.playerLevel++;
						matejControl.RefreshVars();
					}
					
					
					//PLAY UPGRADE SOUND
					
					source.PlayOneShot(Karel.clipsBuyUpgrade[Random.Range(0,4)], gameControl.masterVolScale*gameControl.dialogVolScale);
					
					invScript.RemoveItem("Gold bars", 25);
				}else
				{
					//PLAY ERROR SOUND
					
				}
				
				
				break;
			case 3:
				didSomething = true;
				//Refuel//
				
				
				if(invScript.PlayerHas("Gold bars", 5)  && PlayerScript.carFuel < 60)
				{
					//PLAY REFUEL SOUND
					
					source.PlayOneShot(Karel.clipsBuyUpgrade[Random.Range(0,4)], gameControl.masterVolScale*gameControl.dialogVolScale);
					
					PlayerScript.carFuel += 1;
					Debug.Log(PlayerScript.carFuel);
					invScript.RemoveItem("Gold bars", 5);
				}else
				{
					//PLAY ERROR SOUND
					
				}
				
				break;
			case 4:
				//LEAVE//
				
				if(!didSomething)
				{
					source.PlayOneShot(Karel.clipsLeaveNoBuy[Random.Range(0,2)], gameControl.masterVolScale*gameControl.dialogVolScale);
				}else{
					source.PlayOneShot(Karel.clipsLeave[Random.Range(0,3)], gameControl.masterVolScale*gameControl.dialogVolScale);
				}
				
				//HIDE GARAGE MENU
				Karel.garageMenuAnim.SetBool("Show", false);
				
				//HIDE MOUSE
				Cursor.visible = false;
				UnityEngine.Cursor.lockState = CursorLockMode.Locked;
				
				//PLAY LEAVE VOICELINE
				PlayerMovement.canMove = true;
				
				menuControl.Save();
				break;
				
		}
		if(karelChosen != 4)
		{
			karelChosen = 0;
			StartCoroutine(GarageTalk());
		}else
		{
			karelChosen = 0;
			
		}
		
	}
	
	public void PlayerArrived()
	{
		source.PlayOneShot(Karel.clipsArrive[Random.Range(0,4)], gameControl.masterVolScale*gameControl.dialogVolScale);
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
					lines = Pepa.linesAC; //Vybrat titulky
					clips = Pepa.clipsAC;
					objectivesStatus[currentStatusIndex] = 'N'; //Setne pres Index assignuti objectivu do temp. listu
					GameControllerScript.objectivesStatus = new string(objectivesStatus); //setne objStatus na temp
				}else if(objectivesStatus[currentStatusIndex] == 'N')
				{
					lines = Pepa.linesBF;
					clips = Pepa.clipsBF;
				}else if(objectivesStatus[currentStatusIndex] == 'Y')
				{
					//QUEST COMPLETED
					lines = Pepa.linesF;
					clips = Pepa.clipsF;
					invScript.AddItem("Gold bars",0,10);
					objectivesStatus[currentStatusIndex] = 'C'; //Setne pres Index assignuti objectivu do temp. listu
					GameControllerScript.objectivesStatus = new string(objectivesStatus);
				}else if(objectivesStatus[currentStatusIndex] == 'C')
				{
					lines = Pepa.linesC;
					clips = Pepa.clipsC;
				}
				
				break;
				
			case "Karel":
				currentStatusIndex = 1; //Status Index
				name = "Karel"; //Jmeno NPC
				
				if(objectivesStatus[currentStatusIndex] == 'E'){
					lines = Karel.linesAC; //Vybrat titulky
					clips = Karel.clipsAC;
					objectivesStatus[currentStatusIndex] = 'N'; //Setne pres Index assignuti objectivu do temp. listu
					GameControllerScript.objectivesStatus = new string(objectivesStatus); //setne objStatus na temp
				}else if(objectivesStatus[currentStatusIndex] == 'N')
				{
					lines = Karel.linesBF;
					clips = Karel.clipsBF;
				}else if(objectivesStatus[currentStatusIndex] == 'Y')
				{
					//QUEST COMPLETED
					lines = Karel.linesF;
					clips = Karel.clipsF;
					invScript.AddItem("Gold bars",0,10);
					objectivesStatus[currentStatusIndex] = 'C'; //Setne pres Index assignuti objectivu do temp. listu
					GameControllerScript.objectivesStatus = new string(objectivesStatus);
				}else if(objectivesStatus[currentStatusIndex] == 'C')
				{
					lines = Karel.linesC;
					clips = Karel.clipsC;
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