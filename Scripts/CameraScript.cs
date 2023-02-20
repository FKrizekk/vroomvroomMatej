using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public float MouseSensitivity = 50f;

	public GameObject player;

	public GameControllerScript gameControl;

	float xRotation = 0f;

	private Vector3 lastOffset = new Vector3 (0f, 0f, -1f);

	float PosLerpSpeed = 10f;
	
	public bool active = false;
	
	public static bool isInMap = false;
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if(!isInMap){
			if(PlayerMovement.canMove){
				Vector3 offset = new Vector3 (0, 0.65f, -0.1f);
				Vector3 wantedCameraPosition = player.transform.rotation * offset;
					
				Vector3 smoothedPos = Vector3.Lerp (lastOffset, wantedCameraPosition, Time.deltaTime * PosLerpSpeed);
				transform.position = smoothedPos + player.transform.position;

				lastOffset = smoothedPos;
			}
		}else
		{
			Vector3 wantedCameraPosition = new Vector3(600f,1133.90002f,600f);
				
			Vector3 smoothedPos = Vector3.Lerp (lastOffset, wantedCameraPosition, Time.deltaTime * PosLerpSpeed);
			transform.position = smoothedPos;

			lastOffset = smoothedPos;
		}
	}

	void FixedUpdate(){
		if(!isInMap){
			if(active){
				if(PlayerMovement.canMove){
					MouseSensitivity = gameControl.sensitivity;

					//Look controls
					float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
					float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

					xRotation -= mouseY;
					xRotation = Mathf.Clamp(xRotation,-90f,90f);

					
					player.transform.Rotate(Vector3.up * mouseX);

					transform.eulerAngles = new Vector3(xRotation,player.transform.eulerAngles.y,player.transform.eulerAngles.z);
				}
			}else
			{
				transform.eulerAngles = new Vector3(player.transform.eulerAngles.x,player.transform.eulerAngles.y,transform.eulerAngles.z);
			}
		}else
		{
			if(PlayerMovement.canMove){
				transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(90f,180f,0f), Time.deltaTime * PosLerpSpeed);
			}
		}
	}
}