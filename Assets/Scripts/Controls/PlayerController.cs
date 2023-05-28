using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    RenderTextureSwitcher renderTextureSwitcher;
   // private CharacterController characterController;
    
    public float firstPersonSpeed = 3f;
    public float firstPersonRotationSpeed = 10f;
    private bool isDull;

    private Vector2 moveInput;
    private Vector2 rotationInput;
    private bool isJumping;
    private Vector3 velocity;

    private Vector3 playerVelocity;
    private bool grounded;
    public float gravity = 9.8f;
    public float jumpForce = 5f;




    //public Camera cam;
    private Vector2 lookPos;
    private float xRotation = 0f;
    public float xsens = 30f;
    public float ysens = 30f;

    public float rotationSpeed = .15f;
    public float speed = 5f;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        JumpFP();

    }
    public void OnLook(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>();

    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        //e key has been pressed


    }

    // Start is called before the first frame update
    void Start()
    {
        //characterController = GetComponent<CharacterController>();

        renderTextureSwitcher = FindObjectOfType<RenderTextureSwitcher>();
       

    }

    // Update is called once per frame
    void Update()
    {

        if (isDull != renderTextureSwitcher.getIsDull())
        {
            isDull = !isDull;
            if (isDull)
            {
                Cursor.lockState = CursorLockMode.Locked;
            } else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        } 

       // grounded = characterController.isGrounded;
        if (isDull)
        {
            
            MovePlayerFP();
            LookFP();

            // this means that the first person controller should be avtivated
        } else
        {
            
            movePlayer();

        }

        
    }

    public void movePlayer()
    {
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);

        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(movement),rotationSpeed);

        transform.Translate(movement * speed * Time.deltaTime, Space.World );
    }
    public void MovePlayerFP()
    {

        // Get movement input
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection.y = 0f;
        moveDirection.Normalize();

        // Apply movement
        transform.Translate(moveDirection * firstPersonSpeed * Time.deltaTime, Space.World);

       
        /*transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed);
         Vector3 moveDirection = Vector3.zero;
         moveDirection.x = moveInput.x;
         moveDirection.z = moveInput.y;
         characterController.Move(transform.TransformDirection(moveDirection) * firstPersonSpeed * Time.deltaTime);

         playerVelocity.y += gravity * Time.deltaTime;
         if(grounded && playerVelocity.y > 0f) 
         {
             playerVelocity.y = -2f;
         }
         characterController.Move(playerVelocity * Time.deltaTime); */
    }

    public void JumpFP()
    {
         
        /*if(grounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -3 * gravity);
        }*/
    }
    public void LookFP()
    {
        float rotationY = rotationInput.x * firstPersonRotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotationY, 0f);


        //xRotation =(lookPos.y * Time.deltaTime) * ysens;
        //xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        //cam.transform.localRotation = Quaternion.Euler(xRotation,0,0);
        //transform.Rotate(Vector3.up * (lookPos * Time.deltaTime) * xsens);
    }

}
