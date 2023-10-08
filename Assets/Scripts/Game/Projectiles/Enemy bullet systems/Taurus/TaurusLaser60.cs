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

        CheckCollisionWith<EnemyBullet>();
        spriteRenderer.size = IsColliding ? activeSize : originalSize;
    }

    protected override void HandleCollision<T>(Collider2D coll)
    {
        base.HandleCollision<T>(coll);

        if (coll.TryGetComponent(out EnemyBullet _))
        {
            Vector2 closestPoint = coll.ClosestPoint(transform.position);
            float height = Mathf.Abs(closestPoint.x - transform.position.x);

            activeSize.y = height;
        }
    }

    public override void Destroy()
    {
        transform.parent = originalParent;
        base.Destroy();
    }
}