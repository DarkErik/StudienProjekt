using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipToFan : MonoBehaviour
{
    [SerializeField] private int amount = 2;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
           if (--amount <= 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Fan#1");
            }
        }
    }
}
