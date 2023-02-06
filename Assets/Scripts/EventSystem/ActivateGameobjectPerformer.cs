using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGameobjectPerformer : Performer
{
    private bool deactivateFalse = false;
    [SerializeField] private new GameObject gameObject;
    public override void OnTap(Trigger triggerData)
    {
        deactivateFalse = true;
        if (gameObject != null)
            gameObject.SetActive(!gameObject.activeSelf);
    }

    protected override void OnUpdateFalse()
    {
        if (gameObject != null && gameObject.activeSelf && !deactivateFalse)
        {
            Debug.Log("UpdateFaslse");
            gameObject.SetActive(false);
        }
    }

    protected override void OnUpdate()
    {
        if (gameObject != null && !gameObject.activeSelf)
            gameObject.SetActive(true);
    }
}
