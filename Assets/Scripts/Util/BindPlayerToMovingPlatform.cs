using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindPlayerToMovingPlatform : MonoBehaviour
{
    [SerializeField] private MoveablePlatform plat;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(PlayerController.IsPlayer(collision.gameObject))
        {
            plat.player = PlayerController.Instance.gameObject;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (PlayerController.IsPlayer(collision.gameObject))
        {
            plat.player = null;
        }
    }
}
