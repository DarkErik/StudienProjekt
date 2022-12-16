using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] bool play;

    void Update()
    {
        if (play)
        {
            audioSource.Stop();
            audioSource.Play();
        }
    }
}
