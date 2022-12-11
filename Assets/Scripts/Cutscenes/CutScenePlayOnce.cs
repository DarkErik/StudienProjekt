using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CutScenePlayOnce : MonoBehaviour
{
    public static readonly string ANIMATION_FLAG_PREFIX = "animation#";

    [SerializeField] private string cutsceneAnimationName = "mainCutsceneName";
    [SerializeField] private string cutsceneResultName = "cutsceneAlreadyPlayedName";
    [SerializeField] private string playerFlagAnimName = "FlagName";

    private Animator anim;


    public void Awake()
    {
        anim = GetComponent<Animator>();

        if (PlayerData.instance.IsFlagSet(ANIMATION_FLAG_PREFIX + playerFlagAnimName))
        {
            anim.Play(cutsceneResultName);
        } else
        {
            PlayerData.instance.SetFlag(ANIMATION_FLAG_PREFIX + playerFlagAnimName);
            anim.Play(cutsceneAnimationName);
        }
    }
}
