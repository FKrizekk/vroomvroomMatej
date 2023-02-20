using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

	public GameObject car;
	public GameObject PlayerCamera;
	public GameObject getInText;
	
	public GameObject screen;
	public GameObject gunScreen;
	public GameObject screenCanvas;
	public GameObject gunScreenCanvas;

	public AudioControllerScript cameraAudio;

	int layerMask = 1 << 3;
	
	bool canGetIn = false;
	bool canTalk = false;
	public bool isInCar = false;
	public bool isGunning = false;


	public GunCameraScript gunCameraScript;
	public CameraScript cameraScript;
	
	public Animator mapPanelAnim;
	
	// Start is called before the first frame update
	void Start()
	{
		layerMask = ~layerMask;
		DisableWeapons();
	}

	// Update is called once per frame
	void Update()
	{
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
				}else if(hit.collider.gameObject.tag == "NPC"){
					canTalk = true;
					canGetIn = false;
				}else{
					canGetIn = false;
					canTalk = false;
				}
			}else{
				canGetIn = false;
			}

			if(canTalk){
				if(Input.GetKeyDown("e")){
					hit.collider.gameObject.GetComponent<NPCScript>().Talk();
				}
			}
			
			
			
		}else{
			//INCAR
			canTalk = false;
			if(Input.GetKeyDown("e")){
				car.GetComponent<SimpleCarController>().engine.setPitch(1f);
				GetOut();
				DisableWeapons();
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

		if(canGetIn){
			getInText.SetActive(true);
			if(Input.GetKeyDown("e")){
				GetIn();
			}
		}else{
			getInText.SetActive(false);
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
	}
	
	void DisableWeapons()
	{
		isGunning = false;
		screen.GetComponent<MeshRenderer>().enabled = true;
		screenCanvas.SetActive(true);
		gunScreen.GetComponent<MeshRenderer>().enabled = false;
		gunScreenCanvas.SetActive(false);
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
		GetComponent<CapsuleCollider>().enabled = !GetComponent<CapsuleCollider>().enabled;
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
		GetComponent<CapsuleCollider>().enabled = true;
		GetComponent<CharacterController>().enabled = true;
		car.GetComponent<SimpleCarController>().enabled = false;
		car.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
		car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
	}
}