using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgolathScript : MonoBehaviour
{
	public GameObject infinite;
	
	public void StartENDING()
	{
		SpherusScript sphere = Instantiate(infinite, transform.position, Quaternion.identity).GetComponent<SpherusScript>();
		sphere.Expandus();
		
	}
}
