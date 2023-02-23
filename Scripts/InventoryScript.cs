using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
	public GameObject ItemPrefab;
	
	public Sprite[] sprites;
	
	public List<GameObject> itemObjects = new List<GameObject>();
	public Dictionary<GameObject, int> itemsHash = new Dictionary<GameObject, int>();
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		foreach (var item in itemObjects)
		{
			item.transform.GetChild(4).gameObject.GetComponent<TMP_Text>().text = itemsHash[item].ToString();
		}
	}
	
	public void AddItem(string name, int spriteIndex, int amount)
	{
		var exit = false;
		
		foreach (var key in itemsHash.Keys)
		{
			if(key.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text == name)
			{
				itemsHash[key] += amount;
				exit = true;
				break;
			}else
			{
				exit = false;
			}
		}
		
		if(!exit){
			var spawnedObject = Instantiate(ItemPrefab, gameObject.transform);
			spawnedObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = sprites[spriteIndex];
			spawnedObject.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = name;
			
			itemObjects.Add(spawnedObject);
			itemsHash.Add(spawnedObject, amount);
		}
		
		var i = -700;
		foreach (var item in itemObjects)
		{
			item.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(i,item.GetComponent<RectTransform>().anchoredPosition3D.y,item.GetComponent<RectTransform>().anchoredPosition3D.z);
			i += 350;
		}
	}
	
	public void RemoveItem(string name, int amount)
	{
		foreach (var key in itemsHash.Keys)
		{
			if(key.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text == name)
			{
				itemsHash[key] -= amount;
				if(itemsHash[key] <= 0)
				{
					itemObjects.Remove(key);
					itemsHash.Remove(key);
					Destroy(key);
					
					var i = -700;
					foreach (var item in itemObjects)
					{
						item.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(i,item.GetComponent<RectTransform>().anchoredPosition3D.y,item.GetComponent<RectTransform>().anchoredPosition3D.z);
						i += 350;
					}
				}
				return;
			}
		}
		
		
	}
}