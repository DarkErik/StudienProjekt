using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMChanger : MonoBehaviour
{
    [SerializeField] string newBGM;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            AudioManager.instance.ChangeBackgroundMusic(newBGM);
    }
}
