using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morph : MonoBehaviour
{
    float timer;
    private bool canMorph;

    void Start()
    {
        timer = 0f;
        canMorph = false;
    }

    void Update()
    {
        if (!canMorph)
            canMorph = 0.5f < (timer += Time.deltaTime);

        CheckMorph();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            if (canMorph) {
                if (Input.GetAxis("Morph1") > 0)
                {
                    Morphing(collision.gameObject); //TODO: overwrite slot 1 
                }
                
                if (Input.GetAxis("Morph2") > 0)
                {
                    Morphing(collision.gameObject); //TODO: overwrite slot 1 
                }
            }
        }
    }

    //TODO: method for applying DNA from slot
    private void CheckMorph()
    {
        /* if (canMorph) {
                if (Input.GetAxis("Morph1") > 0)
                {
                    Morphing(collision.gameObject); //TODO: get player of slot 1
                }
                
                if (Input.GetAxis("Morph2") > 0)
                {
                    Morphing(collision.gameObject); //TODO: get player of slot 2 
                }
            }*/
    }

    private void Morphing(GameObject powerUp)
    {
        DisableOldPlayer(gameObject);
        GameObject newPlayer = powerUp.GetComponent<PowerUpObject>().PlayerObject;
        GameObject newPlayerObject = Instantiate(newPlayer, gameObject.transform.position, gameObject.transform.rotation);
        StartCoroutine(FadeIn(newPlayerObject));
    }

    private void DisableOldPlayer(GameObject oldPlayer)
    {
        Collider2D[] colliders = oldPlayer.GetComponents<Collider2D>();
        MonoBehaviour[] scripts = oldPlayer.GetComponents<MonoBehaviour>();
        Rigidbody2D rb = oldPlayer.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;

        if (colliders != null)
        {
            foreach (Collider2D collider in colliders)
                collider.enabled = false;
        }

        if (scripts != null)
        {
            foreach (MonoBehaviour script in scripts)
                script.enabled = false;
        }

        StartCoroutine(FadeOut(oldPlayer));
    }

    IEnumerator FadeOut(GameObject gameObject)
    {
        float fadeValue = 1f;
        SpriteRenderer[] srs = gameObject.GetComponentsInChildren<SpriteRenderer>();
        Material[] materials;
        List<Material> materialList = new List<Material>();
        foreach (SpriteRenderer sr in srs)
            materialList.Add(sr.material);
        materials = materialList.ToArray();

        while (fadeValue > 0f)
        {
            fadeValue -= Time.deltaTime;
            foreach(Material material in materials)
                material.SetFloat("_Fade", fadeValue);
            yield return null;
        }

        Destroy(gameObject);
    }

    IEnumerator FadeIn(GameObject gameObject)
    {
        float fadeValue = 0f;
        SpriteRenderer[] srs = gameObject.GetComponentsInChildren<SpriteRenderer>();
        Material[] materials;
        List<Material> materialList = new List<Material>();
        foreach (SpriteRenderer sr in srs)
            materialList.Add(sr.material);
        materials = materialList.ToArray();

        while (fadeValue < 1f)
        {
            fadeValue += Time.deltaTime;
            foreach (Material material in materials)
                material.SetFloat("_Fade", fadeValue);
            yield return null;
        }
    }

}
