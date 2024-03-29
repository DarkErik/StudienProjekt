using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// [Singelton ("instance")]
/// The script, which controls the player movement
/// </summary>
public class PlayerMovementDefault : PlayerMovement
{
	/// <summary>
	/// Singelton
	/// </summary>
	public static PlayerMovementDefault instance;

	public float moveSpeed = 10f;
	public float jumpForce = 8f;
	/// <summary>
	/// The wall down sliding speed
	/// </summary>
	public float wallSlidingSpeed = 5f;
	/// <summary>
	/// The force, which will be applied midair, when a movement input is set
	/// </summary>
	public float moveForceInAir = 1000;

	/// <summary>
	/// Velocity of a Dash
	/// </summary>
	public float dashVelocity = 100f;

	/// <summary>
	/// The time the Dash will need to get from start to end point
	/// </summary>
	public float dashDuration = 0.25f;

	public float dashEyeCandyPerSecond = 4;
	public Color dashEyeCandyColor;
	public float dashEyeCandyLifeTime = 0.2f;
	public Transform dashEyeCandyCopyBase;


	/// <summary>
	/// The airspeed cap
	/// </summary>
	public Vector2 maxAirSpeed = new Vector2(6, 8);
	/// <summary>
	/// multiplier, when you turn midair
	/// </summary>
	public float inAirTurnAroundMultiplier = 1.5f;
	public float airDragMultiplicator = 0.1f;
	/// <summary>
	/// How much your Jumpvelocity (y) is reduced, when you release the Jump key, before the pivot of the jump
	/// </summary>
	[Range(0, 1)]
	public float jumpHeightCutOffMultiplier = 0.5f;
	/// <summary>
	/// The force of a Walljump (in the wallJumpDir)
	/// </summary>
	public float wallJumpForce = 500f;


	/// <summary>
	/// The amount of time, your input will be blocked after a walljump
	/// </summary>
	public float controlBlockDelayWallJump = 0.2f;

	public float groundCheckRadius = 0.5f;
	public float wallCheckDistance = 0.5f;
	public int amountOfJumps = 1;
	public LayerMask whatIsGround;
	/// <summary>
	/// This Transform, will be fliped, whenever a you switch ur looking-direction
	/// </summary>
	public Transform directionFlipTransform;

	/// <summary>
	/// The Direction of the walljump (assuming you look to the right handside)
	/// </summary>
	public Vector2 wallJumpDir;

	public AudioSource jumpAudio;
	public AudioSource duckAudio;
	public AudioSource wallSlideAudio;

	private int amountOfJumpsLeft;

	private bool facingRight = true;
	private bool jumpPressed = false, jumpNowReleased = false, dashPressed = false;
	private bool isWalking = false;
	private bool isLookingUp = false;
	private bool isDucking = false;
	private bool isGrounded = false, canJump = false;
	private bool isFalling = false;
	private bool isTouchingWall = false;
	private bool isWallSliding = false;
	private bool isDashing = false;

	private float horizontalInput = 0;
	private float verticalInput = 0;
	private float moveBlockDelay = 0;

	public Transform groundCheck;
	public Transform wallCheck;

	private Rigidbody2D body;

	[SerializeField] Animator animator;

	private float dashEndTime = 0f;
	private float nextDashEyeCandy = 0f;
	private bool canDash = true;
	private Vector3 normalizedDashDirection = default;
	private float gravityValueStorageDuringDash;

	[SerializeField] private ParticleSystem particleSystem;

	private string jumpButton = "Jump";
	private string dashButton = "Dash";
	private string horizontalMovementAxis = "Horizontal";
	private string verticalMovementAxis = "Vertical";

	public bool dashUnlocked = false;

	[SerializeField] private SpriteRenderer sr;


	protected virtual void Awake()
	{
		base.Awake();

		instance = this;
		body = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		amountOfJumpsLeft = amountOfJumps;
		wallJumpDir.Normalize();
		if (wallJumpDir.x > 0) wallJumpDir.x = -wallJumpDir.x;

		String color = PlayerPrefs.GetString("playerColor", "#FFFFFF");
		Color newColor;
		ColorUtility.TryParseHtmlString(color, out newColor);
		sr.color = newColor;

	}

	// Update is called once per frame
	void Update()
	{
		GetInputs();
		CheckMovementDir();
		UpdateAnimations();
		CheckCanJump();
		CheckIfWallSliding();
		UpdatePartikleSystem();
		//BUGGED_RESET();
	}

	private void BUGGED_RESET()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || transform.position.z < -50)
			RespawnPoint.Respawn();
	}

	public void FixedUpdate()
	{
		if (!isDashing)
		{
			Move();
			UpdateJumping();
		}
		HandleDashing();
		CheckSurroundings();
		ResetButtons();
	}

	private void HandleDashing()
	{
		if (dashPressed && !isDashing && canDash && dashUnlocked)
		{ // Start Dash
			dashEndTime = Time.time + dashDuration;
			isDashing = true;
			canDash = false;
			gravityValueStorageDuringDash = body.gravityScale;
			body.gravityScale = 0f;

			normalizedDashDirection = new Vector3(Input.GetAxisRaw(horizontalMovementAxis), Input.GetAxisRaw(verticalMovementAxis));
			if (normalizedDashDirection == Vector3.zero)
			{
				normalizedDashDirection = facingRight ? Vector3.right : Vector3.left;
			}
			else
			{
				normalizedDashDirection.Normalize();
			}


		}

		if (isDashing && nextDashEyeCandy <= Time.time)
		{
			GameObject eyeCandy = Instantiate(dashEyeCandyCopyBase.gameObject, dashEyeCandyCopyBase.position, dashEyeCandyCopyBase.rotation);
			eyeCandy.transform.localScale = dashEyeCandyCopyBase.lossyScale;
			foreach (SpriteRenderer renderer in eyeCandy.GetComponentsInChildren<SpriteRenderer>())
			{
				renderer.color = dashEyeCandyColor;
				renderer.sortingOrder -= 3;
			}
			Destroy(eyeCandy, dashEyeCandyLifeTime);
			nextDashEyeCandy = Time.time + (1 / dashEyeCandyPerSecond);

		}

		if (isDashing && Time.time >= dashEndTime)
		{ // End dash
			body.gravityScale = gravityValueStorageDuringDash;
			body.velocity = Vector2.zero;
			isDashing = false;
		}

		if (isDashing)
		{
			body.velocity = normalizedDashDirection * (Time.fixedDeltaTime * dashVelocity);
		}
	}

	/// <summary>
	/// Resets the intern Buttons
	/// </summary>
	private void ResetButtons()
	{
		jumpPressed = false;
		jumpNowReleased = false;
		dashPressed = false;
	}

	/// <summary>
	/// Fetches the Inputs from the input-system
	/// </summary>
	private void GetInputs()
	{
		horizontalInput = Input.GetAxis(horizontalMovementAxis);
		verticalInput = Input.GetAxis(verticalMovementAxis);
		if (!jumpPressed)
			jumpPressed = Input.GetButtonDown(jumpButton);

		if (!jumpNowReleased)
			jumpNowReleased = Input.GetButtonUp(jumpButton);

		if (dashUnlocked && !dashPressed)
			dashPressed = Input.GetButtonDown(dashButton);
	}

	/// <summary>
	/// Handles ground/wall/air movement
	/// </summary>
	private void Move()
	{


		if (moveBlockDelay > 0) moveBlockDelay -= Time.deltaTime;

		if (isGrounded)
		{
			body.velocity = new Vector2(moveSpeed * horizontalInput * Time.deltaTime, body.velocity.y);
			
			//SOUND if (Mathf.Abs(horizontalInput) > 0.3) SoundManager.PlaySound(Sound.PLAYER_FOOTSTEPS, true);
		}
		else
		{
			if (body.velocity.x > 0 == facingRight)
			{ //Input in LookingDir
				if (moveBlockDelay <= 0 && horizontalInput != 0 && Mathf.Abs(body.velocity.x) <= maxAirSpeed.x)
				{
					body.AddForce(new Vector2(moveForceInAir * horizontalInput * Time.deltaTime, 0), ForceMode2D.Force);
					CapAirSpeedX();
				}
			}
			else
			{ //Input against LookingDir
				body.AddForce(new Vector2(moveForceInAir * horizontalInput * inAirTurnAroundMultiplier * Time.deltaTime, 0), ForceMode2D.Force);
			}

			if (horizontalInput == 0)
			{
				body.velocity = new Vector2(body.velocity.x * Mathf.Max(1 - airDragMultiplicator * Time.deltaTime, 0), body.velocity.y);
			}
			CapAirSpeedY();
		}

		if (isWallSliding)
		{
			if (body.velocity.y < -wallSlidingSpeed) body.velocity = new Vector2(body.velocity.x, -wallSlidingSpeed);
		}
	}

	private void CapAirSpeedX()
	{
		if (Mathf.Abs(body.velocity.x) > maxAirSpeed.x)
		{
			body.velocity = new Vector2((facingRight ? 1 : -1) * maxAirSpeed.x, body.velocity.y);
		}
	}

	private void CapAirSpeedY()
	{
		if (Mathf.Abs(body.velocity.y) > maxAirSpeed.y)
			body.velocity = new Vector2(body.velocity.x, (body.velocity.y > 0 ? 1 : -1) * maxAirSpeed.y);
	}

	private void CheckSurroundings()
	{
		bool wasLastFrameGrounded = isGrounded;
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
		//SOUND if (!wasLastFrameGrounded && isGrounded) SoundManager.PlaySound(Sound.PLAYER_LAND, true);

		isTouchingWall = Physics2D.Raycast(wallCheck.position, new Vector2(facingRight ? 1 : -1, 0), wallCheckDistance, whatIsGround);
	}

	private void CheckMovementDir()
	{
		if (facingRight && horizontalInput < 0)
		{
			Flip();
		}
		else if (!facingRight && horizontalInput > 0)
		{
			Flip();
		}

		if (Mathf.Abs(horizontalInput) > 0.3f)
		{
			isWalking = true;
			isDucking = false;
			isLookingUp = false;
		}
		else
		{
			bool prevDucking = isDucking;
			isWalking = false;
			isLookingUp = false;
			isDucking = false;

			if (verticalInput > 0)
			{
				isLookingUp = true;
			}
			else if (verticalInput < 0)
			{
				isDucking = true;
				if (!prevDucking)
					duckAudio.Play();
			}
		}
	}

	private void UpdateJumping()
	{
		if (jumpPressed && isWallSliding)
		{
			int dir = facingRight ? 1 : -1;
			body.velocity = new Vector2(wallJumpDir.x * wallJumpForce * dir, wallJumpDir.y * wallJumpForce);
			moveBlockDelay = controlBlockDelayWallJump;
			jumpPressed = false;
			//jumpAudio.Stop();
			//jumpAudio.Play();
		}

		if (jumpPressed && canJump)
		{
			body.velocity = new Vector2(body.velocity.x, jumpForce);
			jumpPressed = false;
			amountOfJumpsLeft--;
			jumpAudio.Stop();
			jumpAudio.Play();
		}


		if (jumpNowReleased)
		{
			jumpNowReleased = false;
			if (body.velocity.y > 0) body.velocity = new Vector2(body.velocity.x, body.velocity.y * jumpHeightCutOffMultiplier);
		}


		if(!isGrounded && body.velocity.y < 0)
        {
			isFalling = true;
        }
        else 
		{ 
			isFalling = false;
		}
	}

	private void UpdateAnimations()
	{
		animator.SetBool("isWalking", isWalking);
		animator.SetBool("isGrounded", isGrounded);
		animator.SetBool("isWallSliding", isWallSliding);
		animator.SetBool("isLookingUp", isLookingUp);
		animator.SetBool("isDucking", isDucking);
		animator.SetBool("isFalling", isFalling);
	}

	private void Flip()
	{
		facingRight = !facingRight;
		directionFlipTransform.rotation = Quaternion.Euler(0, (directionFlipTransform.eulerAngles.y + 180) % 360, 0);
	}



	private void UpdatePartikleSystem()
	{
		ParticleSystem.EmissionModule emission = particleSystem.emission;

		if (isWallSliding || (isGrounded && isWalking))
		{
			if (!emission.enabled) emission.enabled = true;
			//Debug.Log(particleSystem.emission.enabled);
		}
		else
		{
			//Debug.Log(particleSystem.emission.enabled);
			if (emission.enabled) emission.enabled = false;
		}

	}


	private void CheckIfWallSliding()
	{
		bool prevWallSliding = isWallSliding;
		isWallSliding = !isGrounded && isTouchingWall && body.velocity.y <= 0 && (horizontalInput > 0 == facingRight);

		if (!prevWallSliding && isWallSliding)
			wallSlideAudio.Play();
		if (!isWallSliding)
			wallSlideAudio.Stop();
	}

	private void CheckCanJump()
	{
		if (isGrounded && Mathf.Abs(body.velocity.y) <= 0.1f)
		{
			amountOfJumpsLeft = amountOfJumps;
			canJump = true;
			canDash = true;
		} else
        {
			amountOfJumpsLeft = 0; //TODO fix this someday
        }

		canJump = amountOfJumpsLeft > 0;
		
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

		Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, 0));
	}

	/// <summary>
	/// Shuts down the behaivour, until it will be woken up (WakeUp)
	/// </summary>
	public static new void Shutdown()
	{
		_Shutdown();
	}

	private static void _Shutdown()
	{
		if (instance == null) return;
		instance.enabled = false;
		instance.body.velocity = new Vector2(0, instance.body.velocity.y);

	}

	/// <summary>
	/// Reactivates the behaivour after a call of Shutdown
	/// </summary>
	public static void WakeUp()
	{
		if (instance == null) return;
		instance.enabled = true;
	}

	/// <summary>
	/// Shuts down the Player for a certain time (after that reactivating it automaticly)
	/// </summary>
	/// <param name="time">The sleeping time</param>
	public static void Shutdown(float time)
	{
		instance.StartCoroutine(EnablePlayerControllerAfterTime(time));
		_Shutdown();
	}


	private static IEnumerator EnablePlayerControllerAfterTime(float time)
	{
		yield return new WaitForSeconds(time);
		instance.enabled = true;
	}

	/// <summary>
	/// Return whether the player is standing on the ground currently
	/// </summary>
	/// <returns>True, when the player is standing on the ground, otherwise false</returns>
	public bool IsGrounded()
	{
		return isGrounded;
	}

}


