using System;
using System.Collections.Generic;
using UnityEngine;
using static BezierHelper;

[Serializable]
public class BezierCurve
{
    [SerializeField] List<Vector2> points;

    public BezierCurve(Vector2 centre)
    {
        points = new List<Vector2>
        {
            centre + Vector2.left,
            centre + (Vector2.left + Vector2.up) * 0.5f,
            centre + (Vector2.right + Vector2.down) * 0.5f,
            centre + Vector2.right
        };
    }

    public BezierCurve(int numAnchorPoints)
    {
        if (numAnchorPoints < 2) return;

        points = new List<Vector2>((numAnchorPoints - 1) * 3 + 1);

        for (int i = 0; i < numAnchorPoints; i++)
        {
            points.Add(Vector2.right * i);
            points.Add((Vector2.right * i) + (Vector2.right + Vector2.up) * 0.5f);
            points.Add((Vector2.right * i) + (Vector2.right * 3f + Vector2.down) * 0.5f);
        }

        points.Add(Vector2.right * numAnchorPoints);
    }

    public Vector2 this[int i]
    {
        get => points[i];
        set => points[i] = value;
    }

    public int NumPoints => points.Count;
    public int NumSegments => NumPoints / 3;

    //add new segment to path based on anchor point position
    public void AddSegment(Vector2 anchorPos)
    {
        points.Add(points[^1] * 2 - points[^2]);            //control point for the previous anchor point
        points.Add((points[^1] + anchorPos) * 0.5f);        //control point for the new anchor point
        points.Add(anchorPos);                              //the anchor point itself
    }

    //add segment in between existing anchor points
    public void SplitSegment(Vector2 anchorPos, int segmentIndex)
    {
        Vector2[] newPoints = new Vector2[]
        {
            Vector2.zero,
            anchorPos,
            Vector2.zero
        };

        points.InsertRange(segmentIndex * 3 + 2, newPoints);
        SetAnchorControlPoints(segmentIndex * 3 + 3);
    }

    void SetAnchorControlPoints(int anchorIndex)
    {
        Vector2 anchorPos = points[anchorIndex];
        Vector2 dir = Vector2.zero;
        float[] neighbourDistances = new float[2];

        if (anchorIndex - 3 >= 0)
        {
            Vector2 offset = points[LoopIndex(anchorIndex - 3)] - anchorPos;
            dir += offset.normalized;
            neighbourDistances[0] = offset.magnitude;
        }
        if (anchorIndex + 3 >= 0)
        {
            Vector2 offset = points[LoopIndex(anchorIndex + 3)] - anchorPos;
            dir -= offset.normalized;
            neighbourDistances[1] = -offset.magnitude;
        }

        dir.Normalize();

        for (int i = 0; i < 2; i++)
        {
            int controlIndex = anchorIndex + i * 2 - 1;

            if (controlIndex >= 0 && controlIndex < NumPoints)
            {
                points[LoopIndex(controlIndex)] = anchorPos + 0.5f * neighbourDistances[i] * dir;
            }
        }
    }

    //remove segment from path
    public void RemoveSegment(int anchorIndex)
    {
        if (NumSegments > 2)                                    //make sure there are enough points
        {
            if (anchorIndex == 0)
            {
                points.RemoveRange(0, 3);                       //remove first anchor point, its right control point, and left control point of second anchor point
            }
            else if (anchorIndex == NumPoints - 1)
            {
                points.RemoveRange(anchorIndex - 2, 3);         //remove right control point of second-last anchor point, last anchor point, and its left control point
            }
            else
            {
                points.RemoveRange(anchorIndex - 1, 3);         //remove middle 3 points
            }
        }
    }

    //return array of 4 points in path segment (2 anchor points + their respective control points)
    public Vector2[] GetPointsInSegment(int i)
    {
        return new Vector2[]
        {
            points[i * 3],
            points[i * 3 + 1],
            points[i * 3 + 2],
            points[LoopIndex(i * 3 + 3)]
        };
    }

    public void MovePoint(int i, Vector2 pos)
    {
        Vector2 deltaMovement = pos - points[i];
        points[i] = pos;

        //if the point that moved is an anchor point,
        //move respective control points along with it
        if (i % 3 == 0)
        {
            if (i + 1 < NumPoints)
            {
                points[i + 1] += deltaMovement;
            }
            if (i - 1 >= 0)
            {
                points[i - 1] += deltaMovement;
            }
        }
        //if the point that move is a control point,
        //mirror the control point on the opposite side of the respective anchor point
        else
        {
            bool nextPointIsAnchor = (i + 1) % 3 == 0;          //check if left or right control point is being moved
            int correspondingControlIndex = i + (nextPointIsAnchor ? 2 : -2);
            int anchorIndex = i + (nextPointIsAnchor ? 1 : -1);

            if (correspondingControlIndex >= 0 && correspondingControlIndex < NumPoints)
            {
                points[correspondingControlIndex] = 2f * points[anchorIndex] - pos;
            }
        }
    }

    //return array of evenly spaced points on the bezier curve
    public BezierSpacingPoint[] CalculateEvenlySpacedPoints(float spacing, float resolution = 1f)
    {
        List<BezierSpacingPoint> evenlySpacedPoints = new();

        Vector2 previousPoint = points[0];
        float distSinceLastPoint = 0f;

        for (int i = 0; i < NumSegments; i++)
        {
            Vector2[] p = GetPointsInSegment(i);

            //approximate curve length + split into segments based on <resolution>
            float controlNetLength = Vector2.Distance(p[0], p[1]) + Vector2.Distance(p[1], p[2]) + Vector2.Distance(p[2], p[3]);
            float estimatedCurveLength = Vector2.Distance(p[0], p[3]) + controlNetLength * 0.5f;
            int numDivisions = Mathf.CeilToInt(estimatedCurveLength * resolution * 10f);

            for (float t = 0f; t <= 1f; t += 1f / numDivisions)
            {
                Vector2 pointOnCurve = EvaluateCubic(p[0], p[1], p[2], p[3], t);
                distSinceLastPoint += Vector2.Distance(previousPoint, pointOnCurve);

                //accommodate for potential overshoot
                while (distSinceLastPoint >= spacing)
                {
                    float overshootDistance = distSinceLastPoint - spacing;
                    Vector2 newEvenlySpacedPoint = pointOnCurve + (overshootDistance * (previousPoint - pointOnCurve).normalized);
                    Vector2 directionFromLastPoint = newEvenlySpacedPoint - previousPoint;

                    evenlySpacedPoints.Add(new(newEvenlySpacedPoint, directionFromLastPoint));

                    distSinceLastPoint = overshootDistance;
                    previousPoint = newEvenlySpacedPoint;
                }

                previousPoint = pointOnCurve;
            }
        }

        //insert points[0] to front of list now that its direction is known
        Vector2 firstPointPosition = evenlySpacedPoints[0].position;
        evenlySpacedPoints.Insert(0, new(points[0], firstPointPosition - points[0]));

        return evenlySpacedPoints.ToArray();
    }

    int LoopIndex(int i)
    {
        return (i + NumPoints) % NumPoints;
    }
}

public struct BezierSpacingPoint
{
    public Vector2 position;
    public Vector2 direction;
    public Vector2 normal;

    public BezierSpacingPoint(Vector2 _position, Vector2 _direction)
    {
        position = _position;
        direction = _direction.normalized;
        normal = new(-_direction.y, _direction.x);
    }
}