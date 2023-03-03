using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LojzaDetectTrigger : MonoBehaviour
{
	public NPCScript lojza;
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.name.Contains("Player"))
		{
			lojza.LojzaArriveSound();
		}
	}
}
