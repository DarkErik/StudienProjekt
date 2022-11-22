using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphObjectBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = Util.GetRootTransform(collision.gameObject.transform).gameObject;
        if (PlayerController.IsPlayer(player))
        {
            Morph.RemoveAllItemsAndSlots();

            GameObject.FindObjectOfType<Demorph>()?.CheckDemorph();
        }
    }
}
