using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatform : MonoBehaviour
{
    [SerializeField] private Transform[] points;

    [SerializeField] private AnimationCurve timeCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private float timeScale = 1f;

    //[SerializeField] private AnimationCurve yCurve = AnimationCurve.Linear(0, 0, 0, 0);
    //[SerializeField] private float yScale = 1f;

    //[SerializeField] private AnimationCurve xCurve = AnimationCurve.Linear(0, 0, 0, 0);
    //[SerializeField] private float xScale = 1f;

    [SerializeField] private bool pingpongPoints = false;

    [SerializeField] private GameObject movingPlatform;
    [SerializeField] private Animator gear1;
    [SerializeField] private Animator gear2;

    private int nextGoal = 0;
    private float currentTime = 0;
    private int pingpongDirection = 1;

    [HideInInspector] public GameObject player;

    private void Awake()
    {
        GameObject start = new GameObject("MoveablePlatformStart");
        start.transform.parent = this.transform;
        start.transform.position = movingPlatform.transform.position;

        points = Util.ArrayAddIfNotContains(points, start.transform);

    }

    public void Update()
    {
        currentTime += Time.deltaTime;
        float percentage = timeCurve.Evaluate(currentTime / timeScale);
        if (currentTime <= timeScale)
        {
            Vector3 oldPos = movingPlatform.transform.position;
            movingPlatform.transform.position = Vector3.Lerp(points[(nextGoal - 1 + points.Length) % points.Length].position, points[nextGoal].position, percentage);

            float gearSpeed = Mathf.Min(1, (movingPlatform.transform.position - oldPos).magnitude *  (1f/Time.deltaTime) / 2f);
            gear1.SetFloat("speed", gearSpeed);
            gear2.SetFloat("speed", gearSpeed);

            if (player != null)
            {
                player.transform.position += movingPlatform.transform.position - oldPos;
            }

        } else
        {
            if (pingpongPoints)
            {
                nextGoal += pingpongDirection;

                if (nextGoal < 0 || nextGoal >= points.Length)
                {
                    pingpongDirection *= -1;
                    nextGoal += 2 * pingpongDirection;
                }
            }
            else
            {
                nextGoal = (nextGoal + 1) % points.Length;
            }
            currentTime = 0;
        }
    }

    
}
