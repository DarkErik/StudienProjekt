using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject scientist; //scientist that picks up flashlight

    [SerializeField] GameObject lightning;

    private bool isBright = false; //signals if the flashlight is on
    private bool isFlashlight = false; //signals if the flashlight is ready to be turned on
    private float flash; //input
    private bool isPickUp = false; //signals if the flashlight is currently picked up
    


    void Update()
    {
        //turn flashlight on and off by space
        flash = Input.GetAxis("Flashlight");
        if (flash != 0)
        {
            if (isFlashlight)
            {
                isBright = !isBright;
                lightning.SetActive(isBright);
                isFlashlight = false;
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
                isPickUp = false;
            }
        }
        if(scientist != null)
        {
            if(scientist.GetComponent<PickFlashlight>().puttingDown == true)
            {
                transform.SetParent(null);
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
                    transform.SetParent(collision.transform);
                    transform.rotation = Quaternion.identity;
                    transform.localPosition = new Vector3(0.27f, 0.15f, 0f);
                    isPickUp = true;
                }
            }
        }
    }
}
