using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{

    [SerializeField] AudioSource[] audios;
    [SerializeField] float lowestGap;
    [SerializeField] float highestGap;

    private float timer;
    private float currentGap;
    private int nextAudio;

    void Start()
    {
        if(lowestGap > highestGap)
        {
            float temp = lowestGap;
            lowestGap = highestGap;
            highestGap = temp;
        }

        nextRandoms();
    }

    void Update()
    {
        if (timer < currentGap)
            timer += Time.deltaTime;
        else
        {
            audios[nextAudio].Play();
            nextRandoms();
        }
    }

    private void nextRandoms()
    {
        timer = 0f;
        currentGap = Random.Range(lowestGap, highestGap);
        nextAudio = Random.Range(0, audios.Length);
    }
}
