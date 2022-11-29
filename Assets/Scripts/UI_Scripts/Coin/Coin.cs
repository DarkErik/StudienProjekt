using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CoinCounter.instance.StartMovement(transform.position, ()=>
            {
                CoinCounter.instance.increaseCoins(value);
            });
            
            Destroy(gameObject);
        }
    }
}
