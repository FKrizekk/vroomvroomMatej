using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarScreenScript : MonoBehaviour
{
	public ObjectiveScript objScript;


	//  \|/--
	public Rigidbody carRb;
	public TMP_Text speedometerText;
	

	public TMP_Text objectiveText;
	public bool hasObjective = false;
	public int chosenObj = 0;
	
	int i = 0;
	
	int currentObj;

	string temp;
	string[] loading = new string[] {@"\","|","/","--"};
	int loadingInt = 0;

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(obj());
	}

	// Update is called once per frame
	void Update()
	{
		currentObj = i;
		//Speedometer
		if(((int)(Mathf.Abs(carRb.velocity.x) + Mathf.Abs(carRb.velocity.y) + Mathf.Abs(carRb.velocity.z))) < 10){
			temp = "0" + ((int)(Mathf.Abs(carRb.velocity.x) + Mathf.Abs(carRb.velocity.y) + Mathf.Abs(carRb.velocity.z))).ToString();
		}else{
			temp = ((int)(Mathf.Abs(carRb.velocity.x) + Mathf.Abs(carRb.velocity.y) + Mathf.Abs(carRb.velocity.z))).ToString();
		}
		speedometerText.text = temp + " MPH";
		
		//HasObjective
		if(GameControllerScript.objectivesStatus.Contains('N'))
		{
			hasObjective = true;
		}else
		{
			hasObjective = false;
		}
		
		//Objective Choosing
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Left();
		}else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			Right();
		}
	}
	
	void Right()
	{
		for (int x = i+1; x < GameControllerScript.objectivesStatus.Length; x++)
		{
			if(GameControllerScript.objectivesStatus[x] == 'N')
			{
				i = x;
				break;
			}
		}
	}
	
	void Left()
	{
		for (int x = i-1; x > -1; x--)
		{
			if(GameControllerScript.objectivesStatus[x] == 'N')
			{
				i = x;
				break;
			}
		}
	}

	IEnumerator obj(){
		if(!hasObjective && i>GameControllerScript.objectivesStatus.Length-1 || i < 0 || GameControllerScript.objectivesStatus[i] != 'N'){
			objectiveText.text = "waiting for obj" + loading[loadingInt];
			loadingInt++;
			if(loadingInt > 3){
				loadingInt = 0;
			}
			yield return new WaitForSeconds(0.5f);
		}else{
			objectiveText.text = GameControllerScript.objectivesRemaining[i];
			
		}
		yield return null;
		StartCoroutine(obj());
	}
}
