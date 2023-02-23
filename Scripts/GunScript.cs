using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
	public Animator anim;
	public GunCameraScript gunCameraScript;
	
	public Transform shootPoint;
	
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
		
		switch (weaponsSystemsScript.currentWeaponLvl)
		{
			case 1:
				audio.PlaySound(2, gameController.sfxVolScale*gameController.masterVolScale);
				//Start of the shoot animation(raycast here)
				Debug.DrawRay(shootPoint.position, shootPoint.forward*20);
				if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, 20,layerMask))
				{
					if(hit.collider.gameObject.tag == "Car" && MatejController.matejActive){
						MatejController.health = MatejController.health-10;
						matejControl.matejOnGotHit();
					}
				}
				break;
			case 2:
				audio.PlaySound(2, gameController.sfxVolScale*gameController.masterVolScale);
				//Start of the shoot animation(raycast here)
				Debug.DrawRay(shootPoint.position, shootPoint.forward*20);
				if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, 20,layerMask))
				{
					if(hit.collider.gameObject.tag == "Car" && MatejController.matejActive){
						MatejController.health = MatejController.health-10;
					}
				}
				break;
		}
			
	}
	
	IEnumerator Shoot()
	{
		switch (weaponsSystemsScript.currentWeaponLvl)
		{
			case 1:
				yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && gunCameraScript.active);
				anim.SetBool("Shoot",true);
				yield return new WaitForSeconds(0.005f);
				anim.SetBool("Shoot",false);
				break;
		}
		
		StartCoroutine(Shoot());
	}
}
