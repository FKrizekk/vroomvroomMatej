using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatejController : MonoBehaviour
{   
	public static int health = 10000;

	public MatejMovement matejM;

	public MusicControllerScript musicControl;

	public Animator anim;
	
	public static bool matejActive = false;
	
	public carEngineScript carEngine;
	
	public GameObject player;
	
	public GameControllerScript gameControl;
	
	public GameObject LMissile;
	public GameObject RMissile;
	
	public Transform LRocketLauncher;
	public Transform RRocketLauncher;
	
	public TMP_Text blockCountdown;
	
	public GameObject[] DangerSigns;
	
	public AudioSource source;
	
	public AudioClip[] clips;
	
	float rocketStartTime;
	
	private bool Blocking = false;
	
	private bool blockFailed = false;
	
	public bool notHitYet = true;
	
	bool active = false;
	
	bool axeLogicEnded = false;
	
	public AudioSource axeSource;
	
	bool rocketLogicEnded = false;
	
	int axeTries = 3;
	int	axeCTries = 0;
	
	bool endMatej = false;


	GameObject playercar;
	
	void Start()
	{
		StartCoroutine(MatejLoop());
		RefreshVars();
	}
	
	public void RefreshVars()
	{
		GameObject[] gameObjects = FindObjectsOfType<GameObject>();
	
		for (var i=0; i < gameObjects.Length; i++){
			if(gameObjects[i].name.Contains("PlayerCar")){
				playercar = gameObjects[i];
			}
		}
		
		carEngine = playercar.GetComponent<carEngineScript>();
		PlayerScript.RefreshVars(playercar.name);
		DangerSigns = GameObject.FindGameObjectsWithTag("DangerSign");
		foreach (var item in DangerSigns)
		{
			item.SetActive(false);
		}
		blockCountdown = playercar.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
		GunScreenScript.RefreshVars(playercar.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<Camera>());
		
	}

	void Update()
	{
		//Debug.Log("Matej health: " + health);
		
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.up, out hit, 10000))
		{
			if(hit.collider.gameObject.tag == "Terrain")
			{
				transform.position = new Vector3(transform.position.x,transform.position.y+50,transform.position.z);
			}
		}
		
		if(Input.GetKeyDown("u"))
		{
			//DEBUG HEALTH RESET
			health = 10000;
		}
		
		if(Blocking)
		{
			if(Time.time - rocketStartTime < 1)
			{
				if(Input.GetKeyDown("c"))
				{
					StopCoroutine(RocketLogic());
					rocketLogicEnded = true;
					blockCountdown.text = "";
					carEngine.matejCounterRocketSuccessful();
					HideRockets();
					Blocking = false;
				}
			}else
			{
				blockFailed = true;
				Blocking = false;
				HideRockets();
			}
			
		}
		
		if(transform.position.y < 50)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 100);
			GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}
		
		if(transform.position.x > 10000 || transform.position.x < -1000 || transform.position.z > 10000 || transform.position.z < -10000)
		{
			transform.position = new Vector3(600,100,600);
			GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}
	}

	void ActivateMatej(){
		matejM.active = true;
		musicControl.StartAction();
		matejActive = true;
		Show();
		//carEngine.RadarContact();
		carEngine.matejSpawn();
		active = true;
	}

	void DeActivateMatej(){
		matejM.active = false;
		musicControl.EndAction();
		matejActive = false;
		HideRockets();
		Hide();
		carEngine.matejKilled();
		active = false;
	}

	void Show(){
		anim.SetBool("Hide", false);
		anim.SetBool("Show", true);
	}

	
	void Hide(){
		anim.SetBool("Show", false);
		anim.SetBool("Hide", true);
	}
	
	//ROCKETS//
	
	void DeployRockets()
	{
		anim.SetBool("Rockets", true);
		StartCoroutine(RocketLogic());
		carEngine.matejLockedOn();
	}
	
	IEnumerator RocketLogic()
	{
		var prepTime = Random.Range(3,6);
		var i = 0;
		while (i <= prepTime)
		{
			//Debug.Log("Time before you press BLOCK: " + (prepTime-i));
			
			foreach (var item in DangerSigns)
			{
				item.SetActive(true);
			}
			yield return new WaitForSeconds(0.5f);
			foreach (var item in DangerSigns)
			{
				item.SetActive(false);
			}
			yield return new WaitForSeconds(0.5f);
			blockCountdown.text = (prepTime-i).ToString();
			i++;
		}
		rocketStartTime = Time.time;
		Blocking = true;
		blockCountdown.text = "NOW";
		
		
		yield return new WaitUntil(() => blockFailed);

		blockFailed = false;
		
		
		
		if(Random.Range(1,3) == 1)
		{
			//Left
			Instantiate(LMissile, LRocketLauncher);	
		}else
		{
			//Right
			Instantiate(RMissile, RRocketLauncher);
		}
		blockCountdown.text = "<color=red>X";
		//carEngine.matejFiredRocket();
		yield return new WaitForSecondsRealtime(1);
		blockCountdown.text = "";
		rocketLogicEnded = true;
			
	}
	
	void HideRockets()
	{
		anim.SetBool("Rockets", false);
	}
	
	//AXE//
	IEnumerator AxeLogic()
	{
		yield return new WaitUntil(() => Vector3.Distance(player.transform.position, transform.position) < 5);
		if(axeTries > axeCTries)
		{
			if(Vector3.Distance(player.transform.position, transform.position) < 5)
			{
				anim.SetBool("AxeAttack", true);
				axeCTries++;
				yield return new WaitForSeconds(1.5f);
				StartCoroutine(AxeLogic());
				yield return null;
			}else
			{
				anim.SetBool("AxeAttack", false);
				yield return null;
			}
		}else{
			anim.SetBool("AxeAttack", false);
			axeLogicEnded = true;
			yield return null;
		}
	}
	
	public void PlayerDeath()
	{
		endMatej = true;
	}



	IEnumerator MatejLoop(){
		//yield return new WaitForSeconds(Random.Range(5*60, 15*60));
		yield return new WaitForSeconds(Random.Range(10, 30));
		yield return new WaitUntil(() => PlayerMovement.canMove == true);
		ActivateMatej();
		StartCoroutine(MatejSoundLoop());
		notHitYet = true;
		//---------------------ACTIVATED-----------------------------------------
		var startTime = Time.time;
		var startHealth = health;
		
		yield return new WaitUntil(() => Vector3.Distance(player.transform.position, transform.position) < 30);
		
		var a = Random.Range(0,25);
		//Debug.Log("Deploy random num: " + a);
		if(a > 5)
		{
			Debug.Log("Deploying");
			if(Random.Range(0,25) > 17)
			{
				matejRocketsDeployed();
			}
			DeployRockets();
		}else
		{
			Debug.Log("NOT Deploying");
			rocketLogicEnded = true;
		}
		
		if(health>5000)
		{
			yield return new WaitUntil(() => health < startHealth-100 || Time.time - startTime > 360 || rocketLogicEnded);
		}else
		{
			yield return new WaitUntil(() => health < startHealth-30 || Time.time - startTime > 30 || rocketLogicEnded);
		}
		rocketLogicEnded = false;
		Debug.Log("ROCKET LOGIC ENDED");
		
		axeTries = 5;
		axeCTries = 0;
		StartCoroutine(AxeLogic());
		Debug.Log("AXE LOGIC STARTED");
		
		if(health>5000)
		{
			yield return new WaitUntil(() => health < startHealth-100 || Time.time - startTime > 360 || axeLogicEnded);
		}else
		{
			yield return new WaitUntil(() => health < startHealth-30 || Time.time - startTime > 30 || axeLogicEnded);
		}
		axeLogicEnded = false;
		matejLaugh();
		Debug.Log("AXE LOGIC ENDED");
		
		if(health>5000)
		{
			yield return new WaitUntil(() => health < startHealth-100 || Time.time - startTime > 360 || endMatej);
		}else
		{
			yield return new WaitUntil(() => health < startHealth-30 || Time.time - startTime > 30 || endMatej);
		}
		endMatej = false;
		
		//---------------------DEACTIVATED-----------------------------------------
		DeActivateMatej();
		StopCoroutine(MatejSoundLoop());

		StartCoroutine(MatejLoop());
	}
	
	
	
	//SOUNDS//
	
	IEnumerator MatejSoundLoop()
	{
		yield return new WaitForSeconds(20);
		if(active)
		{
			matejRandomHlaska();
		}
		StartCoroutine(MatejSoundLoop());
	}
	
	void matejRandomHlaska()
	{
		source.PlayOneShot(clips[0 + Random.Range(0,4)],gameControl.dialogVolScale * gameControl.masterVolScale);
	}
	
	public void matejOnGotHit()
	{
		if(notHitYet)
		{
			notHitYet = false;
			source.PlayOneShot(clips[0 + Random.Range(15,20)],gameControl.dialogVolScale * gameControl.masterVolScale);
		}
	}
	
	void matejRocketsDeployed()
	{
		source.PlayOneShot(clips[21],gameControl.dialogVolScale * gameControl.masterVolScale);
	}
	
	void matejLaugh()
	{
		source.PlayOneShot(clips[22],gameControl.dialogVolScale * gameControl.masterVolScale);
	}
	
	public void AxeSound()
	{
		axeSource.PlayOneShot(axeSource.clip,gameControl.sfxVolScale*gameControl.masterVolScale);
	}
}