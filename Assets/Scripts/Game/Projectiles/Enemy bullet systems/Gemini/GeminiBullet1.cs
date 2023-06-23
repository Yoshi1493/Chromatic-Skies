using System.Collections;
using UnityEngine;

public class GeminiBullet1 : EnemyBullet
{
    protected override int CollisionMask => base.CollisionMask | 1 << LayerMask.NameToLayer("Bullet bounds");

    const int ReflectCount = 1;
    int reflectCount = ReflectCount;
    const float ReflectCollisionThreshold = 0.1f;

    protected override void OnEnable()
    {
        base.OnEnable();
        reflectCount = ReflectCount;
    }

    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        yield return null;
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<EdgeCollider2D>();
    }

    protected override void HandleCollision<T>(Collider2D coll)
    {
        base.HandleCollision<T>(coll);

        if (reflectCount > 0)
        {
            Reflect(coll);
            reflectCount--;
        }
    }

    void Reflect(Collider2D coll)
    {
        Vector3 p = coll.ClosestPoint(transform.position);
        Vector3 d = moveDirection;

        if (Mathf.Abs(p.x - transform.position.x) < ReflectCollisionThreshold)
        {
            d.x *= -1;
        }
        if (Mathf.Abs(p.y - transform.position.y) < ReflectCollisionThreshold)
        {
            d.y *= -1;
        }

        moveDirection = d;
    }
}