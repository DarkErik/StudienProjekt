using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Performer : Trigger
{
    protected bool isTriggered = false;
    protected Trigger triggerData = null;

    public void Update()
    {
        if (isTriggered)
        {
            OnUpdate();
        }
    }

    protected abstract void OnUpdate();
    public abstract void OnTap(Trigger triggerData);

    public virtual void SetTrigger(bool trigger, Trigger triggerData)
    {
        isTriggered = trigger;
        this.triggerData = triggerData;
    }
}
