using UnityEngine;

public static class BezierHelper
{
    public static Vector2 EvaluateQuadratic(Vector2 v1, Vector2 v2, Vector2 v3, float t)
    {
        Vector2 p0 = Vector2.Lerp(v1, v2, t);
        Vector2 p1 = Vector2.Lerp(v2, v3, t);
        return Vector2.Lerp(p0, p1, t);
    }

    public static Vector2 EvaluateCubic(Vector2 v1, Vector2 v2, Vector2 v3, Vector2 v4, float t)
    {
        Vector2 p0 = EvaluateQuadratic(v1, v2, v3, t);
        Vector2 p1 = EvaluateQuadratic(v2, v3, v4, t);
        return Vector2.Lerp(p0, p1, t);
    }

    public static Vector2 EvaluateCubicSpline(Vector2[] points, float t)
    {
        if ((points.Length - 1) % 3 != 0 || t < 0 || t > (points.Length * 3) + 1)
        {
            return Vector2.zero;
        }

        float f = t % 1;
        int i = Mathf.FloorToInt(t);

        if (f == 0f)
        {
            return points[i * 3];
        }
        else
        {
            return EvaluateCubic(points[i * 3], points[(i * 3) + 1], points[(i * 3) + 2], points[(i + 1) * 3], f);
        }
    }
}