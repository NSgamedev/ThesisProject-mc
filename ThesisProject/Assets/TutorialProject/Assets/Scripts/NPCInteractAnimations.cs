using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class NPCInteractAnimations : MonoBehaviour, F_IInteractable
{
    [SerializeField] private Animator animator;
    [SerializeField] private int reactionsCount = 3;
 

    private int lastIndex = 0; // stores last played index in 1..reactionsCount


   
    public void Interact()
    {
        int index = 0;
        index = Random.Range(0, reactionsCount);


        animator.SetInteger("RandomIndex", index);
        animator.SetTrigger("Interact");
    }
}
