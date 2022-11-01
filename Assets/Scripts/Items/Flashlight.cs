using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private bool isFlashlight = false;
    private float flash;
    public bool isBright = false;
    public GameObject[] scientists;

    void Start()
    {
        //get all scientists in this scene
        scientists = GameObject.FindGameObjectsWithTag("Scientist");
    }

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

        //if the flashlight is on, set variable pickUp in the scienceguy script to true, if its off, set putDown to true
        if(isBright)
        {
            if(scientists.Length != 0)
            {
                foreach(GameObject scienceGuy in scientists)
                {
                    PickFlashlight pick = (PickFlashlight) scienceGuy.GetComponent(typeof(PickFlashlight));
                    pick.pickUp = true;
                }
            }
        }
        else
        {
            if (scientists.Length != 0)
            {
                foreach (GameObject scienceGuy in scientists)
                {
                    PickFlashlight pick = (PickFlashlight)scienceGuy.GetComponent(typeof(PickFlashlight));
                    pick.putDown = true;
                    transform.SetParent(null);
                }
            }
        }
    }

}
