using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class ItemSlots : MonoBehaviour, IPointerClickHandler
{
    [Header("Item Inventory Data")]
    //Light Data
    public int quantityOfInventory = 0;
    public string nameInInventory;
    public Sprite spriteInInventory;
    public GameObject dropItemToSpawn;

    [Header("Item Data")]
    //Light Data
    public string itemNameD;
    public Sprite itemSpriteD;
    public GameObject itemPrefabD;
    public string itemNameDullD;
    public Sprite itemSpriteDullD;
    public GameObject itemPrefabDullD;

    public bool isFull;
    [SerializeField] public int maxNumberOfItem;

    [Header("Item Slots")]
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    public Color itemColor = Color.white;
    [SerializeField] private Sprite emptySprite;

    [Header("Selected Slot")]
    public GameObject selectedShader;
    public bool thisItemSelected;

    [Header("Instantiate")]
    [SerializeField] private Vector3 prefabOffset = new Vector3(0, 2.0f, 3.0f);

    private InventoryManager inventoryManager;
    [SerializeField] private RenderTextureSwitcher dimensionTransition;

    private void Start()
    {
        inventoryManager = GameObject.Find("GameCanvas").GetComponent<InventoryManager>();
        dimensionTransition = GameObject.Find("Screen Manage").GetComponent<RenderTextureSwitcher>();
    }

    private void Update()
    {
        //Switch ui based on dimension the player is in
        inventorySwitch();

        if (thisItemSelected && Input.GetKeyDown(KeyCode.Space))
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(false);
            thisItemSelected = false;
        }
    }

     public int AddItem(int itemQuantiy, string itemName, Sprite itemSprite, GameObject itemPrefab, string itemNameDull, Sprite itemSpriteDull, GameObject itemPrefabDull)
    {
        //Checker if slot if full
        if(isFull)
        {
            return quantityOfInventory;
        }

        //Set Data Reference
        itemNameD = itemName;
        itemSpriteD = itemSprite;
        itemNameDullD = itemNameDull;
        itemPrefabD = itemPrefab;
        itemSpriteDullD = itemSpriteDull;
        itemPrefabDullD = itemPrefabDull;

        //Update and show Data in UI
        this.nameInInventory = itemName;
        this.spriteInInventory = itemSprite;
        itemImage.sprite = itemSprite;
        itemColor.a = 1.0f;
        itemImage.GetComponent<Image>().color = itemColor;
        
        this.quantityOfInventory += itemQuantiy;
        if(this.quantityOfInventory >= maxNumberOfItem)
        {
            quantityText.text = maxNumberOfItem.ToString("n0");
            isFull = true;

            int extraItems = this.quantityOfInventory - maxNumberOfItem;
            this.quantityOfInventory = maxNumberOfItem;
            return extraItems;
        }

        quantityText.text = this.quantityOfInventory.ToString("n0");

        return 0;
    }

    public void inventorySwitch()
    {
        for (int i = 0; i < inventoryManager.itemSlot.Length; i++)
        {
            if(!dimensionTransition.isLightMode)
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
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Select the Slot
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Check if an item is selected
            //Drops Item
            if (thisItemSelected)
                OnRightClick();
        }
    }

    private void OnLeftClick()
    {
        if (thisItemSelected && this.quantityOfInventory > 0)
        {
            inventoryManager.UseItem(nameInInventory);
            this.quantityOfInventory -= 1;
            quantityText.text = this.quantityOfInventory.ToString();

            if (this.quantityOfInventory <= 0)
            {
                EmptySlot();
            }
        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
        }
    }

    public void OnRightClick()
    {
        if (this.quantityOfInventory > 0)
        {
            //Create a new object to drop
            switch (nameInInventory)
            {
                case "Lantern":
                    CreateNewItem(itemPrefabD);
                    break;
                case "Flashlight":
                    CreateNewItem(itemPrefabD);
                    break;
                case "Ligther":
                    CreateNewItem(itemPrefabD);
                    break;
                case "Staff":
                    CreateNewItem(itemPrefabDullD);
                    break;
                case "Wand":
                    CreateNewItem(itemPrefabDullD);
                    break;
                case "Scepter":
                    CreateNewItem(itemPrefabDullD);
                    break;
                default:
                    return;
            }

            //Update the data
            this.quantityOfInventory -= 1;
            quantityText.text = this.quantityOfInventory.ToString();
            isFull = false;

            if (quantityOfInventory <= 0)
            {
                EmptySlot();
            }
        }

    }

    private void CreateNewItem(GameObject _itemTodrop)
    {
        _itemTodrop.transform.position = GameObject.FindWithTag("Player").transform.position + prefabOffset;
        Instantiate(_itemTodrop, _itemTodrop.transform.position, Quaternion.identity, GameObject.Find("ItemHolder").transform);
    }

    private void EmptySlot()
    {
        //Clear the data in the slot
        nameInInventory = null;
        quantityOfInventory = 0;
        spriteInInventory = null;
        quantityText.text = "";
        dropItemToSpawn = null;
        itemColor.a = 0f;
        isFull = false;
        itemImage.GetComponent<Image>().color = itemColor;
        itemImage.sprite = emptySprite;

        itemNameD = null;
        itemSpriteD = null;
        itemPrefabD = null;
        itemNameDullD = null;
        itemSpriteDullD= null;
        itemPrefabDullD = null;   
    }
}
    

