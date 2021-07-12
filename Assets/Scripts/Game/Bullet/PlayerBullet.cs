using UnityEngine;

public class PlayerBullet : Bullet
{
    Collider2D collisionCondition => Physics2D.OverlapCircle(transform.position, 0.16f);

    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<Enemy>(() => collisionCondition);
    }

    public override void Destroy()
    {
        PlayerBulletPool.Instance.ReturnToPool(0, this);
    }
}