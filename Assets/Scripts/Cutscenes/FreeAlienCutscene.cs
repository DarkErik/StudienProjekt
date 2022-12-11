using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeAlienCutscene : CutScenePlayOnce
{
    [SerializeField] private GameObject player;


    public void ActivatePlayer()
    {
        player.SetActive(true);
    }
}
