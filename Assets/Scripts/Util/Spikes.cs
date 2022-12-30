using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject oldPlayer = Util.GetRootTransform(collision.gameObject.transform).gameObject;
        if (PlayerController.IsPlayer(oldPlayer))
        {
            StartCoroutine(SpikeDeath(oldPlayer));
        }
    }

    private IEnumerator SpikeDeath(GameObject oldPlayer)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.75f);
        Time.timeScale = 1;
        Destroy(oldPlayer);
        
        AudioManager.instance.Play("Damage");
        
        RespawnPoint.Respawn();
    }
}
