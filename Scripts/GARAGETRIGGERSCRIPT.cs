using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GARAGETRIGGERSCRIPT : MonoBehaviour
{
	public NPCScript karelnpc;
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.name.Contains("Ferrari Testarossa"))
		{
			karelnpc.PlayerArrived();
		}
	}
}
