using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBtnPrefab : MonoBehaviour
{
   public string idItem;
   public GameObject house;
   public GameObject btnAdd;
   public Sprite sprite;
   
   void addHouseOnClick(){
       if(house == null){
            house = Instantiate(Resources.Load("prefabs/island/"+idItem, typeof(GameObject))) as GameObject;
       }
		GridBuildingSystem.instance.initializeWithBuilding(house, null);
	}
   void addItemOnClick(){
       if(house == null){
            house = Instantiate(Resources.Load("prefabs/island/"+idItem, typeof(GameObject))) as GameObject;
       }
        
	    GridBuildingSystem.instance.initializeWithBuilding(house, sprite);
	}

    public void addEvent() {
        btnAdd.GetComponent<Button>().onClick.AddListener(addHouseOnClick);
    }
    public void addEventItem(string pathImg) {
        sprite = Resources.Load<Sprite>("IconBtn/"+pathImg) ;
        btnAdd.GetComponent<Button>().onClick.AddListener(addItemOnClick);
    }
}
