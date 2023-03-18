using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteScript : MonoBehaviour
{
	public Material[] mats;
	
	void Start()
	{
		GetComponent<Renderer>().material = mats[1];
	}
	
	void Update()
	{
		var objectivesStatus = GameControllerScript.objectivesStatus.ToCharArray();
		if(objectivesStatus[2] == 'N')
		{
			gameObject.GetComponent<Renderer>().enabled = true;
		}else
		{
			gameObject.GetComponent<Renderer>().enabled = false;
		}
	}
	
	public void Place()
	{
		GetComponent<Renderer>().material = mats[0];
		GameControllerScript.dynPlaced++;
	}
}
