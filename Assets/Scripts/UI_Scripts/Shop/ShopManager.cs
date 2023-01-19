using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI coinText;

    private float fontSizeOrigin;

    void Start()
    {
        fontSizeOrigin = coinText.fontSize;
        coinText.text = CoinCounter.instance.currentCoins.ToString();
    }


    void Update()
    {
        
    }

    public void ShowDescription(string description)
    {
        StopAllCoroutines();
        StartCoroutine(TypeLetters(description));
    }

    IEnumerator TypeLetters(string sentence)
    {
        descriptionText.text = sentence;
        descriptionText.maxVisibleCharacters = 0;

        foreach (char letter in sentence.ToCharArray())
        {
            if (descriptionText.maxVisibleCharacters % 3 == 0)
                AudioManager.instance.Play("VoiceFX");

            descriptionText.maxVisibleCharacters += 1;
            yield return new WaitForSecondsRealtime(0.025f);
        }
    }

    public void Buy(int id)
    {
        int price = 0;
        string key = "";
        string value = "";

        switch (id)
        {
            case 1:
                price = 5;
                key = "playerColor";
                value = "#FFFFFF";
                break;
            case 2:
                price = 5;
                key = "playerColor";
                value = "#4FCBFF";
                break;
            case 3:
                price = 5;
                key = "playerColor";
                value = "#FF4094";
                break;
            case 4:
                price = 100;
                key = "scientistWeaker";
                value = "0.75";
                break;
            default:
                break;
        }

        if (CoinCounter.instance.currentCoins < price)
        {
            ShowDescription("Seems like you don't have enough coins");
            return;
        }

        ShowDescription("Thank you for your purchase");

        CoinCounter.instance.increaseCoins(-price);
        coinText.text = CoinCounter.instance.currentCoins.ToString();
        StartCoroutine(HighlightText());

        PlayerPrefs.SetString(key, value);
    }

    IEnumerator HighlightText()
    {
        coinText.fontSize = fontSizeOrigin + 15f;
        float timer = 0f;
        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        coinText.fontSize = fontSizeOrigin;
    }
}
