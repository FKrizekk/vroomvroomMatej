using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCameraScript : MonoBehaviour
{
	public float MouseSensitivity = 50f;

	public GameObject player;

	public GameControllerScript gameControl;

	float xRotation = 0f;

	private Vector3 lastOffset = new Vector3 (0f, 0f, -1f);

	float PosLerpSpeed = 10f;
	
	
	public GameObject gun;
	public GameObject topGun;
	
	public bool active = false;
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if(active){
			if(PlayerMovement.canMove){
				Vector3 offset = new Vector3 (-0.2f, 0.75f, 0f);
				Vector3 wantedCameraPosition = gun.transform.rotation * offset;
					
				Vector3 smoothedPos = Vector3.Lerp (lastOffset, wantedCameraPosition, Time.deltaTime * PosLerpSpeed);
				transform.position = smoothedPos + gun.transform.position;

				lastOffset = smoothedPos;
			}
		}
	}

	void FixedUpdate(){
		if(active){
			if(PlayerMovement.canMove){
				MouseSensitivity = gameControl.sensitivity;

				//Look controls
				float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
				float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

				xRotation -= mouseY;
				xRotation = Mathf.Clamp(xRotation,-90f,90f);

				
				gun.transform.Rotate(Vector3.up * mouseX);

				
				topGun.transform.eulerAngles = new Vector3(xRotation,gun.transform.eulerAngles.y,gun.transform.eulerAngles.z);
				transform.eulerAngles = new Vector3(topGun.transform.eulerAngles.x,gun.transform.eulerAngles.y,gun.transform.eulerAngles.z+180);
			}
		}
	}
}
