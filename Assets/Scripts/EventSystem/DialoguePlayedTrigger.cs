using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePlayedTrigger : Trigger
{
    [SerializeField] private string dialogueID = "name";
    [SerializeField] private bool fireOnDialogueStart = false;
    [SerializeField] private bool tapOnly = false;

    private bool dialogueStartedPlaying = false;
    private bool dialogueFinished = false;

    public void Start()
    {
        SetTriggerState(false);
    }

    public void Update()
    {

        if (!dialogueStartedPlaying && DialogueManager.currentDialogue != null && DialogueManager.currentDialogue.id == dialogueID)
        {
            if (fireOnDialogueStart)
            {
                if (!tapOnly)
                    SetTriggerState(true);
                else 
                    TapTrigger();
            }
            dialogueStartedPlaying = true;
        }

        if (!fireOnDialogueStart && dialogueStartedPlaying && !dialogueFinished && (DialogueManager.currentDialogue == null || DialogueManager.currentDialogue.id != dialogueID)) {
            dialogueFinished = true;
            if (!tapOnly)
                SetTriggerState(true);
            else
                TapTrigger();
        }
    }

    public void Restart()
    {
        dialogueFinished = false;
        dialogueStartedPlaying = false;
    }
}
