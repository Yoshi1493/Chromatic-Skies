using UnityEngine;

public abstract class EnemyBullet : Bullet
{
    protected abstract Collider2D collisionCondition { get; }

    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<Player>(() => collisionCondition);
    }

    public override void Destroy()
    {
        EnemyBulletPool.Instance.ReturnToPool(bulletIndex, this);
    }    
}