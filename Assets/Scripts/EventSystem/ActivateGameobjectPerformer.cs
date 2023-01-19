using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGameobjectPerformer : Performer
{
    [SerializeField] private new GameObject gameObject;
    public override void OnTap(Trigger triggerData)
    {
        gameObject?.SetActive(!gameObject.activeSelf);
    }

    protected override void OnUpdateFalse()
    {
        if (gameObject != null && gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    protected override void OnUpdate()
    {
        if (gameObject != null && !gameObject.activeSelf)
            gameObject.SetActive(true);
    }
}
