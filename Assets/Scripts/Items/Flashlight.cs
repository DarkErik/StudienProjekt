using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private bool isFlashlight = false;
    private float flash;
    public bool isBright = false;


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
                transform.SetParent(null);
            }
        }
        if (flash == 0)
        {
            isFlashlight = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBright)
        {
            if (collision.tag == "Scientist")
            {
                transform.SetParent(collision.transform);
            }
        }
    }
}
