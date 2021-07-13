using System.Collections;
using UnityEngine;

public class AriesBullet1 : EnemyBullet
{
    protected override Collider2D collisionCondition { get => Physics2D.OverlapCircle(transform.position, 0.16f); }

    protected override IEnumerator Move()
    {
        StartCoroutine(this.ChangeSpeed(3, 1, 1));

        while (true)
        {
            StartCoroutine(this.RotateBy(60, 1, 0));
            StartCoroutine(this.RotateBy(-60, 1, 1));
            yield return CoroutineHelper.WaitForSeconds(1f);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (movementBehaviour != null) StopCoroutine(movementBehaviour);

        movementBehaviour = Move();
        StartCoroutine(movementBehaviour);
    }
}