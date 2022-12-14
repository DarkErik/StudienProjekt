using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemFoundPerfomer : Performer
{
    public static readonly string ITEM_FLAG_PREFIX = "item#";

    [SerializeField] private Animator canvasAnim;
    [SerializeField] private Image itemImg;
    [SerializeField] private TextMeshProUGUI itemNameTxt;

    [SerializeField] public string itemName;
    [SerializeField] private Sprite itemSprite;

    public void Awake()
    {
        itemImg.sprite = itemSprite;
        itemNameTxt.text = itemName;

        if (PlayerData.instance.IsFlagSet(ITEM_FLAG_PREFIX + itemName))
        {
            this.gameObject.SetActive(false);
        }
    }

    protected override void OnUpdate()
    {
    }

    public override void OnTap(Trigger triggerData)
    {
        PlayerData.instance.SetFlag(ITEM_FLAG_PREFIX + itemName);
        StartCoroutine(CloseCanvasCoroutine());
    }

    private IEnumerator CloseCanvasCoroutine()
    {
        PlayerMovement.Shutdown();
        Time.timeScale = 0;
        canvasAnim.gameObject.SetActive(true);
        
        yield return new WaitForSecondsRealtime(2f);
        yield return new WaitUntil(() => Input.GetButton("Jump"));
        canvasAnim.SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        PlayerMovement.WakeUp();
        this.gameObject.SetActive(false);
    }
}
