using System;
using System.Collections.Generic;
using UnityEngine;
using static BezierHelper;

[Serializable]
public class BezierCurve
{
    [SerializeField] List<Vector2> points;

    public BezierCurve()
    {
        points = new List<Vector2>
        {
            Vector2.left,
            (Vector2.left + Vector2.up) * 0.5f,
            (Vector2.right + Vector2.down) * 0.5f,
            Vector2.right
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