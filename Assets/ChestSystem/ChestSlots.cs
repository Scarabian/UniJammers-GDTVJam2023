using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class ChestSlots : MonoBehaviour, IPointerClickHandler
{
    [Header("Item Inventory Data")]
    //Light Data
    private int quantityOfInventory = 0;
    public string nameInInventory;
    public Sprite spriteInInventory;
    public GameObject dropItemToSpawn;

    [Header("Item Data")]
    //Light Data
    public int quantity;
    public string itemNameD;
    public Sprite itemSpriteD;
    public GameObject itemPrefabD;
    public string itemNameDullD;
    public Sprite itemSpriteDullD;
    public GameObject itemPrefabDullD;
    public bool isFull;

    [Header("Item Slots")]
    [SerializeField] private TMP_Text quantityText;

    [SerializeField] private Image itemImage;
    public Color itemColor = Color.white;

    [SerializeField] private Sprite emptySprite;

    [Header("Selected Slot")]
    public GameObject selectedShader;
    public bool thisItemSelected;

    [Header("Reference")]
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private RenderTextureSwitcher dimensionTransition;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("GameCanvas").GetComponent<InventoryManager>();
    }

    private void Update()
    {
        inventorySwitch();
        DisplayItems();

        if (this.quantity <= 0)
        {
            EmptySlot();
        }
    }
    public void DisplayItems()
    {

        //Update and show Data in UI

        this.nameInInventory = itemNameD;
        this.spriteInInventory = itemSpriteD;
        itemImage.sprite = itemSpriteD;
        itemColor.a = 1.0f;
        itemImage.GetComponent<Image>().color = itemColor;

        this.quantityOfInventory = quantity;
        quantityText.text = this.quantityOfInventory.ToString("n0");
        isFull = true;
    }

    public void inventorySwitch()
    {  
        //Switch the Display items 
        if (!dimensionTransition.isLightMode)
        {
            this.nameInInventory = itemNameDullD;
            this.spriteInInventory = itemSpriteDullD;
            this.itemImage.sprite = itemSpriteDullD;
            this.dropItemToSpawn = itemPrefabDullD;
        }
        else
        {
            this.nameInInventory = itemNameD;
            this.spriteInInventory = itemSpriteD;
            this.itemImage.sprite = itemSpriteD;
            this.dropItemToSpawn = itemPrefabD;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Select the Slot
            OnLeftClick();
        }

    }

    private void OnLeftClick()
    {
        if (thisItemSelected && this.quantityOfInventory > 0)
        {
            //Add items in Player Inventory
            inventoryManager.AddItem(1, itemNameD, itemSpriteD, itemPrefabD, itemNameDullD, itemSpriteDullD, itemPrefabDullD);

            //Decrease Quantity in Chest every left click
            this.quantity -= 1;
            quantityText.text = this.quantity.ToString();
            if (this.quantity <= 0)
            {
                EmptySlot();
            }
        }
        else
        {
            // this.selectedShader.SetActive(false);
            // this.thisItemSelected = false;
            selectedShader.SetActive(true);
            thisItemSelected = true;
        }
    }


    private void EmptySlot()
    {
        //Clear the data in the slot
        Destroy(gameObject);
    }
}
