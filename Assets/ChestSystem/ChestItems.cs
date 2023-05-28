using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChestItems : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject chest;

    private bool isMenuActivated;

    public bool Interact(Interactor interactor)
    {
        //Makes the chest interactable with the Player
        InteractWithChest();
        return true;
    }

    private void Start()
    {
        isMenuActivated = false;
    }
    private void Update() 
    {
        //If chest UI is deleted, delete this gameobject too
        if(chest == null)
        {
            Destroy(gameObject);
        }
    }

    public void InteractWithChest()
    {
        if (isMenuActivated)
        {
            //Disable Chest UI
            chest.SetActive(false);
            isMenuActivated = false;
        }
        else if (!isMenuActivated)
        {
            //Enable Chest UI
            chest.SetActive(true);
            isMenuActivated = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "IteractablePoint")
        {
            //Disable Chest ui when trigger exits
            chest.SetActive(false);
            isMenuActivated = false;
        }
    }
}
