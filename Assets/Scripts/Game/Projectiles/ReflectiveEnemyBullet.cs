using UnityEngine;

public abstract class ReflectiveEnemyBullet : EnemyBullet
{
    protected virtual int MaxReflectCount => 1;
    int currentReflectCount;

    const float ReflectCollisionThreshold = 0.1f;

    protected override int CollisionMask => base.CollisionMask | 1 << LayerMask.NameToLayer("Bullet bounds");

    protected override void OnEnable()
    {
        base.OnEnable();

        currentReflectCount = MaxReflectCount;
        spriteRenderer.color = projectileData.gradient.Evaluate(0f);
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<EdgeCollider2D>();
    }

    protected override void HandleCollision<T>(Collider2D coll)
    {
        base.HandleCollision<T>(coll);

        if (currentReflectCount > 0)
        {
            HandleReflection(coll);
            currentReflectCount--;
        }
    }

    protected virtual void HandleReflection(Collider2D coll)
    {
        Vector3 p = coll.ClosestPoint(transform.position);
        Vector3 d = moveDirection;

        if (Mathf.Abs(p.x - transform.position.x) < ReflectCollisionThreshold)
        {
            d.y *= -1;
        }
        if (Mathf.Abs(p.y - transform.position.y) < ReflectCollisionThreshold)
        {
            d.x *= -1;
        }

        moveDirection = d;
    }
}