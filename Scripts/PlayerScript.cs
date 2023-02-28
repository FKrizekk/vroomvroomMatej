using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
	//---------DAMAGE VALUES------------
	
	//AXE MATEJ->PLAYER
	int AxeCarDamage = 100;
	int AxePlayerDamage = 50;
	
	//ROCKET MATEJ->PLAYER
	int RocketCarDamage = 150;
	int RocketPlayerDamage = 50;
	
	//GUN PLAYER->MATEJ
	int LVL1GunDamage = 10;
	int LVL2GunDamage = 20;
	int LVL3GunDamage = 30;
	int LVL4GunDamage = 40;
	
	
	//---------DAMAGE VALUES------------
	
	
	
	
	public static int health = 100;
	
	public static int carHealth = 1000;
	

	public static GameObject car;
	public GameObject PlayerCamera;
	public TMP_Text interactText;
	
	public static GameObject screen;
	public static GameObject gunScreen;
	public static GameObject screenCanvas;
	public static GameObject gunScreenCanvas;
	
	public InventoryScript invScript;

	public AudioControllerScript cameraAudio;

	int layerMask = 1 << 3;
	
	bool canGetIn = false;
	bool canTalk = false;
	bool canPickUp = false;
	public bool isInCar = false;
	public bool isGunning = false;
	
	string lookingAt = "";


	public static GunCameraScript gunCameraScript;
	public CameraScript cameraScript;
	
	public Animator mapPanelAnim;
	
	public static carEngineScript carEngine;
	
	public Animator invAnim;
	
	bool InventoryOpen = false;
	
	bool notYetHighlighted = true;
	
	bool canWPNON = true;
	bool canWPNOFF = true;
	
	RaycastHit lastHit;
	// Start is called before the first frame update
	void Start()
	{
		layerMask = ~layerMask;
		DisableWeapons();
	}
	
	public static void RefreshVars(string CAR)
	{
		car = GameObject.Find(CAR);
		
		screen = GameObject.Find(CAR+"/Screen");
		gunScreen = GameObject.Find(CAR+"/GunScreen");
		screenCanvas = GameObject.Find(CAR+"/Screen/ScreenCanvas");
		gunScreenCanvas = GameObject.Find(CAR+"/GunScreen/ScreenCanvas");
		
		string lvl = CAR.Split("lvl")[1];
		if(lvl == "4")
		{
			gunCameraScript = GameObject.Find("/"+CAR+"/WEAPONS PARENT/Canon_lvl_"+lvl+"/canon_lvl_"+lvl+"_var_1/GunCamera").GetComponent<GunCameraScript>();
		}else if(lvl == "5")
		{
			gunCameraScript = GameObject.Find("/"+CAR+"/WEAPONS PARENT/Canon_lvl_"+lvl+"/canon_lvl_"+"4"+"_var_2/GunCamera").GetComponent<GunCameraScript>();
		}else
		{
			gunCameraScript = GameObject.Find("/"+CAR+"/WEAPONS PARENT/Canon_lvl_"+lvl+"/canon_lvl_"+lvl+"/GunCamera").GetComponent<GunCameraScript>();
		}
		carEngine = GameObject.Find(CAR).GetComponent<carEngineScript>();
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown("r"))
		{
			invScript.AddItem("Gold bars", 0, 1);
		}
		
		if(Input.GetKeyDown("y"))
		{
			invScript.RemoveItem("Gold bars", 1);
		}
		
		if(Input.GetKeyDown("v"))
		{
			invScript.AddItem("Kuřecí řízek", 1, 1);
		}
		
		if(Input.GetKeyDown("b"))
		{
			invScript.RemoveItem("Kuřecí řízek", 1);
		}
		
		if(Input.GetKeyDown("i"))
		{
			if(InventoryOpen)
			{
				InventoryOpen = false;
				invAnim.SetBool("ShowInventory", false);
			}else
			{
				InventoryOpen = true;
				invAnim.SetBool("ShowInventory", true);
			}
		}
		
		
		
		
		
		
		if(isGunning)
		{
			gunCameraScript.active = true;
			cameraScript.active = false;
		}else
		{
			gunCameraScript.active = false;
			cameraScript.active = true;
		}
		
		
		if(!isInCar){
			RaycastHit hit;
			
			if (Physics.Raycast(PlayerCamera.transform.position, (PlayerCamera.transform.forward + PlayerCamera.transform.position) - PlayerCamera.transform.position, out hit, 8,layerMask))
			{
				
				//Debug.Log(hit.collider.gameObject.name);
				if(hit.collider.gameObject.name == "Ferrari Testarossa"){
					canGetIn = true;
					canTalk = false;
					canPickUp = false;
				}else if(hit.collider.gameObject.tag == "NPC"){
					canTalk = true;
					lookingAt = hit.collider.gameObject.name;
					canGetIn = false;
					canPickUp = false;
				}else if(hit.collider.gameObject.name.Contains("PCKPBL"))
				{
					canPickUp = true;
					canGetIn = false;
					canTalk = false;
				}else{
					canGetIn = false;
					canTalk = false;
					canPickUp = false;
					interactText.text = "";
				}
			}else{
				canGetIn = false;
				canTalk = false;
				canPickUp = false;
				interactText.text = "";
			}
			
			
			

			if(canTalk && PlayerMovement.canMove){
				interactText.text = "Press 'E' to interact with " + lookingAt;
				if(Input.GetKeyDown("e")){
					hit.collider.gameObject.GetComponent<NPCScript>().Talk();
				}
			}
			
			if(canPickUp)
			{
				
				if(notYetHighlighted)
				{
					RaycastHit lastHit;
					
					notYetHighlighted = false;
					hit.collider.gameObject.GetComponent<Highlight>().ToggleHighlight(true);
					//Debug.Log("Highlight");
				}
				lastHit = hit;
				interactText.text = "Press 'E' to pickup " + hit.collider.gameObject.tag;
				if(Input.GetKeyDown("e"))
				{
					
					
					switch (hit.collider.gameObject.tag)
					{
						case "Gold bars":
							invScript.AddItem(hit.collider.gameObject.tag, 0, 1);
							break;
						case "Pepa quest item":
							hit.collider.gameObject.GetComponent<QuestItemScript>().FinishQuest(0);
							break;
					}
					Destroy(hit.collider.gameObject);
				}
			}else
			{
				if(!notYetHighlighted)
				{
					notYetHighlighted = true;
					lastHit.collider.gameObject.GetComponent<Highlight>().ToggleHighlight(false);
					//Debug.Log("Unhighlight");
				}
			}
			
			
			
		}else{
			//INCAR
			canTalk = false;
			interactText.text = "";
			if(Input.GetKeyDown("e")){
				car.GetComponent<SimpleCarController>().engine.setPitch(0.5f);
				GetOut();
				isGunning = false;
				screen.GetComponent<MeshRenderer>().enabled = true;
				screenCanvas.SetActive(true);
				gunScreen.GetComponent<MeshRenderer>().enabled = false;
				gunScreenCanvas.SetActive(false);
			}
			
			//Weapon systems toggle
			if(Input.GetKeyDown("x"))
			{
				if(!isGunning)
				{
					EnableWeapons();
				}else
				{
					DisableWeapons();
				}
			}

		}

		if(canGetIn && !canTalk){
			interactText.text = "Press 'E' to get in";
			if(Input.GetKeyDown("e")){
				GetIn();
				interactText.text = "";
			}
		}
		
		if(Input.GetKeyDown("m"))
		{
			if(CameraScript.isInMap)
			{
				HideMap();
			}else
			{
				ShowMap();
			}
		}
	}
	
	void EnableWeapons()
	{
		isGunning = true;
		screen.GetComponent<MeshRenderer>().enabled = false;
		screenCanvas.SetActive(false);
		gunScreen.GetComponent<MeshRenderer>().enabled = true;
		gunScreenCanvas.SetActive(true);
		if(canWPNON)
		{
			carEngine.weaponsSystemsOn();
			canWPNON = false;
			StartCoroutine(wpnsOnTimer());
		}
	}
	
	IEnumerator wpnsOnTimer()
	{
		yield return new WaitForSeconds(carEngine.clips[4].length);
		canWPNON = true;
	}
	
	void DisableWeapons()
	{
		isGunning = false;
		screen.GetComponent<MeshRenderer>().enabled = true;
		screenCanvas.SetActive(true);
		gunScreen.GetComponent<MeshRenderer>().enabled = false;
		gunScreenCanvas.SetActive(false);
		if(canWPNON)
		{
			carEngine.weaponsSystemsOff();
			canWPNON = false;
			StartCoroutine(wpnsOffTimer());
		}
	}
	
	IEnumerator wpnsOffTimer()
	{
		yield return new WaitForSeconds(carEngine.clips[5].length);
		canWPNON = true;
	}
	
	void ShowMap()
	{
		//Move camera to: Vector3(600f,1133.90002f,600f)
		CameraScript.isInMap = true;
		Cursor.visible = true;
		UnityEngine.Cursor.lockState = CursorLockMode.None;
		mapPanelAnim.SetBool("ShowMap", true);
		cameraScript.gameObject.transform.eulerAngles = new Vector3(90f,0f,0f);
		StartCoroutine(ShowMapp());
	}
	
	IEnumerator ShowMapp()
	{
		yield return new WaitForSeconds(1f);
		//Time.timeScale = 0f;
	}
	
	void HideMap()
	{
		//Time.timeScale = 1f;
		CameraScript.isInMap = false;
		Cursor.visible = false;
		UnityEngine.Cursor.lockState = CursorLockMode.Locked;
		mapPanelAnim.SetBool("ShowMap", false);
	}

	void GetIn(){
		isInCar = true;
		canGetIn = false;
		//Debug.Log("is in car");
		GetComponent<MeshRenderer>().enabled = false;
		GetComponent<CapsuleCollider>().isTrigger = true;
		GetComponent<CharacterController>().enabled = !GetComponent<CharacterController>().enabled;
		car.GetComponent<SimpleCarController>().enabled = true;
		transform.SetParent(car.transform,false);
		PlayerCamera.transform.SetParent(null,true);
		transform.localPosition = new Vector3(-0.330000013f,0.200000003f,0.0199999996f);
		transform.rotation = Quaternion.Euler(car.transform.eulerAngles);
		car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
	}

	void GetOut(){
		isInCar = false;
		//Debug.Log("is NOT in car");
		GetComponent<MeshRenderer>().enabled = true;
		Vector3 carPos = car.transform.position;
		transform.parent = null;
		PlayerCamera.transform.SetParent(this.gameObject.transform,false);
		PlayerCamera.transform.localPosition = new Vector3(0,0.78f,0);
		PlayerCamera.transform.localRotation = Quaternion.Euler(0,0,0);
		transform.position = new Vector3(carPos.x-2,carPos.y+2,carPos.z+3);
		transform.eulerAngles = new Vector3(0,car.transform.eulerAngles.y,0);
		GetComponent<CapsuleCollider>().isTrigger = false;
		GetComponent<CharacterController>().enabled = true;
		car.GetComponent<SimpleCarController>().enabled = false;
		car.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
		car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
	}
	
	public void GotHitByAxe()
	{
		//Debug.Log(carHealth);
		if(isInCar)
		{
			carHealth -= AxeCarDamage;
		}else
		{
			health -= AxePlayerDamage;
		}
		//Debug.Log(carHealth);
	}
}