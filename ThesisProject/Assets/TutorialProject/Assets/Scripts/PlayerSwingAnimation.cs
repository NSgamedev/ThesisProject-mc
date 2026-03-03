using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwingAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeReference] Animator playerAnimator;
    [SerializeReference] PlayerMovement playerMoveScript;

    [SerializeField] private string attackTriggerName = "isSwinging"; // Name of the trigger parameter in the Animator

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerMoveScript = GetComponent<PlayerMovement>();
    }

    public void OnAttack(InputValue input)
    {
        bool isCurrentlyAttacking = playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Swing"); // Check if the current animation state is the attack state  
        if (!isCurrentlyAttacking && !playerMoveScript.IsMoving && input.isPressed)
        playerAnimator.SetTrigger("isSwinging");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
