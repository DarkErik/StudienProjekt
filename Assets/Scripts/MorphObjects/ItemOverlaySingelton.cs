using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemOverlaySingelton : MonoBehaviour
{
    public static ItemOverlaySingelton Instance { get; private set; }

    public Image item1;
    public PowerUpObject powerup1;

    public Image item2;
    public PowerUpObject powerup2;

    public void Awake()
    {
        Instance = this;
    }
}
