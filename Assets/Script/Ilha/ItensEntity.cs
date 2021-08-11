using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class ItensEntity
{
    public string idItem;
    public string idItemImage;
    public string itemName;
    public ItemType itemType;
    public ItemCategory itemCategory;
    
}
public enum ItemType
{
    HELMET = 0,
    ARMOR = 1,
    SWORD = 2,
    BEAST = 3,
    HAMMER = 4,
    SHIELD = 5
}

public enum ItemCategory
{
    HOUSE = 1,
    DECORATION = 2,
    ITENS = 3,
    NFT = 4
}