using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressedTrigger : Trigger
{
    [SerializeField] private string keyname = "e";
    public void Update()
    {
        if (Input.GetKeyDown(keyname))
        {
            TapTrigger();
            SetTriggerState(true);
            Debug.Log("Button Pressed");
        }
        if(Input.GetKeyUp(keyname))
        {
            SetTriggerState(false);
        }
    }
}
