using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindPlayerToMovingPlatform : MonoBehaviour
{
    [SerializeField] private MoveablePlatform plat;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        MoveAlong move = collision.gameObject.GetComponent<MoveAlong>();
        if(move != null)
        {
            if (move.rb.velocity.y <= 0)
            {
                move.rb.velocity = new Vector2(move.rb.velocity.x, 0);
                plat.ontopObjects.AddLast(move);
            }

            Debug.Log("Bind: " + move.gameObject.name);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        MoveAlong move = collision.gameObject.GetComponent<MoveAlong>();
        if (move != null)
        {
            plat.ontopObjects.Remove(move);
            Debug.Log("Unbind: " + move.gameObject.name);
        }
    }

    public void OnTriggerEnter2D(Collider2D c)
    {
        if (PlayerController.IsPlayer(c.gameObject))
        {
            plat.newPlayerInTrigger = true;
        }
    }

    public void OnTriggerExit2D(Collider2D c)
    {
        if (PlayerController.IsPlayer(c.gameObject))
        {
            plat.newPlayerInTrigger = false;
        }
    }
}
