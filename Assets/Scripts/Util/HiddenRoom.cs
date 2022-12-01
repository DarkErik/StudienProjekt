using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenRoom : MonoBehaviour
{
    private Tilemap tm;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        tm = GetComponent<Tilemap>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("FadeOut", true);
            //tm.color = new Color(1f, 1f, 1f, 0.3f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("FadeOut", false);
            //tm.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
