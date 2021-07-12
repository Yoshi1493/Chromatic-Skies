using UnityEngine;

public class AriesBullet1 : EnemyBullet
{
    protected override Collider2D collisionCondition { get => Physics2D.OverlapCircle(transform.position, 0.16f); }

    protected override void OnEnable()
    {
        base.OnEnable();
        RotateBy(360f, 5f);
    }
}