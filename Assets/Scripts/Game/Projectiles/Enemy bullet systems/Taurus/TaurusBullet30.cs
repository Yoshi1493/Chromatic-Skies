using System.Collections;
using UnityEngine;

public class TaurusBullet30 : EnemyBullet
{
    protected override int CollisionMask => base.CollisionMask | 1 << LayerMask.NameToLayer("Player bullet");

    protected override float MaxLifetime => 12f;

    protected override void Awake()
    {
        base.Awake();
        GetComponent<CircleCollider2D>().radius = HitboxSize;
    }

    protected override IEnumerator Move()
    {
        MoveSpeed = 2f;
        yield return null;
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<PlayerBullet>();
    }

    protected override void HandleCollision<T>(Collider2D coll)
    {
        base.HandleCollision<T>(coll);

        if (coll.TryGetComponent(out PlayerBullet bullet))
        {
            Vector3 pos = coll.ClosestPoint(transform.position);
            float rot = coll.transform.position.GetRotationDifference(transform.position);
            SpawnDestructionParticles(pos, rot);

            bullet.Destroy();
            Destroy();
        }
    }
}