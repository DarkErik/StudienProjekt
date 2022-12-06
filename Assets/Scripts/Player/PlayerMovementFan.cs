using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementFan : MonoBehaviour
{
    enum Mode
    {
        Off,
        Forward,
        Backward
    }

    [SerializeField] private float walkSpeed;
    [SerializeField] private float movementSmoothing;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private AreaEffector2D areaEffector;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject windZone;
    [SerializeField] private AudioSource audioIdle;
    [SerializeField] private AudioSource audioWalk;
    [SerializeField] private AudioSource audioWind;

    private bool isFacingRight;
    private float timer;
    private bool canChangeMode;
    private Vector2 velocity = Vector2.zero;
    private Mode currentMode;
    private float lastSpeed;


    void Start()
    {
        isFacingRight = true;
        canChangeMode = true;
        timer = 0f;
        currentMode = Mode.Off;
        OffMode();
        lastSpeed = 0f;
        audioIdle.Play();
    }

    void Update()
    {
        Walk();

        if (!canChangeMode) //ability can be toggled every 0.5 seconds
            canChangeMode = 0.5f < (timer += Time.deltaTime);
        else
            ChangeMode();
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

        if (Mathf.Abs(horizontalInput) > 0 && lastSpeed <= 0)
        {
            audioIdle.Stop();
            audioWalk.Play();
        }

        if (Mathf.Abs(horizontalInput) <= 0 && lastSpeed > 0)
        {
            audioWalk.Stop();
            audioIdle.Play();
        }

        lastSpeed = Mathf.Abs(horizontalInput);
    }

    //change facing direction to movement direction
    private void Flip(int multiplier)
    {
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + multiplier * 180, transform.rotation.eulerAngles.z);
        isFacingRight = !isFacingRight;

        if (currentMode == Mode.Forward)
        {
            if (isFacingRight)
                areaEffector.forceAngle = 0;
            else
                areaEffector.forceAngle = -180;
        }

        if (currentMode == Mode.Backward)
        {
            if (isFacingRight)
                areaEffector.forceAngle = -180;
            else
                areaEffector.forceAngle = 0;
        }

    }

    private void ChangeMode()
    {
        if (Input.GetAxis("Ability") > 0)
        {
            canChangeMode = false;
            timer = 0;

            int currentModeInt = ((int)currentMode + 1) % 3;
            currentMode = (Mode)currentModeInt;

            switch (currentMode)
            {
                case Mode.Off:
                    OffMode();
                    break;
                case Mode.Forward:
                    ForwardMode();
                    break;
                case Mode.Backward:
                    BackwardMode();
                    break;
                default:
                    break;
            }
        }
    }

    private void OffMode()
    {
        windZone.SetActive(false);
        animator.SetInteger("Mode", 0);
        audioWind.Stop();
    }

    private void ForwardMode()
    {
        windZone.SetActive(true);
        animator.SetInteger("Mode", 1);
        audioWind.Stop();
        audioWind.Play();

        if (isFacingRight)
        {
            areaEffector.forceAngle = 0;
            particles.transform.eulerAngles = new Vector3(particles.transform.rotation.eulerAngles.x, 0, particles.transform.rotation.eulerAngles.z);
        }
        else
        {
            areaEffector.forceAngle = -180;
            particles.transform.eulerAngles = new Vector3(particles.transform.rotation.eulerAngles.x, 180, particles.transform.rotation.eulerAngles.z);
        }
    }

    private void BackwardMode()
    {
        windZone.SetActive(true);
        animator.SetInteger("Mode", 2);
        audioWind.Stop();
        audioWind.Play();

        if (isFacingRight)
        {
            areaEffector.forceAngle = -180;
            particles.transform.eulerAngles = new Vector3(particles.transform.rotation.eulerAngles.x, 180, particles.transform.rotation.eulerAngles.z);
        }
        else
        {
            areaEffector.forceAngle = 0;
            particles.transform.eulerAngles = new Vector3(particles.transform.rotation.eulerAngles.x, 0, particles.transform.rotation.eulerAngles.z);
        }
    }
}
