using System.Collections;
using System.Collections.Generic;
using uj.input;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputReader inputReader;
    CharacterController charController;
    private bool isFirstPlay = false;

    Vector2 moveInput;

    [SerializeField] float moveSpeed = 5;
    [SerializeField] private float playerMaxHealth = 10.0f;
    [SerializeField] public float playerCurrentHealth;
    [SerializeField] private float playerMaxLight= 10.0f;
    [SerializeField] public float playerCurrentLight;

    private void Start()
    {
        inputReader = FindObjectOfType<InputReader>();
        charController = GetComponent<CharacterController>();
        playerCurrentHealth = playerMaxHealth;
        playerCurrentLight = playerMaxLight; // The light bar is manupulated in the Dimension Transition Script
    }

    private void Update()
    {
        moveInput = inputReader.GetMoveInput();
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);

        charController.Move(moveSpeed * Time.deltaTime * movement);
    }

    public void TakeDamage(float damage)
    {
        //Deals Damage to Player
        playerCurrentHealth -= damage;
    }



    
}
