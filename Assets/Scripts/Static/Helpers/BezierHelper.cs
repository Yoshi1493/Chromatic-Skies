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
}