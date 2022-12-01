using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPerformer : Performer
{
    [SerializeField] private Dialogue dialogue;

    public override void OnTap(Trigger triggerData)
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    protected override void OnUpdate()
    {
    }
}
