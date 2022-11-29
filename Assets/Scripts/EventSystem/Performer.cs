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
        } else
        {
            OnUpdateFalse();
        }
    }

    protected abstract void OnUpdate();
    protected virtual void OnUpdateFalse()
    {
    }

    public abstract void OnTap(Trigger triggerData);

    public virtual void SetTrigger(bool trigger, Trigger triggerData)
    {
        isTriggered = trigger;
        this.triggerData = triggerData;
    }
}
