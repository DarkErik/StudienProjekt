using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickFlashlight : MonoBehaviour
{
    public bool puttingDown = false;
    [SerializeField] GameObject handPos;

    void ActivateHandPos()
    {
        handPos.SetActive(true);
    }

    void DeactivateHandPos()
    {
        handPos.SetActive(false);
    }

}
