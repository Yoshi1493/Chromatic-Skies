using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusMovementSystem6 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 p1 = parentShip.transform.position;
        Vector3 p2 = new(screenHalfWidth * 1.5f * Mathf.Sign(p1.x), 5f);
        Vector3 p3 = new(-p2.x, 0.8f * Random.Range(0f, screenHalfHeight));
        Vector3 p4 = new(p2.x, Random.Range(0f, screenHalfHeight));
        Vector3 p5 = new(Random.Range(-screenHalfWidth, screenHalfWidth) * 0.5f, screenHalfHeight * 1.1f);
        Vector3 p6 = new(p5.x, 2.5f);

        yield return this.MoveTo(p2, 2f);
        yield return this.MoveToLinear(p3, 1.5f);
        yield return WaitForSeconds(0.5f);

        yield return this.MoveToLinear(p4, 1.5f);
        yield return WaitForSeconds(1f);

        yield return this.MoveFromTo(p5, p6, 1f);
    }
}