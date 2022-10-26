using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { private set; get; }

    public int playerTransformationIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    public static bool IsPlayer(GameObject go)
    {
        return go == Instance.gameObject;
    }
}
