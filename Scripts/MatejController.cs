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

	void Start()
	{
		StartCoroutine(MatejLoop());
	}

	void Update()
	{
		Debug.Log(health);
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


	IEnumerator MatejLoop(){
		//yield return new WaitForSeconds(Random.Range(5*60, 15*60));
		yield return new WaitForSeconds(Random.Range(10, 30));
		yield return new WaitUntil(() => PlayerMovement.canMove == true);
		ActivateMatej();
		var startTime = Time.time;
		var startHealth = health;
		if(health>5000)
		{
			yield return new WaitUntil(() => health < startHealth-100 || Time.time - startTime > 360);
		}else
		{
			yield return new WaitUntil(() => health < startHealth-30 || Time.time - startTime > 30);
		}
		DeActivateMatej();

		StartCoroutine(MatejLoop());
	}
}