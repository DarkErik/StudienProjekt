using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickFlashlight : MonoBehaviour
{
    [SerializeField] GameObject handPos;
   /* private void Update()
    {
        
        if (GetComponent<Patrol>().isPickUp)
        {
            handPos.SetActive(true);
        }
        if (GetComponent<Patrol>().isPickUp)
        {
            handPos.SetActive(false);
        }
        
    }*/

    void ActivateHandPos()
    {
        handPos.SetActive(true);
    }

    void DeactivateHandPos()
    {
        handPos.SetActive(false);
    }

}
