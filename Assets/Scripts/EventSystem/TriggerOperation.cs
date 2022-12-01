using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOperation : Performer
{

    [SerializeField] private OperationType operationType;
    [SerializeField] private Trigger[] trigger;
    //[SerializeField] private Performer[] performers;

    private bool[] triggerStateArr;


    private void Awake()
    {
        foreach(Trigger t in trigger)
        {
            if (!Util.ArrayContains(t.performer, this))
            {
                t.performer = Util.AppendArray(t.performer, this);
            }
        }

        triggerStateArr = new bool[trigger.Length];
    }

    private bool Eval()
    {
        switch(operationType)
        {
            case OperationType.AND:
                for(int i = 0; i < triggerStateArr.Length; i++)
                {
                    if (!triggerStateArr[i]) return false;
                }
                return true;
            case OperationType.OR:
                for(int  i = 0; i < triggerStateArr.Length;i++)
                {
                    if (triggerStateArr[i]) return true;
                }
                return false;
            case OperationType.NOT:
                return !triggerStateArr[0];
            default:
                Debug.LogError("Unkown Type");
                return false;
        }
    }

    private int TriggerToIndex(Trigger t)
    {
        for(int i = 0; i < trigger.Length; i++)
        {
            if (trigger[i] == t) return i;
        }
        return -1;
    }

    public override void OnTap(Trigger triggerData)
    {
        int index = TriggerToIndex(triggerData);
        bool oldState = triggerStateArr[index];
        triggerStateArr[index] = true;
        if (Eval())
        {
            TapTrigger();
        }
        triggerStateArr[index] = false;
    }

    protected override void OnUpdate()
    {
        //DO NOTHING
    }


    public override void SetTrigger(bool trigger, Trigger triggerData)
    {
        int index = TriggerToIndex(triggerData);

        triggerStateArr[index] = trigger;
        SetTriggerState(Eval());
    }



    public enum OperationType
    {
        AND,
        OR,
        NOT
    }
}
