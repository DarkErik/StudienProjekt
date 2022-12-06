using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demorph : MonoBehaviour
{
    public GameObject defaultPlayer;

    void Update()
    {
        if (Input.GetAxis("Demorph") > 0)
            CheckDemorph();
    }

    //start morphing to default player
    public void CheckDemorph()
    {
        if (PlayerController.Instance.getUUID() != 0)
        {
            AudioManager.instance.Play("Demorph");
            DisableOldPlayer(gameObject);
            GameObject newPlayerObject = Instantiate(defaultPlayer, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.3f, gameObject.transform.position.z), Quaternion.identity);
            StartCoroutine(FadeIn(newPlayerObject));
        }
    }

    //disable old player's scripts and colliders
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

    //fade out old player
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
            foreach (Material material in materials)
                material.SetFloat("_Fade", fadeValue);
            yield return null;
        }

        Destroy(gameObject);
    }

    //fade in default player
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
