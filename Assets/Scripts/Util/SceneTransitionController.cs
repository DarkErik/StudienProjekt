using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour
{

    public string sceneToLoad;
    public float speed = 2f;
    public Texture2D texture;

    private Material material;
    private bool shouldReveal;
    private GameObject canvas;

    void Start()
    {
        material = GetComponent<Image>().material;
        material.SetTexture("_TransitionTex", texture);
        shouldReveal = true;
        canvas = GameObject.Find("/MainCanvas");
    }

    void Update()
    {
        if (shouldReveal) //open animation
        {
            material.SetFloat("_Cutoff", Mathf.MoveTowards(material.GetFloat("_Cutoff"), 1.1f, speed * Time.deltaTime));
        } else //close animation and load new scene
        {
            canvas.SetActive(false);
            material.SetFloat("_Cutoff", Mathf.MoveTowards(material.GetFloat("_Cutoff"), -0.1f - material.GetFloat("_EdgeSmoothing"), speed * Time.deltaTime));
            if (material.GetFloat("_Cutoff") == (-0.1f - material.GetFloat("_EdgeSmoothing")))
                SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void ChangeScene()
    {
        shouldReveal = false;
    }

}
