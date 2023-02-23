using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBarsScript : MonoBehaviour
{
	public void Delete()
	{
		Destroy(gameObject);
	}
	
	public void Highlight()
	{
		Debug.Log("HIGHLIGHT");
	}
}
