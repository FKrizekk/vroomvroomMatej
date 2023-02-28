using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class InventoryScript : MonoBehaviour
{
	public GameObject ItemPrefab;
	
	public Sprite[] sprites;
	
	public List<GameObject> itemObjects = new List<GameObject>();
	public Dictionary<GameObject, int> itemsHash = new Dictionary<GameObject, int>();
	
	Dictionary<string, int> nameSpriteIndexDict = new Dictionary<string, int>
	{
		{"Gold bars", 0},
		{"Kuřecí řízek", 1}
	};
	
	public void UpdateInventory()
	{
		var list = GameControllerScript.inventory.Split(',');
		foreach(var item in list)
		{
			Debug.Log("LOADED INV ITEM AS: " + item);
			string name = item.Split('-')[0];
			int spriteIndex = int.Parse(item.Split('-')[1]);
			int amount = int.Parse(item.Split('-')[2]);
			
			AddItem(name, spriteIndex, amount);
		}
	}
	
	public void SaveInventory()
	{
		string inv = "";
		foreach(var key in itemsHash.Keys)
		{
			var tempName = key.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text;
			if(key != new List<GameObject>(this.itemsHash.Keys).Last())
			{
				inv = inv+tempName+"-"+nameSpriteIndexDict[tempName].ToString()+"-"+itemsHash[key].ToString()+",";
			}else
			{
				inv = inv+tempName+"-"+nameSpriteIndexDict[tempName].ToString()+"-"+itemsHash[key].ToString();
			}
		}
		PlayerPrefs.SetString("inventory", inv);
		Debug.Log("SAVED INVENTORY AS: " + inv);
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