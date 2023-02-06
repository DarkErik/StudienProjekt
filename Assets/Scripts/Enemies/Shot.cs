using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{

    private static LinkedList<Shot> shots = new LinkedList<Shot>();
    [HideInInspector] public float speed;

    private void Awake()
    {
        shots.AddLast(this);
    }


    public void Init(float speed, float dir)
    {
        this.speed = speed;
        transform.localRotation = Quaternion.Euler(0,0, dir);

    }

    public void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    public static void DestroyAllShots()
    {
        foreach(Shot shot in shots)
        {
            if (shot != null)
                Destroy(shot.gameObject);
        }
        shots.Clear();
    }
}
