using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
	public Animator anim;
	public GunCameraScript gunCameraScript;
	
	//public Transform gunCamera;
	public Transform gunCamera;
	
	public LayerMask layerMask;
	
	public AudioControllerScript audio;
	public GameControllerScript gameController;
	
	public MatejController matejControl;
	
	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(Shoot());
	}
	
	public void Shoot2()
	{
		RaycastHit hit;
		switch (int.Parse(gameObject.name.Split("_")[2]))
		{
			case 1:
				audio.PlaySound(2, gameController.sfxVolScale*gameController.masterVolScale);
				//Start of the shoot animation(raycast here)
				Debug.DrawRay(gunCamera.position, gunCamera.forward*20);
				if (Physics.Raycast(gunCamera.position, gunCamera.forward, out hit, PlayerScript.LVL1GunRange,layerMask))
				{
					if(hit.collider.gameObject.tag == "Car" && MatejController.matejActive){
						MatejController.health = MatejController.health-PlayerScript.LVL1GunDamage;
						matejControl.matejOnGotHit();
					}
				}
				break;
			case 2:
				audio.PlaySound(3, gameController.sfxVolScale*gameController.masterVolScale);
				//Start of the shoot animation(raycast here)
				Debug.DrawRay(gunCamera.position, gunCamera.forward*20);
				if (Physics.Raycast(gunCamera.position, gunCamera.forward, out hit, PlayerScript.LVL2GunRange,layerMask))
				{
					if(hit.collider.gameObject.tag == "Car" && MatejController.matejActive){
						MatejController.health = MatejController.health-PlayerScript.LVL2GunDamage;
					}
				}
				break;
			case 3:
				audio.PlaySound(4, gameController.sfxVolScale*gameController.masterVolScale);
				//Start of the shoot animation(raycast here)
				Debug.DrawRay(gunCamera.position, gunCamera.forward*20);
				if (Physics.Raycast(gunCamera.position, gunCamera.forward, out hit, PlayerScript.LVL3GunRange,layerMask))
				{
					if(hit.collider.gameObject.tag == "Car" && MatejController.matejActive){
						MatejController.health = MatejController.health-PlayerScript.LVL3GunDamage;
					}
				}
				break;
			case 4:
				audio.PlaySound(5, gameController.sfxVolScale*gameController.masterVolScale);
				//Start of the shoot animation(raycast here)
				Debug.DrawRay(gunCamera.position, gunCamera.forward*20);
				if (Physics.Raycast(gunCamera.position, gunCamera.forward, out hit, PlayerScript.LVL4GunRange,layerMask))
				{
					if(hit.collider.gameObject.tag == "Car" && MatejController.matejActive){
						MatejController.health = MatejController.health-PlayerScript.LVL4GunDamage;
					}
				}
				break;
			case 5:
				audio.PlaySound(6, gameController.sfxVolScale*gameController.masterVolScale);
				//Start of the shoot animation(raycast here)
				Debug.DrawRay(gunCamera.position, gunCamera.forward*20);
				if (Physics.Raycast(gunCamera.position, gunCamera.forward, out hit, PlayerScript.LVL5GunRange,layerMask))
				{
					if(hit.collider.gameObject.tag == "Car" && MatejController.matejActive){
						MatejController.health = MatejController.health-PlayerScript.LVL5GunDamage;
					}
				}
				break;

		}
			
	}
	
	IEnumerator Shoot()
	{
		switch (int.Parse(gameObject.name.Split("_")[2]))
		{
			case 1:
				yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && gunCameraScript.active);
				anim.SetBool("Shoot",true);
				yield return new WaitForSeconds(0.05f);
				anim.SetBool("Shoot",false);
				break;
			case 2:
				yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && gunCameraScript.active);
				anim.SetBool("Shoot",true);
				yield return new WaitForSeconds(0.05f);
				anim.SetBool("Shoot",false);
				break;
			case 3:
				yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && gunCameraScript.active);
				anim.SetBool("Shoot",true);
				yield return new WaitForSeconds(0.05f);
				anim.SetBool("Shoot",false);
				break;
			case 4:
				yield return new WaitUntil(() => Input.GetMouseButton(0) && gunCameraScript.active);
				anim.SetBool("Shoot",true);
				yield return new WaitForSeconds(0.005f);
				anim.SetBool("Shoot",false);
				break;
			case 5:
				yield return new WaitUntil(() => Input.GetMouseButton(0) && gunCameraScript.active);
				anim.SetBool("Shoot",true);
				yield return new WaitForSeconds(1f);
				anim.SetBool("Shoot",false);
				break;
		}
		
		StartCoroutine(Shoot());
	}
}
