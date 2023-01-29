using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerRestartPerfomer : Performer
{
    [SerializeField] private DialoguePlayedTrigger trigger;

    public override void OnTap(Trigger triggerData)
    {
        trigger.Restart();
    }

    protected override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
