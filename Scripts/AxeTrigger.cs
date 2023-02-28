using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTrigger : MonoBehaviour
{
	public PlayerScript playerScript;

	
	void OnTriggerEnter(Collider col)
	{
		//Debug.Log("TRIGGER ENTER");
		//Debug.Log(col.gameObject.name);
		if(col.gameObject.name == "Ferrari Testarosa" || col.gameObject.name == "Player")
		{
			//Debug.Log("IF STATEMENT");
			playerScript.GotHitByAxe();
		}
	}
}
