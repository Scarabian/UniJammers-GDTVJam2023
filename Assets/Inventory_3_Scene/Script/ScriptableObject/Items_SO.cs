using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Items_SO", menuName = "Inventory System_3/Items")]
public class Items_SO : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public GameObject itemPrefab;
    public int itemQuantiy;
    public WhatWorldAreYouIn wORLD = new WhatWorldAreYouIn();
    public string itemNameDull;
    public Sprite itemSpriteDull;
    public GameObject itemPrefabDull;

    public enum WhatWorldAreYouIn
    {
        Light,
        Dull
    }

    public void ItemChange()
    {
        if(itemName == "FlashLight")
        {
            
        }
    }

    public void UseItem()
    {
        if(itemName == "Sun")
        {
            //run animation of light being use by the player
            Debug.Log("Sun is used");
        }
    }
}
