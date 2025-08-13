using System.Collections;
using UnityEngine;
using PathCreation;
using static CoroutineHelper;
using static BezierHelper;

public class CapricornCommonMovement7 : CommonEnemyMovement
{
    [SerializeField] Vector2[] movementPoints;

    protected override IEnumerator Move()
    {
        Vector2 originalPosition = parentShip.transform.position;

        float t = 0f;

        while (t < (movementPoints.Length - 1) / 3)
        {
            parentShip.transform.position = originalPosition + EvaluateCubicSpline(movementPoints, t);

            yield return null;
            t += Time.deltaTime / 1f;
        }

        yield return WaitForSeconds(3f);

        yield return LeaveScene();
    }
}