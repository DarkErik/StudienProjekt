using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public bool isPatrolling; //enable/disable partolling

    [SerializeField] private float walkSpeed; //change speed
    [SerializeField] private LayerMask groundLayer; //layer of the ground
    [SerializeField] private LayerMask wallLayer; //layer of the wall
    [SerializeField] private Transform groundCheck; //gameobject in front of enemy at foot level
    [SerializeField] private Rigidbody2D rb; //enemy's rigidbody
    [SerializeField] private Collider2D bodyCollider; //enemy's body collider
    [SerializeField] private Animator animator;

    private bool flip;
    private int flipMultiplier;

    void Start()
    {
        isPatrolling = true;
        flipMultiplier = 1;
    }

    void Update()
    {
        //patrol
        if (isPatrolling)
            Partolling();
        else
            animator.SetFloat("Speed", 0f); //change Animator variable "Speed" to disable walking animation
            animator.SetFloat("Speed", 0f); //change Animator variable "Speed" to disable walking animation
    }

    private void FixedUpdate()
    {
        if (isPatrolling)
            flip = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    void Partolling()
    {
        if (flip || bodyCollider.IsTouchingLayers(wallLayer))
            ChangeDirection();

        rb.velocity = new Vector2(walkSpeed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(walkSpeed)); //change Animator variable "Speed" to disable walking animation
    }

    void DeactivatePickUp()
    {
        animator.SetBool("Pick", false);
    }

    void DeactivatePutDown()
    {
        animator.SetBool("Put", false);
    }

    void ChangeDirection()
    {
        isPatrolling = false;
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + flipMultiplier * 180, transform.rotation.eulerAngles.z);
        walkSpeed *= -1;
        flipMultiplier *= -1;
        isPatrolling = true;
        flip = false;
    }

    public void PickUp()
    {
        {
            animator.SetBool("Pick", true); //initiate pickup animation
            isPatrolling = false;
        }
    }
public void PutDown()
    {
        animator.SetBool("Put", true); //initiate putdown animation
        isPatrolling = false;
    }
}
