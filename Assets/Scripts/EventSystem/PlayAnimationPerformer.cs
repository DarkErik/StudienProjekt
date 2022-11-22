using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationPerformer : Performer
{
    [SerializeField] private string animTriggerName = "name of animation trigger";
    [SerializeField] private Animator anim;

    private void Awake()
    {
        if (anim == null) anim = GetComponent<Animator>();
    }


    public override void OnTap(Trigger triggerData)
    {
        anim.SetTrigger(animTriggerName);
        Debug.Log("Play: " + animTriggerName);
    }

    protected override void OnUpdate()
    {
        //Debug.Log("This Performer has undefinied Behaivour in OnUpdate");
    }
}
