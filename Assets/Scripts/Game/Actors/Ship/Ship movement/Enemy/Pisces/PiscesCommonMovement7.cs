using System.Collections;
using UnityEngine;
using static BezierHelper;

public class PiscesCommonMovement7 : CommonEnemyMovement
{
    [SerializeField] Vector2[] movementPoints;

    protected override IEnumerator Move()
    {
        for (int i = 0; i < movementPoints.Length; i++)
        {
            movementPoints[i].x *= SignX;
            movementPoints[i].x += transform.position.x;
        }

        float t = 0f;

        while (t < (movementPoints.Length - 1) / 3)
        {
            parentShip.transform.position = EvaluateCubicSpline(movementPoints, t);

            yield return null;
            t += Time.deltaTime / 2f;
        }

        yield return LeaveScene();
    }
}