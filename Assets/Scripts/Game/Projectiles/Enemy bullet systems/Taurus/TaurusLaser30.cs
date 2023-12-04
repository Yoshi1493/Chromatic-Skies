using UnityEngine;

public class TaurusLaser30 : Laser
{
    protected override int CollisionMask => base.CollisionMask | 1 << LayerMask.NameToLayer("Enemy bullet");

    protected override int MaxCollisions => 16;
    protected override float MaxLifetime => 1.5f;

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
            Vector3 shipPos = coll.ClosestPoint(transform.position);
            float shipDistance = (shipPos - transform.position).sqrMagnitude;

            if (shipDistance <= closestCollisionDistance && !ship.invincible)
            {
                ship.TakeDamage(projectileData.Power.value);

                //get particle spawn rotation
                float rot = coll.transform.position.GetRotationDifference(transform.position);
                SpawnDestructionParticles(shipPos, rot);
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
}