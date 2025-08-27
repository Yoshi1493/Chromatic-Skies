using System.Collections;
using UnityEngine;
using static BezierHelper;

public class PiscesCommonMovement2 : CommonEnemyMovement
{
    [SerializeField] Vector2[] movementPoints;

    protected override IEnumerator Move()
    {
        float t = 0f;

        while (t < (movementPoints.Length - 1) / 3)
        {
            parentShip.transform.position = EvaluateCubicSpline(movementPoints, t);

            yield return null;
            t += Time.deltaTime;
        }

        yield return LeaveScene();
    }
}