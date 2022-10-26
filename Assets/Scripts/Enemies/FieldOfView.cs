using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius; //view distance
    [Range(0, 360)] public float angle; //view angle
    public LayerMask targetMask; //layer of player
    public LayerMask obstructionMask; //layer of obstructions
    public Transform eyePos; //postition of enemy's eyes
    [HideInInspector] public bool seePlayer; //player is seen

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider2D rangeCheck = Physics2D.OverlapCircle(eyePos.position, radius, targetMask);

        if (rangeCheck != null)
        {
            Transform target = rangeCheck.transform;
            Vector3 directionToTarget = (target.position - eyePos.position).normalized;

            if (Vector3.Angle(eyePos.right, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(eyePos.position, target.position);

                if (!Physics2D.Raycast(eyePos.position, directionToTarget, distanceToTarget, obstructionMask))
                    seePlayer = true;
                else
                    seePlayer = false;
            }
            else
                seePlayer = false;
        }
        else if (seePlayer)
            seePlayer = false;
    }
}
