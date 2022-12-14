using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFlagPerfomer : Performer
{
    [SerializeField] public string flagName = "name";
    [SerializeField] private bool unsetFlag = false;

    public override void OnTap(Trigger triggerData)
    {
        Debug.Log("SET: " + flagName);
        if (unsetFlag)
        {
            PlayerData.instance.RemoveFlag(flagName);
        } else
        {
            PlayerData.instance.SetFlag(flagName);
        }
    }

    protected override void OnUpdate()
    {
    }
}
