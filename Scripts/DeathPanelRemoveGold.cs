using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathPanelRemoveGold : MonoBehaviour
{
	public InventoryScript invScript;
	
	public TMP_Text textik;
	
	public PlayerScript playerScript;
	public MatejController matejControl;
	
	public Animator anim;
	
	bool canRespawn = false;
	
	public void Death()
	{
		invScript.RemoveItem("Gold bars", 1);
	}
	
	public void GetInvGoldAmount()
	{
		textik.text = invScript.GetAmountOf("Gold bars").ToString();
	}
	
	public void SendRespawnSignal()
	{
		playerScript.Respawn();
		matejControl.PlayerDeath();
	}
	
	public void DisableDiedBool()
	{
		anim.SetBool("Died", false);
	}
	
	public void DisableRespawnScreenBool()
	{
		anim.SetBool("RespawnScreen", false);
	}
	
	public void CanRespawn()
	{
		canRespawn = true;
	}
	
	void Update()
	{
		if(Input.GetKeyDown("space") && canRespawn)
		{
			canRespawn = false;
			if(PlayerScript.carHealth > 0)
			{
				//playerScript.GetIn();
				PlayerScript.health = 100;
			}else
			{
				PlayerScript.car.transform.position = new Vector3(1085.01001f,80.25f,114.739998f);
				//playerScript.GetIn();
				PlayerScript.carHealth = 1000;
				PlayerScript.health = 100;
			}
			anim.SetBool("RespawnScreen", true);
		}
	}
}
