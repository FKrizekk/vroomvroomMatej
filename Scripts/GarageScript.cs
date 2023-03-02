using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageScript : MonoBehaviour
{
	public Animator anim;
	
	public void RepairHighlight()
	{
		anim.SetBool("HighlightRepair", true);
	}
	
	public void RepairUnHighlight()
	{
		anim.SetBool("HighlightRepair", false);
		anim.SetBool("SelectRepair", false);
	}
	
	public void RepairSelect()
	{
		anim.SetBool("SelectRepair", true);
		Invoke("RepairUnselect",0.1f);
	}
	
	void RepairUnselect()
	{
		anim.SetBool("SelectRepair", false);
	}
}
