using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public static Factory Instance { get; private set; }


    public GameObject[] playerTransformations;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetPlayerTransformation(int uuid)
    {
        foreach(GameObject transform in playerTransformations)
        {
            if (transform.GetComponent<PlayerController>().getUUID() == uuid) return transform;
        }
        Debug.LogError("Unkown UUID");
        return null;
    }
}
