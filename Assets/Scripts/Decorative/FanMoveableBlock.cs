using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanMoveableBlock : MonoBehaviour
{
    private string moveTag = "PowerUp";
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(moveTag))
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(moveTag))
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }

}
