using System.Collections;
using UnityEngine;
using static BezierHelper;

public class PiscesCommonMovement1 : CommonEnemyMovement
{
    [SerializeField] Vector2[] movementPoints;

    protected override IEnumerator Move()
    {
        for (int i = 0; i < movementPoints.Length; i++)
        {
            movementPoints[i].x *= SignX;
            movementPoints[i].x += transform.position.x;
        }

        AnimationCurve movementInterpolation = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        float t = 0;

        while (t < (movementPoints.Length - 1) / 3)
        {
            parentShip.transform.position = EvaluateCubicSpline(movementPoints, t);

            yield return null;
            t += Time.deltaTime / 2f;
        }

        yield return LeaveScene();
    }
}