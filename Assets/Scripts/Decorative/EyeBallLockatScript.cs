using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBallLockatScript : MonoBehaviour
{
    [SerializeField] private float distance = 0.155f;

    public void Update()
    {
        transform.localPosition = PlayerController.Instance.transform.localPosition;
        transform.localPosition = Vector3.ClampMagnitude(transform.localPosition, distance);
    }
}
