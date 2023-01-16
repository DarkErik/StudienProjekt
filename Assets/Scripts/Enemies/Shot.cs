using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [HideInInspector] public float speed;

    public void Init(float speed, float dir)
    {
        this.speed = speed;
        transform.localRotation = Quaternion.Euler(0,0, dir);

    }

    public void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
