using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatejController : MonoBehaviour
{   
	public static int health = 10000;

	public MatejMovement matejM;

	public MusicControllerScript musicControl;

	public Animator anim;
	
	public static bool matejActive = false;
	
	public carEngineScript carEngine;
	
	public GameObject player;
	
	public GameObject LMissile;
	public GameObject RMissile;
	
	public Transform LRocketLauncher;
	public Transform RRocketLauncher;

	void Start()
	{
		StartCoroutine(MatejLoop());
	}

	void Update()
	{
		Debug.Log(health);
		
		if(Input.GetKeyDown("u"))
		{
			anim.SetBool("Rockets", true);
		}else if(Input.GetKeyDown("i"))
		{
			anim.SetBool("Rockets", false);
		}
	}

	void ActivateMatej(){
		matejM.active = true;
		musicControl.StartAction();
		matejActive = true;
		Show();
		carEngine.RadarContact();
	}

	void DeActivateMatej(){
		matejM.active = false;
		musicControl.EndAction();
		matejActive = false;
		HideRockets();
		Hide();
	}

	void Show(){
		anim.SetBool("Hide", false);
		anim.SetBool("Show", true);
	}

	
	void Hide(){
		anim.SetBool("Show", false);
		anim.SetBool("Hide", true);
	}
	
	void DeployRockets()
	{
		anim.SetBool("Rockets", true);
		StartCoroutine(RocketLogic());
	}
	
	IEnumerator RocketLogic()
	{
		var prepTime = Random.Range(1,4);
		var i = 0;
		while (i <= prepTime)
		{
			Debug.Log("Time before you are fucked: " + (prepTime-i));
			yield return new WaitForSecondsRealtime(1);
			i++;
		}
		
		if(Random.Range(1,3) == 1)
		{
			//Left
			Instantiate(LMissile, LRocketLauncher);	
		}else
		{
			//Right
			Instantiate(RMissile, RRocketLauncher);
		}
			
	}
	
	void HideRockets()
	{
		anim.SetBool("Rockets", false);
	}


	IEnumerator MatejLoop(){
		//yield return new WaitForSeconds(Random.Range(5*60, 15*60));
		yield return new WaitForSeconds(Random.Range(10, 30));
		yield return new WaitUntil(() => PlayerMovement.canMove == true);
		ActivateMatej();
		//---------------------ACTIVATED-----------------------------------------
		var startTime = Time.time;
		var startHealth = health;
		
		yield return new WaitUntil(() => Vector3.Distance(player.transform.position, transform.position) < 15);
		
		var a = Random.Range(0,25);
		Debug.Log("Deploy random num: " + a);
		if(a > 5)
		{
			Debug.Log("Deploying");
			DeployRockets();
		}else
		{
			Debug.Log("NOT Deploying");
		}
		
		if(health>5000)
		{
			yield return new WaitUntil(() => health < startHealth-100 || Time.time - startTime > 360);
		}else
		{
			yield return new WaitUntil(() => health < startHealth-30 || Time.time - startTime > 30);
		}
		//---------------------DEACTIVATED-----------------------------------------
		DeActivateMatej();

		StartCoroutine(MatejLoop());
	}
}