using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject scientist; //scientist that picks up flashlight

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
                transform.GetChild(0).gameObject.SetActive(isBright);
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
                Debug.Log("putdown");
                scientist.GetComponent<Patrol>().putDown = true;
                isPickUp = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBright)
        {
            if (!isPickUp)
            {
                if (collision.tag == "Scientist") //if colliding with scientist, set pickUp of script Patrol true, so the pickUp animation can start playing
                {
                    collision.GetComponent<Patrol>().pickUp = true;
                    scientist = collision.gameObject;
                }
                if (collision.tag == "ScientistHand") //if colliding with the scientists HandPoint, get picked up
                {
                    Debug.Log("collision");
                    transform.SetParent(collision.transform);
                    transform.rotation = Quaternion.identity;
                    transform.localPosition = new Vector3(0.27f, 0.15f, 0f);
                    isPickUp = true;
                }
            }
        }
    }
}
