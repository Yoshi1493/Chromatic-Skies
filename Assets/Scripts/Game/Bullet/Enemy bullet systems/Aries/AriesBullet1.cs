using System.Collections;
using UnityEngine;

public class AriesBullet1 : EnemyBullet
{
    protected override Collider2D collisionCondition { get => Physics2D.OverlapCircle(transform.position, 0.16f); }

    protected override IEnumerator Move()
    {
        StartCoroutine(this.ChangeSpeed(3, 1, 1));
        //yield return this.RotateAround();

        while (true)
        {
            yield return this.RotateBy(120, 1, 0);
            yield return this.RotateBy(-120, 1, 0);
        }
    }
}