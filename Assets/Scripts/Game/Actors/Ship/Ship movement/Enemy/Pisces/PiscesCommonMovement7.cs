using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static BezierHelper;

public class PiscesCommonMovement7 : CommonEnemyMovement
{
    [SerializeField] Vector2[] movementPoints;

    protected override IEnumerator Move()
    {
        yield return null;

        for (int i = 0; i < movementPoints.Length; i++)
        {
            movementPoints[i].x *= SignX;
        }

        Vector2 r = Random.insideUnitCircle;
        movementPoints[^2] += r;
        movementPoints[^1] += r;

        AnimationCurve movementInterpolation = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        float t = 0f;

        while (t < (movementPoints.Length - 1) / 3)
        {
            parentShip.transform.position = EvaluateCubicSpline(movementPoints, movementInterpolation.Evaluate(t));

            yield return null;
            t += Time.deltaTime / 2f;
        }

        yield return WaitForSeconds(8f);

        yield return parentShip.TranslateAround(transform.position + (5f * Vector3.up), -SignX * 90f, 2f);
        yield return LeaveScene();
    }
}