using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPerformer : Performer
{
    public static readonly string DIALOGUE_FLAG_PREFIX = "dialogue#";

    [SerializeField] private Dialogue[] dialogues;
    private int index = 0;
    [Header("IF TRUE, YOU MUST SET ID!")]
    [SerializeField] private bool playDialogueZeroOnlyOnce = false;
    public override void OnTap(Trigger triggerData)
    {
        if(playDialogueZeroOnlyOnce && index == 0)
        {
            if (PlayerData.instance.IsFlagSet(DIALOGUE_FLAG_PREFIX + dialogues[0].id))
            {
                index++;
                if (index >= dialogues.Length) Debug.LogError("No Second Dialogue set!");
            } else
            {
                PlayerData.instance.SetFlag(DIALOGUE_FLAG_PREFIX + dialogues[0].id);
            }
        }

        DialogueManager.Instance.StartDialogue(dialogues[index]);
        if (++index >= dialogues.Length) 
            index = dialogues.Length - 1;
    }

    protected override void OnUpdate()
    {
    }
}
