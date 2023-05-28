using System.Collections;
using System.Collections.Generic;
using uj.input;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputReader inputReader;
    CharacterController characterController;
    RenderTextureSwitcher renderTextureSwitcher;

    private bool isJumping;
    private Vector2 movementInput;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 5f;

    
    [SerializeField] private float playerMaxHealth = 10.0f;
    [SerializeField] public float playerCurrentHealth;
    [SerializeField] private float playerMaxLight= 10.0f;
    [SerializeField] public float playerCurrentLight;

    private void Start()
    {

        inputReader = FindObjectOfType<InputReader>();
        characterController = GetComponent<CharacterController>();
        renderTextureSwitcher = FindObjectOfType<RenderTextureSwitcher>();
        playerCurrentHealth = playerMaxHealth;
        //***********************************************2 b moved
        playerCurrentLight = playerMaxLight; // The light bar is manupulated in the Dimension Transition Script
    }

    private void Update()
    {
        
        
        if (renderTextureSwitcher.getIsDull())
        {
            // this means that the first person controller should be avtivated
        } 
        else
        {
            MovePlayer();
            RotatePlayer();
            CheckJump();

        }        
    }

    public void TakeDamage(float damage)
    {
        //Deals Damage to Player
        playerCurrentHealth -= damage;
    }

    //***********************THIRD PERSON CONTROLLER****************************************************
    private void MovePlayer()
    {
        Vector3 moveDirection = new Vector3(movementInput.x, 0f, movementInput.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        if (movementInput.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(movementInput.x, 0f, movementInput.y));
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void CheckJump()
    {
        if (isJumping)
        {
            characterController.Move(Vector3.up * jumpForce * Time.deltaTime);
            isJumping = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isJumping = true;
        }
    }
    //***********************THIRD PERSON CONTROLLER****************************************************
}/*
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 5f;

    private CharacterController characterController;
    private Vector2 movementInput;
    private bool isJumping;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MovePlayer();
        RotatePlayer();
        CheckJump();
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = new Vector3(movementInput.x, 0f, movementInput.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        if (movementInput.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(movementInput.x, 0f, movementInput.y));
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void CheckJump()
    {
        if (isJumping)
        {
            characterController.Move(Vector3.up * jumpForce * Time.deltaTime);
            isJumping = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isJumping = true;
        }
    }
}*/

