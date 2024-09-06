using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static BezierHelper;

public class CapricornMovementSystem3 : EnemyMovement
{
    [SerializeField] AnimationCurve movementCurve;

    protected override IEnumerator Move()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return this.MoveToRandomPosition(1f, 1.5f, 3f);
        }

        yield return WaitForSeconds(1f);

        float currentLerpTime = 0f;
        float totalLerpTime = 2.5f;

        Vector3 p0 = transform.position;
        Vector3 p2 = parentShip.transform.position.GetRandomPositionWithinBounds(parentShip.shipData.boundaryLayer, 3f, 5f);
        float x1 = p0.y > p2.y ? p2.x : p0.x;
        float y1 = p0.y > p2.y ? p0.y : p2.y;
        Vector3 p1 = new(x1, y1);

        while (currentLerpTime < totalLerpTime)
        {
            float t = movementCurve.Evaluate(currentLerpTime / totalLerpTime);
            parentShip.transform.position = EvaluateQuadratic(p0, p1, p2, t);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        parentShip.transform.position = p2;
    }
}