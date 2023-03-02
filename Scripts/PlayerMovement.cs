using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController controller;

	public PlayerScript playerScript;

	public Transform groundCheck;

	float groundDistance = 0.4f;
	public LayerMask groundMask;

	float speed = 8f;
	float gravity = -9.81f;
	public bool isGrounded;
	float jumpHeight = 2f;

	public static bool canMove = true;

	Vector3 velocity;
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if(canMove && !playerScript.isInCar){
			if(Input.GetKey(KeyCode.LeftShift)){
				speed = 9f;
			}else{
				speed = 5f;
			}

			if(Input.GetKeyDown("space") && isGrounded){
				velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
				playerScript.PlayJumpSound();
			}


			isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);

			if(isGrounded && velocity.y < 0){
				velocity.y = -2f;
			}
			
			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");

			Vector3 move = transform.right * x + transform.forward * z;

			controller.Move(move * speed * Time.deltaTime);

			velocity.y += gravity * Time.deltaTime;

			controller.Move(velocity * Time.deltaTime);
		}
	}
}
