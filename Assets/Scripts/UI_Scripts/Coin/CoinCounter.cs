using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;
    
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Camera cam;
    private int currentCoins;
    private float fontSizeOrigin;

    private void Awake()
    {
        instance = this;
        currentCoins = 0;
    }

    private void Start()
    {
        coinText.text = currentCoins.ToString();
        fontSizeOrigin = coinText.fontSize;
    }

    public void StartMovement(Vector3 initialPos, Action onComplete)
    {
        GameObject coin = Instantiate(coinPrefab, transform);
        StartCoroutine(MoveCoin(coin.transform, initialPos, onComplete));
    }

    IEnumerator MoveCoin(Transform coin, Vector3 initialPos, Action onComplete)
    {
        Vector3 targetPos = cam.ScreenToWorldPoint(new Vector3(target.position.x, target.position.y, cam.transform.position.z * (-1)));
        coin.position = initialPos;

        while ((targetPos - coin.position).magnitude > 0.1f)
        {
            coin.Translate((targetPos - coin.position).normalized * speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            targetPos = cam.ScreenToWorldPoint(new Vector3(target.position.x, target.position.y, cam.transform.position.z * (-1)));
        }

        onComplete.Invoke();
        Destroy(coin.gameObject);
        StartCoroutine(HighlightText());
    }

    IEnumerator HighlightText()
    {
        coinText.fontSize = fontSizeOrigin + 15f;
        float timer = 0f;
        while (timer < 0.5f) {
            timer += Time.deltaTime;
            yield return null;
        }
        coinText.fontSize = fontSizeOrigin;
    }

    public void increaseCoins(int v)
    {
        currentCoins += v;
        coinText.text = currentCoins.ToString();
    }
}
