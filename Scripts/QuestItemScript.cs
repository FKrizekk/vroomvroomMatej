using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItemScript : MonoBehaviour
{
	public void FinishQuest(int index)
	{
		var objectivesStatus = GameControllerScript.objectivesStatus.ToCharArray();
		objectivesStatus[index] = 'Y';
		GameControllerScript.objectivesStatus = new string(objectivesStatus);
	}
}
