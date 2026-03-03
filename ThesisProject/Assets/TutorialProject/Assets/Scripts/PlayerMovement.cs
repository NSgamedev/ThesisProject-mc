using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public Rigidbody characterRb;
    public Vector3 movementInput;
    public Vector3 movementVector;
    [SerializeField] float movementSpeed;

    // Jump-specific variables:
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float distToGround = 0.1f;
    [SerializeField] LayerMask groundMask;
    [SerializeReference] Animator playerAnimator;
    [SerializeField] bool isMoving;
    public bool IsMoving { get { return isMoving; } } // Public property to access the isMoving variable from other scripts


    // Start is called before the first frame update
    void Start()
    {
        characterRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    private void OnMovement(InputValue input)
    {
        Vector2 inputValue = input.Get<Vector2>();
        movementInput = new Vector3(inputValue.x, 0f, inputValue.y); // Convert the 2D input to a 3D movement vector

        // Update the isMoving variable based on whether there is any movement input
        isMoving = movementInput != Vector3.zero; // Set isMoving to true if there is movement input, otherwise set it to false
        playerAnimator.SetBool("isMoving", isMoving); // Set the "isMoving" parameter in the Animator based on whether there is movement input

    }

    private void ApplyMovement()
    {
        if(movementInput != Vector3.zero)
        {
            movementVector = (movementInput.x * transform.right) + (movementInput.z * transform.forward); // Calculate the movement vector based on the input and the player's orientation

            movementVector.y = 0f;

            characterRb.velocity = movementVector * Time.fixedDeltaTime * movementSpeed; // Apply the movement vector to the player's Rigidbody
        }
    }

    private bool IsGrounded()
    {
        // Check if the player is grounded by performing a raycast downwards from the player's position
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f, groundMask);
    }


    // Update is called once per frame
    void Update()
    {
        ApplyMovement();

        // Jumping logic:
        if (Keyboard.current.spaceKey.wasPressedThisFrame && IsGrounded())
        {
            characterRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply an upward force to the player's Rigidbody to make it jump
        }
    }

    private void OnMovementStop(InputValue input)
    {
        movementVector = Vector3.zero; // Stop the player's movement when the input is released    
        characterRb.velocity = Vector3.zero; // Set the Rigidbody's velocity to zero to stop the player immediately 

        // Update the isMoving variable to false when the movement input is released
        playerAnimator.SetBool("isMoving", false); // Set the "isMoving" parameter in the Animator to false when the movement input is released
    }
}
