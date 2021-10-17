using UnityEngine;

public abstract class Bullet : Projectile
{
    float HitboxSize => spriteRenderer.size.x / 2;
    protected override Collider2D CollisionCondition => Physics2D.OverlapCircle(transform.position, HitboxSize);

    protected override void HandleCollisionWithShip<TShip>(Collider2D coll)
    {
        base.HandleCollisionWithShip<TShip>(coll);
        Destroy();
    }

    protected override void Move(float moveSpeed)
    {
        base.Move(moveSpeed);
        transform.eulerAngles = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg * Vector3.forward;
    }
}