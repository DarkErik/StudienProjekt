using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementUmbrella : MonoBehaviour
{
    [SerializeField] private float glideSpeedHorizonal;
    [SerializeField] private float glideSpeedVertical;
    [SerializeField] private float jumpForce;
    [SerializeField] private float movementSmoothing;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    private bool isGrounded;
    private bool hasDoubleJump;
    private float defaultGravityScale;
    private Vector2 velocity = Vector2.zero;
    private bool isFacingRight;

    void Start()
    {
        isFacingRight = true;

        isGrounded = false;
        hasDoubleJump = false;
        defaultGravityScale = rb.gravityScale;
    }

    void Update()
    {
        CheckGrounded();

        if (isGrounded || hasDoubleJump)
            Jump();

        if (animator.GetBool("IsJumping"))
            Glide();
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

    //called once when player lands
    public void OnLand()
    {
        hasDoubleJump = true;
        rb.gravityScale = defaultGravityScale;
        animator.SetBool("IsJumping", false); //disable jumping animation with Animator variable "isJumping"
    }

    //horizontal movement
    private void Glide()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 && rb.velocity.y < 0)
            rb.gravityScale = 0f;
        else
            rb.gravityScale = defaultGravityScale;

        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput < 0 && isFacingRight)
            Flip(-1);
        if (horizontalInput > 0 && !isFacingRight)
            Flip(1);

        Vector2 targetVelocity;
        if (rb.gravityScale == 0f)
            targetVelocity = new Vector2(horizontalInput * glideSpeedHorizonal, -glideSpeedVertical);
        else
            targetVelocity = new Vector2(horizontalInput * glideSpeedHorizonal, rb.velocity.y);

         rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing); //smooth movement
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
        if (Input.GetButtonDown("Jump")) {

            if (!isGrounded)
                hasDoubleJump = false;

            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
            animator.SetBool("IsJumping", true); //enable jumping animation with Animator variable "isJumping"
        }
    }
}
