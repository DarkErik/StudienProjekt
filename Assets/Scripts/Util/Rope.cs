using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    protected static readonly Vector2 GRAVITY = new Vector2(0, -1);

    [SerializeField] protected LineRenderer render;
    protected List<Segment> ropeSegments = new List<Segment>();
    [SerializeField] protected float ropeSegmentLength = 0.2f;
    [SerializeField] protected int simulationSteps = 30;
    protected System.Action onSnap;

    public Vector2 startPosition;

    void Start()
    {
        Init(Vector2.zero, 35, null);   
    }

    void Update()
    {
        Simulate(Time.deltaTime);
        DrawRope();
    }

    public void Init(Vector2 endPos, int segmentAmount, System.Action onSnap)
    {
        this.onSnap = onSnap;
        this.startPosition = endPos;

        for (int i = 0; i < segmentAmount; i++)
        {
            ropeSegments.Add(new Segment(endPos));
            endPos.y -= ropeSegmentLength;
        }
    }

    protected void Simulate(float deltaTime)
    {

        for(int i = 0; i < ropeSegments.Count; i++)
        {
            Segment currentSeg = this.ropeSegments[i];
            Vector2 velocity = currentSeg.currentPos - currentSeg.oldPos;
            currentSeg.oldPos = currentSeg.currentPos;
            currentSeg.currentPos += velocity + GRAVITY * deltaTime;


        }

        if ((Util.V2ToV3(startPosition ) - transform.position).sqrMagnitude > Util.square(ropeSegmentLength * ropeSegments.Count))
        {
            if (onSnap != null) onSnap();
        }

        for(int i = 0; i < simulationSteps; i++)
        {
            ApplyConstrains();
        }
    }

    protected void ApplyConstrains()
    {

        ropeSegments[0].currentPos = startPosition;
        ropeSegments[ropeSegments.Count - 1].currentPos = transform.position;


        for(int i = 0; i < ropeSegments.Count - 1; i++)
        {
            Segment current = ropeSegments[i];
            Segment next = ropeSegments[i + 1];

            float dist = (current.currentPos - next.currentPos).magnitude;
            float error  = Mathf.Abs(dist - ropeSegmentLength);

            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSegmentLength)
            {
                changeDir = (current.currentPos - next.currentPos).normalized;
            } else //if(DistanceJoint2D < ropeSegmentLength)
            {
                changeDir = (next.currentPos - current.currentPos).normalized;
            }

            Vector2 changeAmount = changeDir * error;

            if (i != 0)
            {
                current.currentPos -= changeAmount * 0.5f;
                next.currentPos += changeAmount * 0.5f;
            } else
            {
                next.currentPos += changeAmount;
            }
        }
    }

    protected void DrawRope()
    {
        Vector3[] ropePos = new Vector3[ropeSegments.Count];
        for(int i = 0; i < ropeSegments.Count; i++)
        {
            ropePos[i] = this.ropeSegments[i].currentPos;
        }
        render.positionCount = ropeSegments.Count;
        render.SetPositions(ropePos);
    }

    protected class Segment
    {
        public Vector2 currentPos;
        public Vector2 oldPos;

        public Segment(Vector2 startPos)
        {
            currentPos = startPos;
            oldPos = startPos;
        }
    }
}
