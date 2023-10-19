using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CapricornMovementSystem6 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 p1 = new(transform.position.x, screenHalfHeight * 1.2f);
        
        yield return this.MoveTo(p1, 1f);

        Vector3 p2 = new(screenHalfWidth * 1.2f * PositiveOrNegativeOne, Random.Range(-screenHalfHeight, screenHalfHeight) * 0.2f);
        Vector3 p3 = p2.RotateVectorBy(Random.Range(-15f, 15f));
        p3.x = -p2.x;

        yield return this.MoveFromTo(p2, p3, 2f);

        Vector3 p4 = new(Random.Range(-screenHalfWidth, screenHalfWidth) * 0.2f, screenHalfHeight * -1.1f);
        Vector3 p5 = p4.RotateVectorBy(Random.Range(-15f, 15f));
        p5.y = -p4.y;

        yield return this.MoveFromTo(p4, p5, 1.5f);

        Vector3 p6 = new(Random.Range(-screenHalfWidth, screenHalfWidth) * 0.5f, screenHalfHeight * 1.1f);
        Vector3 p7 = new(p6.x, 2.5f);

        yield return this.MoveFromTo(p6, p7, 2f);

        yield return WaitForSeconds(5f);
    }
}