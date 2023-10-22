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

        Vector3 p2 = new(screenHalfWidth * 1.2f * PositiveOrNegativeOne, screenHalfHeight * Random.Range(0.25f, 0.5f));
        Vector3 p3 = new Vector3(p2.x, 0f).RotateVectorBy(Random.Range(-15f, 15f));
        p3.x = -p2.x;
        p3.y += p2.y;

        yield return this.MoveFromTo(p2, p3, 2f);

        Vector3 p4 = new(PositiveOrNegativeOne * screenHalfWidth * Random.Range(0.5f, 0.75f), screenHalfHeight * 1.1f);
        Vector3 p5 = p4.RotateVectorBy(Random.Range(-15f, 15f));
        p5.y = -p4.y;

        yield return this.MoveFromTo(p4, p5, 1.5f);

        Vector3 p6 = new(-Mathf.Sign(p4.x) * screenHalfWidth * Random.Range(0.25f, 0.5f), screenHalfHeight * 1.1f);
        Vector3 p7 = new(p6.x, 2.5f);

        yield return this.MoveFromTo(p6, p7, 2f);

        yield return WaitForSeconds(5f);

        yield return this.MoveToRandomPosition(1f);
    }
}