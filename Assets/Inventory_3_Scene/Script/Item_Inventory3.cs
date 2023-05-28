using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Inventory3 : MonoBehaviour
{
    [SerializeField] Items_SO items_SO;

    private string itemName;
    private Sprite itemSprite;
    private int itemQuantiy;
    private GameObject itemPrefab;
    private string itemNameDull;
    private Sprite itemSpriteDull;
    private GameObject itemPrefabDull;

    private InventoryManager inventoryManager;

    private void Awake() 
    {
        //Get Data Reference and set it to the empty items
        itemName = items_SO.itemName;
        itemSprite = items_SO.itemSprite;
        itemNameDull = items_SO.itemNameDull;
        itemSpriteDull= items_SO.itemSpriteDull;
        itemQuantiy = items_SO.itemQuantiy;
        itemPrefab = items_SO.itemPrefab;
        itemPrefabDull = items_SO.itemPrefabDull;
    }

    // Start is called before the first frame update
    private void Start()
    {
        inventoryManager = GameObject.Find("GameCanvas").GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        //Check if the tag is the Player
        if(other.tag == "Player")
        {
            //Add the items referenced in the awake to the manager
            int leftOverItems = inventoryManager.AddItem(itemQuantiy, itemName, itemSprite, itemPrefab, itemNameDull, itemSpriteDull, itemPrefabDull);

            if(leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                itemQuantiy = leftOverItems;
            }
        }
    }
}
