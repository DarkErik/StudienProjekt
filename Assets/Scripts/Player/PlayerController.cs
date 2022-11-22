using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { private set; get; }

    [SerializeField] private int uuid = 0;

    private void Awake()
    {
        Instance = this;
        CameraController.SetFollowTarget(transform);
    }

    public int getUUID()
    {
        return uuid;
    }
    public static bool IsPlayer(GameObject go)
    {
        return go == Instance.gameObject;
    }
}
