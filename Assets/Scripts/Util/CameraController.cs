using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [SerializeField] private Cinemachine.CinemachineVirtualCamera vCam;

    private void Awake()
    {
        Instance = this;
    }

    public static void SetFollowTarget(Transform followTarget)
    {
        Instance.vCam.Follow = followTarget;
    }
}
