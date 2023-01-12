using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static readonly string COIN_PREFIX = "coin#";
    [SerializeField] private int value;
    private string coinFlagName;

    private void Start()
    {
        coinFlagName = $"{COIN_PREFIX}{UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}#{transform.position.x}#{transform.position.y}";

        if (PlayerData.instance.IsFlagSet(coinFlagName))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CoinCounter.instance.StartMovement(transform.position, ()=>
            {
                CoinCounter.instance.increaseCoins(value);
                PlayerData.instance.SetFlag(coinFlagName);
            });
            
            Destroy(gameObject);
        }
    }
}
