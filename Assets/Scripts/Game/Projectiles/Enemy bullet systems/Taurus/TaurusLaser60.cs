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

        if (active)
        {
            if (!IsColliding)
            {
                activeSize = originalSize;
            }

            spriteRenderer.size = activeSize;
        }
    }

    protected override void HandleCollision<T>(Collider2D coll)
    {
        float closestCollisionDistance = (coll.ClosestPoint(transform.position) - (Vector2)transform.position).sqrMagnitude;

        if (NumCollisions > 1)
        {
            for (int i = 0; i < NumCollisions; i++)
            {
                float sqrDistance = (collisionResults[i].ClosestPoint(transform.position) - (Vector2)transform.position).sqrMagnitude;

                if (collisionResults[i].TryGetComponent(out EnemyBullet _) && sqrDistance < closestCollisionDistance)
                {
                    closestCollisionDistance = sqrDistance;
                }
            }
        }

        if (coll.TryGetComponent(out Ship ship))
        {
            float shipDistance = (coll.ClosestPoint(transform.position) - (Vector2)transform.position).sqrMagnitude;

            if (shipDistance <= closestCollisionDistance && !ship.invincible)
            {
                ship.TakeDamage(projectileData.Power.value);
            }
        }

        if (coll.TryGetComponent(out EnemyBullet _))
        {
            activeSize.y = Mathf.Sqrt(closestCollisionDistance);
        }
        else
        {
            activeSize.y = originalSize.y;
        }
    }

    public override void Destroy()
    {
        transform.parent = originalParent;
        base.Destroy();
    }
}