using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickFlashlight : MonoBehaviour
{
    public bool pickUp = false;
    public bool putDown = false;
    [SerializeField] private Animator animator;
    private void Update()
    {
        if (putDown)
        {
            animator.SetBool("PutDown", true);
            putDown = false;
            animator.SetBool("Pick", false);
        }
    }


    //if the flashlight is on, pick it up when scientist is touching it, then set scientist as a parent
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pickUp)
        {
            if(collision.tag == "PowerUp")
            {
                animator.SetBool("Pick", true);
            }
            animator.SetBool("PutDown", false);
            pickUp = false;
        }
    }
}
