using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovementLantern : PlayerMovement
{
    enum LightMode
    {
        Off,
        Green,
        Red
    }

    public Light2D spotLight;
    public Material lanternGreenMaterial;
    public Material lanternRedMaterial;
    public AudioSource audioChangeMode;

    private float timer;
    private bool canChangeMode;
    private LightMode currentMode;

    void Start()
    {
        timer = 0f;
        canChangeMode = true;

        if (lanternGreenMaterial.GetFloat("_GreenValue") == 1)
            GreenLightMode();
        else if (lanternRedMaterial.GetFloat("_RedValue") == 1)
            RedLightMode();
        else
            OffLightMode();
    }


    void Update()
    {
        if (!canChangeMode) //mode can only be changed every 0.5 seconds
            canChangeMode = 0.5f < (timer += Time.deltaTime);
        else
            CheckChangeMode();
    }

    //switch light mode
    private void CheckChangeMode()
    {
        if (Input.GetAxis("Ability") > 0)
        {
            canChangeMode = false;
            timer = 0;

            audioChangeMode.Play();

            int currentModeInt = ((int)currentMode + 1) % 3;
            currentMode = (LightMode)currentModeInt;

            switch (currentMode)
            {
                case LightMode.Off:
                    OffLightMode();
                    break;
                case LightMode.Green:
                    GreenLightMode();
                    break;
                case LightMode.Red:
                    RedLightMode();
                    break;
                default:
                    break;
            }
        }
    }

    //apply off light mode
    private void OffLightMode()
    {
        spotLight.color = new Color(1,1,1,0);
        lanternGreenMaterial.SetFloat("_GreenValue", 0);
        lanternRedMaterial.SetFloat("_RedValue", 0);
    }

    //apply green light mode
    private void GreenLightMode()
    {
        spotLight.color = new Color(0.3683744f, 1, 0.2311321f, 1);
        lanternGreenMaterial.SetFloat("_GreenValue", 1);
        lanternRedMaterial.SetFloat("_RedValue", 0);
    }

    //apply red light mode
    private void RedLightMode()
    {
        spotLight.color = new Color(1, 0.4491155f, 0.3632075f, 1);
        lanternGreenMaterial.SetFloat("_GreenValue", 0);
        lanternRedMaterial.SetFloat("_RedValue", 1);
    }


}
