using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherusScript : MonoBehaviour
{
	bool expand = false;
	
	public void Expandus()
	{
		expand = true;
	}
	
	void Update()
	{
		if(expand)
		{
			float speed = 1;
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(500,500,500), Time.deltaTime * speed);
		}
	}
}
