using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableScript : MonoBehaviour
{
	public Material mat;
	
	public Material matHGL;
	
	Color emissiveColor = Color.white;
	
	public bool hgl = false;
	
	
	void Start()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit, 10000))
		{
			transform.position = hit.point;
		}
	}
	
	public void Highlight()
	{
		foreach (Transform child in transform)
		{
			child.gameObject.GetComponent<MeshRenderer>().material = matHGL;
		}
	}

	public void UnHighlight()
	{
		foreach (Transform child in transform)
		{
			child.gameObject.GetComponent<MeshRenderer>().material = mat;	
		}
	}
}