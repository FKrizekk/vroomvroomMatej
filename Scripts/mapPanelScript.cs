using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapPanelScript : MonoBehaviour
{
	public GameObject arrow;
	
	// Update is called once per frame
	void Update()
	{
		if(CameraScript.isInMap)
		{
			arrow.SetActive(true);
		}else
		{
			arrow.SetActive(false);
		}
	}
}
