using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemSlots[] itemSlot;
    public Items_SO[] items_SOs;

    public void UseItem(string itemName)
    {
        for (int i = 0; i < items_SOs.Length; i++)
        {
            if (items_SOs[i].itemName == itemName)
            {
                items_SOs[i].UseItem();
            }
        }
    }

    public int AddItem(int itemQuantiy, string itemName, Sprite itemSprite, GameObject itemPrefab, string itemNameDull, Sprite itemSpriteDull, GameObject itemPrefabDull)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if(itemSlot[i].isFull == false && itemSlot[i].nameInInventory == itemName || itemSlot[i].quantityOfInventory == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemQuantiy, itemName, itemSprite, itemPrefab, itemNameDull, itemSpriteDull, itemPrefabDull);

                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(leftOverItems, itemName, itemSprite, itemPrefab, itemNameDull, itemSpriteDull, itemPrefabDull);
                }
                return leftOverItems;
            }

            /* if(itemSlot[i].isFull == false && itemSlot[i].nameInInventory == items_SOs[i].name)
            {
                int leftOverItems = itemSlot[i].AddItem(itemQuantiy, itemName, itemSprite, itemPrefab, itemNameDull, itemSpriteDull, itemPrefabDull);
            } */
        }
        return itemQuantiy; 
    }


    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            //Check every Slot in the array and turn off the component
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
}
