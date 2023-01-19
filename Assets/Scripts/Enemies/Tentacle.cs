using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    [SerializeField] private int length = 30;
    [SerializeField] private LineRenderer lineRenderer;
    private Vector3[] segmentPoses;
    private Vector3[] segmentVelocity;

    [SerializeField] private Transform targetDir;
    [SerializeField] private float targetDistance = 0.2f;
    [SerializeField] private float smoothSpeed = 0.02f;
    [SerializeField] private float trailSpeed = 350;

    [SerializeField] private float wiggleSpeed = 10;
    [SerializeField] private float wiggleMagnitude = 20;
    [SerializeField] private Transform wiggleDir;
    void Start()
    {
        lineRenderer.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentVelocity = new Vector3[length];
    }

    // Update is called once per frame
    void Update()
    {
        wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);

        segmentPoses[0] = targetDir.position;

        for(int i = 1; i < length; i++)
        {
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i - 1] + targetDir.right * targetDistance, ref segmentVelocity[i], smoothSpeed + i / trailSpeed);
        }
        lineRenderer.SetPositions(segmentPoses);
    }
}
