using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private bool isFlashlight = false;
    private float flash;
    private bool isReady = true;

    void Start()
    {
        
    }

    void Update()
    {
        flash = Input.GetAxis("Flashlight");
        
        if (flash != 0)
        {
            isFlashlight = !isFlashlight;
        }

        transform.GetChild(0).gameObject.SetActive(isFlashlight);
    }
}
