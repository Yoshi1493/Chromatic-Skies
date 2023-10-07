using UnityEngine;

public class TaurusLaser60 : Laser
{
    Transform originalParent;
    protected override int CollisionMask => base.CollisionMask | 1 << LayerMask.NameToLayer("Enemy bullet");

    protected override float MaxLifetime => 5f;

    protected override void Awake()
    {
        base.Awake();
        originalParent = FindObjectOfType<EnemyLaserPool>().transform;
    }

    protected override void Update()
    {
        base.Update();

        if (isActiveAndEnabled)
        {
            CheckCollisionWith<EnemyBullet>();

            if (!CollisionCondition)
            {
                spriteRenderer.size = originalSize;
            }
        }
    }

    protected override void HandleCollision<T>(Collider2D coll)
    {
        base.HandleCollision<T>(coll);

        if (coll.TryGetComponent(out EnemyBullet _))
        {
            Vector2 closestPoint = coll.ClosestPoint(transform.position);
            float height = Mathf.Abs(closestPoint.x - transform.position.x);

            Vector2 size = spriteRenderer.size;
            size.y = height;
            spriteRenderer.size = size;
        }
    }

    public override void Destroy()
    {
        transform.parent = originalParent;
        base.Destroy();
    }
}