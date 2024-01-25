using System.Collections;
using UnityEngine;

public class ScorpioMovementSystem2 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 p0 = parentShip.transform.position;
        yield return this.MoveToRandomPosition(1f, 2f, 2f);

        Vector3 p1 = (1.1f * (p0 - transform.position)) + transform.position;
        yield return this.MoveTo(p1, 1f);

        Vector3 p2 = new(Mathf.Sign(-p1.x) * screenHalfWidth * 1.1f, 0.9f * Random.Range(0f, screenHalfHeight));
        yield return this.MoveTo(p2, 1.5f);

        Vector3 p3 = new(0f, screenHalfHeight * 1.1f);
        Vector3 p4 = new(p3.x, 2.5f);
        yield return this.MoveFromTo(p3, p4, 2f);
    }
}