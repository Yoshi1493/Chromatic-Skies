using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static BezierHelper;

public class PiscesCommonMovement2 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Player player = FindObjectOfType<Player>();
        float d = Mathf.Sign(player.transform.position.x - transform.position.x);

        AnimationCurve movementInterpolation = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        Vector2[] movementPoints = new Vector2[]
        {
            transform.position,
            transform.position + (d * 2f * Vector3.right),
            new(transform.position.x + (d * 4f), 3f),
            new(transform.position.x + (d * 4f), 1f)
        };

        float t = 0f;

        while (t < (movementPoints.Length - 1) / 3)
        {
            parentShip.transform.position = EvaluateCubicSpline(movementPoints, movementInterpolation.Evaluate(t));

            yield return null;
            t += Time.deltaTime / 2f;
        }

        yield return WaitForSeconds(1.5f);

        movementPoints[0] = transform.position;
        movementPoints[1] = transform.position + (2f * Vector3.up);
        movementPoints[2] = new(transform.position.x + (d * 2f), 6f);
        movementPoints[3] = new(transform.position.x + (d * 4f), 6f);

        t = 0f;

        while (t < (movementPoints.Length - 1) / 3)
        {
            parentShip.transform.position = EvaluateCubicSpline(movementPoints, movementInterpolation.Evaluate(t));

            yield return null;
            t += Time.deltaTime / 2f;
        }

        yield return LeaveScene();
    }
}