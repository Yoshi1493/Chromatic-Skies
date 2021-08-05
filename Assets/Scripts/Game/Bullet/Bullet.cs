using UnityEngine;

public abstract class Bullet : Projectile
{
    protected int bulletIndex;

    protected const float MaxLifetime = 10f;
    float currentLifetime;

    float HitboxSize => spriteRenderer.size.x / 2;
    protected override Collider2D CollisionCondition => Physics2D.OverlapCircle(transform.position, HitboxSize);

    protected virtual void OnEnable()
    {
        currentLifetime = 0;
    }

    protected virtual void Update()
    {
        Move(MoveSpeed);

        currentLifetime += Time.deltaTime;
        if (currentLifetime > MaxLifetime) Destroy();
    }

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

    public abstract override void Destroy();
}