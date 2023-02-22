using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
	public GameObject player;
	
	public Rigidbody rb;

	void Start()
	{
		player = GameObject.Find("Player");
		transform.parent = null;
	}

	// Update is called once per frame
	void Update()
	{
		var step = 10f * Time.deltaTime; // calculate distance to move
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y,player.transform.position.z), step);
	}
	
	void FixedUpdate()
	{
		Vector3 targetDirection = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z);
		rb.MoveRotation(Quaternion.Euler(new Vector3(Quaternion.LookRotation(targetDirection * 100).eulerAngles.x,Quaternion.LookRotation(targetDirection * 100).eulerAngles.y + 90,Quaternion.LookRotation(targetDirection * 100).eulerAngles.z)));
	}
}
