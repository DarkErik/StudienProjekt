using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale : MonoBehaviour
{
    [SerializeField] private float newScale;
    [SerializeField] private float changePerSecond;
    [SerializeField] private RectTransform rtransform;

    public void Increase()
    {
        StopAllCoroutines();
        StartCoroutine(IncreaseCoroutine());
    }

    IEnumerator IncreaseCoroutine()
    {
        float scale = 1f;
        while (scale < newScale)
        {
            scale = Mathf.Clamp(scale + changePerSecond * Time.deltaTime, 1f, newScale);
            rtransform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        
    }

    public void Decrease()
    {
        StopAllCoroutines();
        StartCoroutine(DecreaseCoroutine());
    }

    IEnumerator DecreaseCoroutine()
    {
        float scale = rtransform.localScale.x;
        while (scale > 1f)
        {
            scale = Mathf.Clamp(scale - changePerSecond * Time.deltaTime, 1f, newScale);
            rtransform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

    }
}
