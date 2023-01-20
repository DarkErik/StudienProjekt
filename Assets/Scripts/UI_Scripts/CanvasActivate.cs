using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasActivate : MonoBehaviour
{
    bool pause = false;
    float overlay = 0f;
    [SerializeField] GameObject ShopMenu;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject coinPanel;
    [SerializeField] Image itemOverlayBackground;
    [SerializeField] Image itemOverlayItem1;
    [SerializeField] Image itemOverlayItem2;
    [SerializeField] Image itemBackgroundItem1;
    [SerializeField] Image itemBackgroundItem2;
    [SerializeField] TextMeshProUGUI itemOverlayItem1Text;
    [SerializeField] TextMeshProUGUI itemOverlayItem2Text;
    [SerializeField] Image itemBackgroundItem1Text;
    [SerializeField] Image itemBackgroundItem2Text;

    void Update()
    {
        pause = Input.GetButtonDown("PauseGame");
        overlay = Input.GetAxis("OverlayItems");
        if(pause)
        {
            if(ShopMenu != null)
            {
                if (!ShopMenu.activeInHierarchy)
                {
                    PauseMenu.SetActive(true);
                    if (coinPanel != null)
                        coinPanel.SetActive(false);
                    
                } else
                {
                    ShopMenu.SetActive(false);
                }
            } else {
                PauseMenu.SetActive(true);
                if (coinPanel != null)
                    coinPanel.SetActive(false);
            }
            
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
                itemBackgroundItem1.enabled = true;
                itemBackgroundItem2.enabled = true;
                itemBackgroundItem1Text.enabled = true;
                itemBackgroundItem2Text.enabled = true;
            } 
        }
        else
        {
            itemOverlayBackground.enabled = false;
            itemOverlayItem1.enabled = false;
            itemOverlayItem2.enabled = false;
            itemOverlayItem1Text.enabled = false;
            itemOverlayItem2Text.enabled = false;
            itemBackgroundItem1.enabled = false;
            itemBackgroundItem2.enabled = false;
            itemBackgroundItem1Text.enabled = false;
            itemBackgroundItem2Text.enabled = false;
        }
    }

    public void ContinueGame()
    {
        PauseMenu.SetActive(false);
        if (coinPanel != null)
            coinPanel.SetActive(true);
    }
}
