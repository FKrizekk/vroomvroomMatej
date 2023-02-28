using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo {
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public bool motor;
	public bool steering;
}
	 
public class SimpleCarController : MonoBehaviour {
	public List<AxleInfo> axleInfos; 
	public float maxMotorTorque;
	public float maxSteeringAngle;
	public float reduceCenterMassY = 0;
	public Rigidbody rb;

	public GameObject steeringWheel;
	float wheelN = 0;

	public carEngineScript engine;
	 
	void Start(){
		
		this.GetComponent<Rigidbody>().centerOfMass = new Vector3(this.GetComponent<Rigidbody>().centerOfMass.x, reduceCenterMassY , this.GetComponent<Rigidbody>().centerOfMass.z);
	}

	// finds the corresponding visual wheel
	// correctly applies the transform
	public void ApplyLocalPositionToVisuals(WheelCollider collider)
	{
		if (collider.transform.childCount == 0) {
			return;
		}
	 
		Transform visualWheel = collider.transform.GetChild(0);
	 
		Vector3 position;
		Quaternion rotation;
		collider.GetWorldPose(out position, out rotation);
	 
		visualWheel.transform.position = position;
		visualWheel.transform.rotation = rotation;
	}

	void Update(){
		if(Input.GetKey("w") || Input.GetKey("s")){
			//engine.setPitch(1.7f);
			engine.setPitch(0.7f);
		}else{
			//engine.setPitch(1f);
			engine.setPitch(0.5f);
		}

		if(Input.GetKey("a")){
			wheelN = -1;
		}else if(Input.GetKey("d")){
			wheelN = 1;
		}else{
			wheelN = 0;
		}
		wheelN = wheelN * 90f + 180f + -transform.eulerAngles.z;
		Vector3 targetRotation = new Vector3(steeringWheel.transform.eulerAngles.x,steeringWheel.transform.eulerAngles.y, wheelN);
		steeringWheel.transform.rotation = Quaternion.Slerp(steeringWheel.transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * 10f);
	}
	 
	public void FixedUpdate()
	{
		float motor = maxMotorTorque * Input.GetAxis("Vertical");
		float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
	 
		foreach (AxleInfo axleInfo in axleInfos) {
			if (axleInfo.steering) {
				axleInfo.leftWheel.steerAngle = steering;
				axleInfo.rightWheel.steerAngle = steering;
			}
			if (axleInfo.motor) {
				axleInfo.leftWheel.motorTorque = motor;
				axleInfo.rightWheel.motorTorque = motor;
			}
			ApplyLocalPositionToVisuals(axleInfo.leftWheel);
			ApplyLocalPositionToVisuals(axleInfo.rightWheel);
		}
	}
}