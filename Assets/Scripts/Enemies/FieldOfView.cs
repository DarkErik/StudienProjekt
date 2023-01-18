using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius = 7; //view distance
    public float additionalIndicationRadius = 3; 
    [Range(0, 360)] public float angle; //view angle
    public LayerMask targetMask; //layer of player
    public LayerMask obstructionMask; //layer of obstructions
    public Transform eyePos; //postition of enemy's eyes
    [HideInInspector] public bool seePlayer; //player is seen

    private void Start()
    {
        radius = radius * float.Parse(PlayerPrefs.GetString("scientistWeaker", "1"));

        //StartCoroutine(FOVRoutine());
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

    public void Update()
    {
        FieldOfViewCheck();
    }

    private void FieldOfViewCheck()
    {
        Collider2D rangeCheck = Physics2D.OverlapCircle(eyePos.position, radius + additionalIndicationRadius, targetMask);

        if (rangeCheck != null)
        {
            Transform target = rangeCheck.transform;
            Vector3 directionToTarget = (target.position - eyePos.position);
            float distance = directionToTarget.magnitude;
            directionToTarget.Normalize();

            if (Vector3.Angle(eyePos.right, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(eyePos.position, target.position);

                if (!Physics2D.Raycast(eyePos.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        if (distance <= radius)
                        {
                            seePlayer = true;
                        }
                        PostProcessingScientist.Instance.TellIntensity(1 - ((distance - radius) / additionalIndicationRadius));
                    } else
                        seePlayer = false;
            }
            else
                seePlayer = false;
        }
        else if (seePlayer)
            seePlayer = false;
    }


    public void OnDrawGizmos()
    {
        float radii = radius + additionalIndicationRadius;
        Gizmos.DrawWireSphere(eyePos.position, radius);
        Gizmos.DrawWireSphere(eyePos.position, radii);
        Gizmos.DrawLine(eyePos.position, eyePos.position + new Vector3(Mathf.Cos(angle / 2), Mathf.Sin(angle/2))*radii);
        Gizmos.DrawLine(eyePos.position, eyePos.position + new Vector3(Mathf.Cos(angle / 2), -Mathf.Sin(angle/2))*radii); 
        Gizmos.DrawLine(eyePos.position, eyePos.position + new Vector3(-Mathf.Cos(angle / 2), Mathf.Sin(angle/2))*radii);
        Gizmos.DrawLine(eyePos.position, eyePos.position + new Vector3(-Mathf.Cos(angle / 2), -Mathf.Sin(angle/2))*radii);
    }
}
