using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static LayerMask playerLayer = -1;
    public static PlayerController Instance { private set; get; }

    [SerializeField] private int uuid = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CameraController.SetFollowTarget(transform);
    }
    public int getUUID()
    {
        return uuid;
    }
    public static bool IsPlayer(GameObject go)
    {


        return go == Instance.gameObject || (Instance.uuid == 3 && go.transform.parent != null && go.transform.parent.gameObject == Instance.gameObject);
        //if (playerLayer == -1) playerLayer = LayerMask.GetMask("Player");
        //return go.layer == playerLayer;
    }
}
