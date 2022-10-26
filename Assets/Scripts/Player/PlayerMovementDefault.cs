using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementDefault : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float movementSmoothing;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    private bool isGrounded;
    private Vector2 velocity = Vector2.zero;
    private bool isFacingRight;


    void Start()
    {
        isFacingRight = Vector3.Dot(Vector3.right, transform.forward) > 0.9f;

        isGrounded = true;
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        
        Walk();

        if (isGrounded)
            Jump();
    }

    //horizontal movement
    private void Walk()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        
        if (horizontalInput < 0 && isFacingRight)
            Flip(-1);
        if (horizontalInput > 0 && !isFacingRight)
            Flip(1);

        Vector2 targetVelocity = new Vector2(horizontalInput * walkSpeed, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing); //smooth movement
        
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput)); //enable or disable walking animation with Animator variable "Speed"
    }

    //change facing direction to movement direction
    private void Flip(int multiplier)
    {
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + multiplier * 180, transform.rotation.eulerAngles.z);
        isFacingRight = !isFacingRight;
    }

    //jump movement
    private void Jump()
    {
        if (Mathf.Abs(Input.GetAxis("Jump")) > 0)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            animator.SetBool("IsJumping", true); //enable jumping animation with Animator variable "isJumping"
        }
    }

    //check if player is grounded and call landing method
    private void CheckGrounded()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        if (Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer))
        {
            isGrounded = true;
            if (!wasGrounded)
                OnLand();
        }
    }

    //called when landing
    public void OnLand()
    {
        animator.SetBool("IsJumping", false); //disable jumping animation with Animator variable "isJumping"
    }
}
