using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemPerfomer : Performer
{
    [SerializeField] private GameObject item;
    [SerializeField] private Transform from;
    [SerializeField] private Transform to;
    [SerializeField] private AnimationCurve height = AnimationCurve.Constant(0, 1, 0);
    [SerializeField] private float heightScale = 1;
    [SerializeField] public float time = 1;

    private bool alreadyDroped = false;

    private ItemFoundPerfomer itemFoundPerfomer;

    private void Awake()
    {
        itemFoundPerfomer = item.GetComponentInChildren<ItemFoundPerfomer>();
    }


    public void Drop() {
        if (!alreadyDroped && (itemFoundPerfomer == null || !PlayerData.instance.IsFlagSet(ItemFoundPerfomer.ITEM_FLAG_PREFIX + itemFoundPerfomer.itemName)))
        {
            alreadyDroped = true;
            item.SetActive(true);
            StartCoroutine(InterpolateItem());
        }
    }

    private IEnumerator InterpolateItem()
    {
        float currentTime = 0;
        while (currentTime < time)
        {
            item.transform.position = Vector3.Lerp(from.position, to.position, currentTime / time) + Vector3.up * height.Evaluate(currentTime / time) * heightScale;
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        }

        if (item != null)
        item.transform.position = to.position;
    }

    protected override void OnUpdate()
    {
        Drop();
    }

    public override void OnTap(Trigger triggerData)
    {
        Drop();
    }
}
