using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCoilSpring : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float movementSmoothing;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    private bool isGrounded;
    private bool isCharging;
    private float previousJumpForce;
    private Vector2 velocity = Vector2.zero;
    private bool isFacingRight;

    void Start()
    {
        isFacingRight = true;

        isGrounded = false;
        isCharging = false;
        previousJumpForce = 0f;
    }

    void Update()
    {
        CheckGrounded();

        if(!isGrounded) //only when in air, the player can move horizontally
            Walk();
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
        isCharging = false;
        animator.SetBool("IsJumping", false); //disable jumping animation with Animator variable "isJumping"
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.2f);
        ChargeJump(); //next jump
    }

    //auto jump or charging process
    private void ChargeJump()
    {
        if (Input.GetAxis("Jump") > 0 && !isCharging)
        {
            movementSmoothing = 0.7f; //slower horizontal movement at charged jump
            StartCoroutine(Charging());
        }
        else if (Input.GetAxis("Jump") <= 0 && !isCharging)
        {
            movementSmoothing = 0.15f;
            Jump(jumpForce);
        }
    }

    //jump movement
    private void Jump(float jumpForce)
    {
        if (previousJumpForce > jumpForce) //smooth vertical movement
            jumpForce = jumpForce * 0.3f + previousJumpForce * 0.7f;
        previousJumpForce = jumpForce;

        rb.velocity = new Vector2(0f, jumpForce);
        animator.SetBool("IsJumping", true); //enable jumping animation with Animator variable "isJumping"
    }

    //charrging process for charged jump
    private IEnumerator Charging()
    {
        isCharging = true;
        animator.SetBool("IsCharging", true); //enable charging animation with Animator variable "IsCharging"

        float multiplier = 1f;
        while (Input.GetAxis("Jump") > 0)
        {
            multiplier += 0.2f;
            yield return new WaitForSeconds(0.1f);
        }
        multiplier = Mathf.Min(multiplier, 3.5f);
        animator.SetBool("IsCharging", false); //disable charging animation with Animator variable "IsCharging"
        Jump(jumpForce * multiplier);
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
    }

    //change facing direction to movement direction
    private void Flip(int multiplier)
    {
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + multiplier * 180, transform.rotation.eulerAngles.z);
        isFacingRight = !isFacingRight;
    }
}
