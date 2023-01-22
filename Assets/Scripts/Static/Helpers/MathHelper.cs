using System.Collections.Generic;
using UnityEngine;

public static class MathHelper
{
    #region Math

    /// <summary>
    /// returns <f> rounded to the nearest multiple of <n>.
    /// always rounds up if <f> is exactly halfway between two multiples of <n>
    /// </summary>
    public static float RoundToNearestMultipleOf(float f, float n = 1)
    {
        float m = f % n;
        if (m == 0 || n < 0) return f;

        int d = (int)(f / n);

        if (m < n * 0.5f)
        {
            return d * n;
        }
        else
        {
            return (d + 1) * n;
        }
    }

    /// <summary>
    /// returns bezier curve coordinate along line [<v1>, <v2>] and line [<v2>, <v3>], based on lerp progress <t>
    /// </summary>
    public static Vector3 QuadraticBezierCurve(Vector3 v1, Vector3 v2, Vector3 v3, float t)
    {
        return Vector3.Lerp(Vector3.Lerp(v1, v2, t), Vector3.Lerp(v2, v3, t), t);
    }

    #endregion

    #region Vector

    /// <summary>
    /// returns the angle (in degrees) that the line created by <pos1> and <pos2> subtends from (0, 0).
    /// </summary>
    public static float GetRotationDifference(this Vector3 pos1, Vector3 pos2)
    {
        Vector3 distance = pos2 - pos1;
        return Mathf.Atan2(-distance.x, distance.y) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// rotates <v> anticlockwise by <theta> degrees along the x-y plane.
    /// </summary>
    public static Vector3 RotateVectorBy(this Vector3 v, float theta)
    {
        Vector3 _v = v;
        float theta_r = theta * Mathf.Deg2Rad;

        v.x = (Mathf.Cos(theta_r) * _v.x) - (Mathf.Sin(theta_r) * _v.y);
        v.y = (Mathf.Sin(theta_r) * _v.x) + (Mathf.Cos(theta_r) * _v.y);

        return v;
    }

    /// <summary>
    /// overload of RotateVectorBy() which allows <v> to be passed by reference instead
    /// </summary>
    public static void RotateVectorBy(ref Vector3 v, float theta)
    {
        Vector3 _v = v;
        float theta_r = theta * Mathf.Deg2Rad;

        v.x = (Mathf.Cos(theta_r) * _v.x) - (Mathf.Sin(theta_r) * _v.y);
        v.y = (Mathf.Sin(theta_r) * _v.x) + (Mathf.Cos(theta_r) * _v.y);
    }

    /// <summary>
    /// returns true if <v> and <pos> are less than <dist> units apart
    /// </summary>
    public static bool IsTooClose(this Vector2 v, Vector2 pos, float minDistance = 1f)
    {
        return (pos - v).sqrMagnitude < minDistance;
    }

    #endregion

    #region Random

    public static int PositiveOrNegativeOne => Random.value > 0.5f ? -1 : 1;
    public static float RandomAngleDeg => Random.Range(0f, 360f);

    public static List<Vector2> GetRandomPointsAlongBounds(Vector2 minBoundary, Vector2 maxBoundary, float minSpacing = 1f, float maxSpacing = 3f)
    {
        List<Vector2> points = new();
        if (minSpacing < 0f || maxSpacing < 0f || maxSpacing < minSpacing) return points;

        float x, y;

        if (minBoundary.x < maxBoundary.x)
        {
            x = minBoundary.x;

            do
            {
                x += Random.Range(minSpacing, maxSpacing);
                y = Random.value > 0.5f ? maxBoundary.y : minBoundary.y;

                points.Add(new Vector2(x, y));
            }
            while (x < maxBoundary.x);
        }

        if (minBoundary.y < maxBoundary.y)
        {
            y = minBoundary.y;

            do
            {
                x = Random.value > 0.5f ? maxBoundary.x : minBoundary.x;
                y += Random.Range(minSpacing, maxSpacing);

                points.Add(new Vector2(x, y));
            }
            while (y < maxBoundary.y);
        }

        return points;
    }

    public static List<Vector2> GetRandomPointsWithinBounds(Vector2 minBoundary, Vector2 maxBoundary, int pointCount)
    {
        List<Vector2> points = new(pointCount);
        if (minBoundary.x > maxBoundary.x || minBoundary.y > maxBoundary.y) return points;

        float x, y;

        for (int i = 0; i < pointCount; i++)
        {
            x = Random.Range(minBoundary.x, maxBoundary.x);
            y = Random.Range(minBoundary.y, maxBoundary.y);

            points.Add(new Vector2(x, y));
        }

        return points;
    }


    public static void Randomize<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            int r = Random.Range(i, list.Count);
            var temp = list[i];
            list[i] = list[r];
            list[r] = temp;
        }
    }

    #endregion

    static void print(object message) { Debug.Log(message); }
}