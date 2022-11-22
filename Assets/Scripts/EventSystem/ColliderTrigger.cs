using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : Trigger
{
    [SerializeField] private bool onlyPlayer = true;
    [SerializeField] private bool tapOnClose = false;
    [SerializeField] private bool usePushableMask = false;
    [SerializeField] private LayerMask pushable;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((!onlyPlayer || PlayerController.IsPlayer(Util.GetRootTransform(collision.transform).gameObject)) && (!usePushableMask || pushable == (pushable | (1 << collision.gameObject.layer))))
        {
            if (!tapOnClose)
                TapTrigger();
            SetTriggerState(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((!onlyPlayer || PlayerController.IsPlayer(Util.GetRootTransform(collision.transform).gameObject)) && (!usePushableMask || pushable == (pushable | (1 << collision.gameObject.layer))))
        {
            if (tapOnClose)
                TapTrigger();
            SetTriggerState(false);
        }
    }
}
