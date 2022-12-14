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

    [SerializeField] private bool onlyMoveWhenPlayerInTrigger = false;
    [SerializeField] private bool pingpongPoints = false;

    [SerializeField] private GameObject movingPlatform;
    [SerializeField] private Animator gear1;
    [SerializeField] private Animator gear2;

    [SerializeField] private float velocityConst;

    private int nextGoal = 0;
    private float currentTime = 0;
    private int pingpongDirection = 1;

    [HideInInspector] public LinkedList<MoveAlong> ontopObjects = new LinkedList<MoveAlong>();
    private bool oldPlayerInTrigger = true;
    [HideInInspector] public bool newPlayerInTrigger;

    private void Awake()
    {
        if (onlyMoveWhenPlayerInTrigger) pingpongPoints = true;

        GameObject start = new GameObject("MoveablePlatformStart");
        start.transform.parent = this.transform;
        start.transform.position = movingPlatform.transform.position;

        points = Util.ArrayAddIfNotContains(points, start.transform);

    }

    public void Update()
    {
        if (onlyMoveWhenPlayerInTrigger && oldPlayerInTrigger && !newPlayerInTrigger)
        {
            oldPlayerInTrigger = newPlayerInTrigger;
            currentTime = Mathf.Max(0, timeScale - currentTime);
            pingpongDirection = -1;
            //if (nextGoal != points.Length - 1)
                nextGoal = (nextGoal - 1 + points.Length) % points.Length;


        } else if(onlyMoveWhenPlayerInTrigger && !oldPlayerInTrigger && newPlayerInTrigger)
        {
            oldPlayerInTrigger = newPlayerInTrigger;
            currentTime = Mathf.Max(0, timeScale - currentTime);
            pingpongDirection = 1;
            //if (nextGoal != points.Length - 2)
                nextGoal = (nextGoal + 1) % points.Length;
        }
        

        currentTime += Time.deltaTime;
        float percentage = timeCurve.Evaluate(currentTime / timeScale);
        if (currentTime <= timeScale)
        {
            Vector3 oldPos = movingPlatform.transform.position;

            movingPlatform.transform.position = Vector3.Lerp(points[(nextGoal - pingpongDirection + points.Length) % points.Length].position, points[nextGoal].position, percentage);

            float gearSpeed = Mathf.Min(1, (movingPlatform.transform.position - oldPos).magnitude *  (1f/Time.deltaTime) / 2f);
            if (!float.IsNaN(gearSpeed))
            {
                gear1.SetFloat("speed", gearSpeed);
                gear2.SetFloat("speed", gearSpeed);
            }

            Vector3 moveDir = (movingPlatform.transform.position - oldPos).normalized;
            float movePower = (movingPlatform.transform.position - oldPos).magnitude * (1 / Time.deltaTime);
            foreach (MoveAlong move in ontopObjects) 
            {

                move.direction = moveDir;
                move.power = movePower;
                
                //else
                //{

                //    player.transform.position += movingPlatform.transform.position - oldPos;
                //}


            }

        } else
        {
            if (pingpongPoints)
            {
                int oldGoal = nextGoal;
                nextGoal += pingpongDirection + points.Length;
                nextGoal %= points.Length;
           

                if (!onlyMoveWhenPlayerInTrigger)
                {
                    if (pingpongDirection == 1 && oldGoal == points.Length - 2)
                    {
                        pingpongDirection *= -1;
                        nextGoal = (points.Length - 3 + points.Length) % points.Length;
                    }
                    else if (pingpongDirection == -1 && oldGoal == points.Length - 1)
                    {
                        pingpongDirection *= -1;
                        nextGoal = 0;
                    }
                    currentTime = 0;
                } else
                {
                    if (pingpongDirection == 1 && nextGoal >= points.Length - 1 && oldGoal == points.Length - 2)
                    {
                        nextGoal = points.Length - 2;
                    }
                    else if (pingpongDirection == -1 && oldGoal == points.Length - 1)
                    {
                        nextGoal = points.Length - 1;
                    } else
                    {
                        currentTime = 0;
                    }
                }
            }
            else
            {
                nextGoal = (nextGoal + 1) % points.Length;
                currentTime = 0;
            }
            
        }
    }


}
