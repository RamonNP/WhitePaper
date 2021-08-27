using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Owner : MonoBehaviour
{
    public ItensEntity item2;
    public List<ItemBtnPrefab> houses;
    public GameObject painelHouse;
    public GameObject painelDecoration;
    public GameObject painelItens;
    public GameObject painelNft;
    public TextAsset jsonFile1;
    public TextAsset jsonFile2;
    public TextAsset jsonFile31;
    // Start is called before the first frame update
    void Start()
    {
        LoadItensDatabase();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadItensDatabase() {
		IslandItensJson itensJson = JsonUtility.FromJson<IslandItensJson>(jsonFile1.text);

		foreach (ItensEntity item in itensJson.Itens)
        {
            GameObject obj = new GameObject();
            obj.AddComponent(typeof(ItemBtnPrefab));
            ItemBtnPrefab itemBtn = obj.GetComponent<ItemBtnPrefab>();
            itemBtn.idItem =  item.idItem;
            
            itemBtn.btnAdd = Instantiate(Resources.Load("prefabs/island/btnAddItem", typeof(GameObject))) as GameObject;
            itemBtn.btnAdd.GetComponent<Image>().sprite = Resources.Load<Sprite>("IconBtn/"+item.idItemImage) ;

            item2 = item;
            switch (item.itemCategory)
            {
                case ItemCategory.HOUSE :
                    itemBtn.btnAdd.transform.parent = painelHouse.transform;
                    itemBtn.addEvent(); //apenas podemos iniciar o evento quando sabe qual objeto sera setado.
                break;
                case ItemCategory.DECORATION :
                    itemBtn.btnAdd.transform.parent = painelDecoration.transform;
                    itemBtn.addEvent(); //apenas podemos iniciar o evento quando sabe qual objeto sera setado.
                break;
                case ItemCategory.ITENS :
                    itemBtn.btnAdd.transform.parent = painelItens.transform;
                    itemBtn.btnAdd.GetComponent<Image>().sprite = Resources.Load<Sprite>("IconBtn/"+item.idItemImage) ;
                    itemBtn.addEventItem(item.idItemImage); //apenas podemos iniciar o evento quando sabe qual objeto sera setado.
                break;
                case ItemCategory.NFT :
                    itemBtn.btnAdd.transform.parent = painelNft.transform;
                    itemBtn.addEventItem(item.idItemImage); //apenas podemos iniciar o evento quando sabe qual objeto sera setado.
                break;

            }
			
            houses.Add(itemBtn);
            itemBtn.btnAdd.transform.position = Vector3.zero;
            itemBtn.btnAdd.transform.localPosition = Vector3.zero;
            itemBtn.btnAdd.transform.localScale = Vector3.one;
        } 
	}
}
