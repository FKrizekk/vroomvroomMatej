using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
	public GameObject gold;
	
	int goldAmount = 70;
	
	float cordMax = 1200;
	
	void Start()
	{
		for (int i = 0; i < goldAmount; i++)
		{
			Instantiate(gold, new Vector3(Random.Range(0,cordMax),400,Random.Range(0,cordMax)), Quaternion.identity);
		}
	}
}
