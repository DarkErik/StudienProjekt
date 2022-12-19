using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlong : MonoBehaviour
{
    public Vector3 direction;
    public float power;
    public float powerLoss = 1;
    [HideInInspector] public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        transform.position += direction * power * Time.deltaTime;
        power = Mathf.Max(0, power - Time.deltaTime * powerLoss);
    }
}
