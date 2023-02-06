using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreshStart : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.ChangeBackgroundMusic("BGM1");
        CoinCounter.instance.increaseCoins(-CoinCounter.instance.currentCoins);
        PlayerPrefs.SetString("playerColor", "#FFFFFF");
    }

}
