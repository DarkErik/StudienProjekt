using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanonPerformer : Performer
{
    [SerializeField] private DropItemPerfomer diP;
    [SerializeField] private GameObject explosion;
    private bool alreadyActivated = false;


    public override void OnTap(Trigger triggerData)
    {
        if (!alreadyActivated)
        {
            alreadyActivated = true;
            StartCoroutine(Expl());
        }
    }

    private IEnumerator Expl()
    {
        yield return new WaitForSeconds(diP.time);
        explosion.SetActive(true);
        Boss.Instance.ContinuePhase();
    }
    protected override void OnUpdate()
    {

    }
}
