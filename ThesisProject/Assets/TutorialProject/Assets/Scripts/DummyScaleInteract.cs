using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DummyScaleInteract : MonoBehaviour, F_IInteractable
{
    [SerializeReference] Animator animator;
    private float scale = 1f;
    private bool isBig = false;

    private float smallScale = 1f;
    private float bigScale = 50f;
    // Start is called before the first frame update

    public void OnInteract(InputValue value)
    {

        if (!value.isPressed) return;


        if(isBig && value.isPressed)
        {
            animator.SetFloat("scale", smallScale);
        }
        else if (!isBig && value.isPressed)
        {
            animator.SetFloat("scale", bigScale);
        }
        Interact();
    }

    public void Interact()
    {    
        scale = isBig ? bigScale : smallScale;
        animator.SetFloat("scale", scale);
        animator.SetTrigger("Interact");
        isBig = !isBig;
    }

}
