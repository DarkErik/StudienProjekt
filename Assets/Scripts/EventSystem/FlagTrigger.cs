using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagTrigger : Trigger
{
    [SerializeField] private string flag = "name";
    private bool oldState;

    public void Start()
    {
        oldState = PlayerData.instance.IsFlagSet(flag);
        SetTriggerState(oldState);
    }
    public void Update()
    {
        if (PlayerData.instance.IsFlagSet(flag) != oldState)
        {
            oldState = !oldState;
            SetTriggerState(oldState);
        }
    }
}
