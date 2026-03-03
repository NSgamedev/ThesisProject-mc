using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public int mouseSensitivity;
    public Transform playerCamera;
    public float xRotation;
    public float yRotation;
    public float mouseX;
    public float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Hide the cursor
    }

    private void OnLook(InputValue value)
    {
        mouseX = value.Get<Vector2>().x; // Get horizontal mouse movement
        mouseY = value.Get<Vector2>().y; // Get vertical mouse movement
    }

    // Update is called once per frame
    void Update()
    {
        // Scaling mouse movement by deltaTime and sensitivity
        mouseX *= Time.deltaTime * mouseSensitivity;
        mouseY *= Time.deltaTime * mouseSensitivity;

        // Adjusting yRotation based on mouseX movement
        yRotation += mouseX;

        // Adjusting xRotation based on mouseY movement, and clamping it within a range
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -35f, 40f);

        // Applying rotation to the player object (for left and right rotation)
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

    }
}
