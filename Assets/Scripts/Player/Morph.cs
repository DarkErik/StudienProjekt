using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Morph : MonoBehaviour
{
    private GameObject itemSlot1;
    private GameObject itemSlot2;

    private PowerUpObject item1;
    private PowerUpObject item2;
    private Image item1Image;
    private Image item2Image;

    private float timer;
    private bool canMorph;
    private bool inCollider;

    void Start()
    {
        timer = 0f;
        canMorph = false;
        inCollider = false;

        itemSlot1 = GameObject.Find("/MainCanvas/ItemOverlay/Item1");
        itemSlot2 = GameObject.Find("/MainCanvas/ItemOverlay/Item2");

        if (itemSlot1 != null)
        {
            item1 = itemSlot1.GetComponent<PowerUpObject>();
            item1Image = itemSlot1.GetComponent<Image>();
        }

        if (itemSlot2 != null)
        {
            item2 = itemSlot2.GetComponent<PowerUpObject>();
            item2Image = itemSlot2.GetComponent<Image>();
        }
        
    }

    void Update()
    {
        if (itemSlot1 == null)
        {
            itemSlot1 = GameObject.Find("/MainCanvas/ItemOverlay/Item1");
            item1 = itemSlot1.GetComponent<PowerUpObject>();
            item1Image = itemSlot1.GetComponent<Image>();
        }

        if (itemSlot2 == null)
        {
            itemSlot2 = GameObject.Find("/MainCanvas/ItemOverlay/Item2");
            item2 = itemSlot2.GetComponent<PowerUpObject>();
            item2Image = itemSlot2.GetComponent<Image>();
        }

        if (itemSlot1 != null && itemSlot2 != null)
        {
            if (!canMorph)
                canMorph = 0.5f < (timer += Time.deltaTime);

            if (!inCollider)
                CheckMorph();
        }
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
                    item1Image.SetNativeSize();
                    Morphing(newPlayer);
                }
                
                if (Input.GetAxis("Morph2") > 0)
                {
                    GameObject newPlayer = collision.gameObject.GetComponent<PowerUpObject>().PlayerObject;
                    item2.PlayerObject = newPlayer;
                    item2Image.sprite = newPlayer.GetComponentInChildren<SpriteRenderer>().sprite;
                    item2Image.SetNativeSize();
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
        GameObject newPlayerObject = Instantiate(newPlayer, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.3f, gameObject.transform.position.z), Quaternion.identity);
        StartCoroutine(FadeIn(newPlayerObject));
    }

    private void DisableOldPlayer(GameObject oldPlayer)
    {
        Collider2D[] colliders = oldPlayer.GetComponents<Collider2D>();
        Collider2D[] collidersChildren = oldPlayer.GetComponentsInChildren<Collider2D>();
        MonoBehaviour[] scripts = oldPlayer.GetComponents<MonoBehaviour>();
        Rigidbody2D rb = oldPlayer.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;

        if (colliders != null)
        {
            foreach (Collider2D collider in colliders)
                collider.enabled = false;
        }

        if (collidersChildren != null)
        {
            foreach (Collider2D collider in collidersChildren)
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
