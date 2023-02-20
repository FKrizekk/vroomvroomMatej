using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapObjScript : MonoBehaviour
{
	public Animator anim;
	
	
	public void ShowDetails()
	{
		anim.SetBool("ShowDetails", true);
	}
	
	public void HideDetails()
	{
		anim.SetBool("ShowDetails", false);
	}
	
	void Update()
	{
		int i = (int)char.GetNumericValue(gameObject.name.Split('(')[1][0]);
		
		if(GameControllerScript.objectivesStatus[i-1] == 'N')
		{
			transform.localScale = new Vector3(1f,1f,1f);
		}else
		{
			transform.localScale = new Vector3(1f,0f,1f);
		}
	}
}
