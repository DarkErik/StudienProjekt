using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasActivate : MonoBehaviour
{
    float pause = 0f;
    float overlay = 0f;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject ItemOverlay;

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
                ItemOverlay.SetActive(true);
            } 
        }
        else
        {
            ItemOverlay.SetActive(false);
        }
    }

    public void ContinueGame()
    {
        PauseMenu.SetActive(false);
    }
}
