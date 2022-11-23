using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : MonoBehaviour
{
    [SerializeField] public Performer[] performer;

    public void TapTrigger()
    {
        foreach (Performer performer in performer)
        {
            performer.OnTap(this);
        }
    }



    public void SetTriggerState(bool state)
    {
        foreach (Performer performer in performer)
        {
            performer.SetTrigger(state, this);
        }
    }
}
