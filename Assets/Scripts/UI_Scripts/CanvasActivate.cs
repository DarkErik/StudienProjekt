using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasActivate : MonoBehaviour
{
    float pause = 0f;
    float overlay = 0f;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] Image itemOverlayBackground;
    [SerializeField] Image itemOverlayItem1;
    [SerializeField] Image itemOverlayItem2;
    [SerializeField] TextMeshProUGUI itemOverlayItem1Text;
    [SerializeField] TextMeshProUGUI itemOverlayItem2Text;

    void Update()
    {
        pause = Input.GetAxis("PauseGame");
        overlay = Input.GetAxis("OverlayItems");
        if(pause != 0)
        {
            PauseMenu.SetActive(true);
        }
        if (overlay != 0)
        {
            if(PauseMenu.activeInHierarchy == false)
            {
                itemOverlayBackground.enabled = true;
                itemOverlayItem1.enabled = true;
                itemOverlayItem2.enabled = true;
                itemOverlayItem1Text.enabled = true;
                itemOverlayItem2Text.enabled = true;
            } 
        }
        else
        {
            itemOverlayBackground.enabled = false;
            itemOverlayItem1.enabled = false;
            itemOverlayItem2.enabled = false;
            itemOverlayItem1Text.enabled = false;
            itemOverlayItem2Text.enabled = false;
        }
    }

    public void ContinueGame()
    {
        PauseMenu.SetActive(false);
    }
}
