using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusMovementSystem4 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);

        Vector3 p1 = parentShip.transform.position;
        Vector3 p2 = new(screenHalfWidth * 1.5f * Mathf.Sign(p1.x), 5f);
        Vector3 p3 = new(-p2.x, Random.Range(0f, screenHalfHeight));
        Vector3 p4 = new(p2.x, Random.Range(0f, screenHalfHeight));
        Vector3 p5 = new(Random.Range(-screenHalfWidth, screenHalfWidth) * 0.5f, screenHalfHeight * 1.1f);
        Vector3 p6 = new(p5.x, 2.5f);

        float t1 = 2f;
        float t2 = 1.5f;
        float t3 = 1f;

        yield return this.MoveTo(p2, t1);
        yield return this.MoveToLinear(p3, t2);
        yield return WaitForSeconds(0.5f);

        yield return this.MoveToLinear(p4, t2);
        yield return WaitForSeconds(1f);

        yield return this.MoveFromTo(p5, p6, t3);
    }
}