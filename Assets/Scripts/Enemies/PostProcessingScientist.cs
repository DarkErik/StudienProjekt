using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PostProcessingScientist : MonoBehaviour
{
    public static PostProcessingScientist Instance { get; private set; }

    public float intensity = 0f;
    public bool spotted = false;

    [SerializeField] private Volume volume;
    [SerializeField] private GameObject spottedScreen;
    private Vignette vignette;
    private ChromaticAberration chromaticAberration;

    private void Awake()
    {
        Instance = this;
        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<ChromaticAberration>(out chromaticAberration);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        float sinTime = Mathf.Sin(Time.time * 5) * 0.2f;

        if (intensity == 0)
            sinTime = 0;

        vignette.intensity.value = Mathf.Min(1, intensity) + sinTime;
        chromaticAberration.intensity.value = Mathf.Min(1, intensity) + sinTime;
            
        if (intensity >= 1 && !spotted)
        {
            spotted = true;
            Time.timeScale = 0;
            StartCoroutine(Spotted());
        }

        intensity = 0;
    }

    private IEnumerator Spotted()
    {
        Instantiate(spottedScreen);
        yield return new WaitForSecondsRealtime(2f);
        RespawnPoint.Respawn();
        spotted = false;
        Time.timeScale = 1;
    }

    public void TellIntensity(float intensity)
    {
        Debug.Log(intensity);
        if (this.intensity < intensity)
        {
            this.intensity = intensity;
        }
    }

}
