using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCredits : MonoBehaviour
{
    private int amount = 0;


    public void Update()
    {
        if (Input.anyKeyDown)
        {
            amount++;
            if (amount > 2)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
