using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject scientist; //scientist that picks up flashlight
    [SerializeField] int flashlightLayer = 9;
    [SerializeField] int defaultLayer = 0;
    [SerializeField] int noCollisionLayer = 10;
    [SerializeField] GameObject lightning;
    [SerializeField] Rigidbody2D rb;
    public AudioSource audioChangeMode;

    private bool isBright = false; //signals if the flashlight is on
    private bool isFlashlight = false; //signals if the flashlight is ready to be turned on
    private float flash; //input
    private bool isPickUp = false; //signals if the flashlight is currently picked up

    void Update()
    {
        if (scientist != null)
        {
            if (scientist.GetComponent<PickFlashlight>().activateRb == true)
            {
                if (rb == null)
                {
                    rb = gameObject.AddComponent<Rigidbody2D>();
                }
            }
        }
        //turn flashlight on and off by space
        flash = Input.GetAxis("Flashlight");
        if (flash != 0)
        {
            if (isFlashlight)
            {
                isBright = !isBright;
                if (!isBright) 
                { 
                    gameObject.layer = flashlightLayer;
                }
                else
                { 
                    gameObject.layer = defaultLayer;
                }
                lightning.SetActive(isBright);
                isFlashlight = false;
                audioChangeMode.Play();
            }
        }
        if (flash == 0)
        {
            isFlashlight = true;
        }

        if (!isBright)
        {
            if (isPickUp)
            {
                scientist.GetComponent<Patrol>().PutDown();
                gameObject.layer = flashlightLayer;

                isPickUp = false;
            }
        }
        if(scientist != null)
        {
            if(scientist.GetComponent<PickFlashlight>().puttingDown == true)
            {
                transform.SetParent(null);
                if(transform.rotation.y != 0)
                {
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBright)
        {
            if (!isPickUp)
            {
                if (collision.CompareTag("Scientist")) //if colliding with scientist, set pickUp of script Patrol true, so the pickUp animation can start playing
                {
                    collision.GetComponent<Patrol>().PickUp();
                    scientist = collision.gameObject;
                }
                if (collision.CompareTag("ScientistHand")) //if colliding with the scientists HandPoint, get picked up
                {
                    Destroy(rb);
                    transform.SetParent(collision.transform);
                    gameObject.layer = noCollisionLayer;
                    //transform.rotation = collision.transform.rotation * Quaternion.Euler(0f, 0f, 0f);
                    transform.localRotation = Quaternion.Euler(0f, 0f, 137f);
                    //transform.rotation = Quaternion.identity;
                    transform.localPosition = new Vector3(0f, 0f, 0f);
                    isPickUp = true;
                }
            }
        }
    }
}
