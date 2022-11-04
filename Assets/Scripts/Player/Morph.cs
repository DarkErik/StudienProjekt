using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Morph : MonoBehaviour
{
    [SerializeField] private PowerUpObject item1;
    [SerializeField] private PowerUpObject item2;
    [SerializeField] private Image item1Image;
    [SerializeField] private Image item2Image;

    private float timer;
    private bool canMorph;
    private bool inCollider;

    void Start()
    {
        timer = 0f;
        canMorph = false;
        inCollider = false;
    }

    void Update()
    {
        if (!canMorph)
            canMorph = 0.5f < (timer += Time.deltaTime);

        if (!inCollider)
            CheckMorph();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            inCollider = true;

            if (canMorph) {
                if (Input.GetAxis("Morph1") > 0)
                {
                    GameObject newPlayer = collision.gameObject.GetComponent<PowerUpObject>().PlayerObject;
                    item1.PlayerObject = newPlayer;
                    item1Image.sprite = newPlayer.GetComponentInChildren<SpriteRenderer>().sprite;
                    Morphing(newPlayer);
                }
                
                if (Input.GetAxis("Morph2") > 0)
                {
                    GameObject newPlayer = collision.gameObject.GetComponent<PowerUpObject>().PlayerObject;
                    item2.PlayerObject = newPlayer;
                    item2Image.sprite = newPlayer.GetComponentInChildren<SpriteRenderer>().sprite;
                    Morphing(newPlayer);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
            inCollider = false;
        }

    //method for applying DNA from slot
    private void CheckMorph()
    {
        if (canMorph) {
                if (Input.GetAxis("Morph1") > 0)
                {
                    if (item1.PlayerObject != null)
                        Morphing(item1.PlayerObject);
                }
                
                if (Input.GetAxis("Morph2") > 0)
                {
                    if (item2.PlayerObject != null)
                        Morphing(item2.PlayerObject);
                }
            }
    }

    private void Morphing(GameObject newPlayer)
    {
        DisableOldPlayer(gameObject);
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
