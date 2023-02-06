using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;
    
    [SerializeField] private float speed;
    private Transform target;
    private TextMeshProUGUI coinText;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private AudioSource audioCollect;
    private Camera cam;
    public int currentCoins;
    private float fontSizeOrigin;

    private void Awake()
    {
        instance = this;
        currentCoins = PlayerPrefs.GetInt("coinAmount", 0);
        Debug.Log(currentCoins);
    }

    private void Start()
    {
        target = GameObject.Find("/Manager/MainCanvas/Panel/CoinImage").GetComponent<Transform>();
        coinText = GameObject.Find("/Manager/MainCanvas/Panel/CoinNumber").GetComponent<TextMeshProUGUI>();
        cam = GameObject.Find("/Manager/CameraConfiner/Main Camera").GetComponent<Camera>();
        coinText.text = currentCoins.ToString();
        fontSizeOrigin = coinText.fontSize;
    }

    public void StartMovement(Vector3 initialPos, Action onComplete)
    {
        audioCollect.Play();
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
        PlayerPrefs.SetInt("coinAmount", currentCoins);
        coinText.text = currentCoins.ToString();
    }
}
