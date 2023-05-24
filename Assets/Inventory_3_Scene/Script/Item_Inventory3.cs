using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Inventory3 : MonoBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] public int quantity;
    [SerializeField] public Sprite itemSprite;
    private InventoryManager inventoryManager;
    // Start is called before the first frame update
    private void Start()
    {
        inventoryManager = GameObject.Find("GameCanvas").GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, itemSprite);

            if(leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                quantity = leftOverItems;
            }
        }
    }
}
