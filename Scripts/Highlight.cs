using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
	//we assign all the renderers here through the inspector
	[SerializeField]
	private List<Renderer> renderers;
	[SerializeField]
	private Color color = Color.white;

	//helper list to cache all the materials ofd this object
	private List<Material> materials;
	

	
	//Gets all the materials from each renderer
	private void Awake()
	{
		
		materials = new List<Material>();
		foreach (var renderer in renderers)
		{
			//A single child-object might have mutliple materials on it
			//that is why we need to all materials with "s"
			materials.AddRange(new List<Material>(renderer.materials));
		}
	}
	
	void Start()
	{
		RaycastHit hit;
		if(Physics.Raycast(transform.position,Vector3.down,out hit, 1000))
		{
			transform.position = new Vector3(transform.position.x,hit.point.y,transform.position.z);
		}
	}

	public void ToggleHighlight(bool val)
	{
		
		if (val)
		{
			foreach (var material in materials)
			{
				material.EnableKeyword("_EmissiveColor");
				//Debug.Log(material);
				material.SetFloat("_EmissiveExposureWeight", 0.5f); 
				//material.SetColor("_EmissiveColor", color * 15);
				
			}
		}
		else
		{
			foreach (var material in materials)
			{
				material.EnableKeyword("_EmissiveColor");
				//Debug.Log(material);
				material.SetFloat("_EmissiveExposureWeight", 1f); 
				//material.SetColor("_EmissiveColor", color * 0);
				
			}
		}
		
		
	}
}