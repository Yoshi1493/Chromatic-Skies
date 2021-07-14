using System.Collections;
using UnityEngine;

public abstract class EnemyBullet : Bullet
{
    protected abstract Collider2D collisionCondition { get; }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (movementBehaviour != null) StopCoroutine(movementBehaviour);

        movementBehaviour = Move();
        StartCoroutine(movementBehaviour);
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<Player>(() => collisionCondition);
    }

    public override void Destroy()
    {
        EnemyBulletPool.Instance.ReturnToPool(bulletIndex, this);
    }

    protected abstract IEnumerator Move();
}