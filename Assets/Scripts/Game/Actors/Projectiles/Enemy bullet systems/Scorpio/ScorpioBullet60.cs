using System.Collections;
using UnityEngine;
using static MathHelper;

public class ScorpioBullet60 : EnemyBullet
{
    protected override int MaxCollisions => 64;
    protected override int CollisionMask => base.CollisionMask | 1 << LayerMask.NameToLayer("Enemy bullet");

    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        Vector3 v = transform.position - (3f * transform.up.RotateVectorBy(PositiveOrNegativeOne * Random.Range(60f, 80f)));
        yield return this.MoveTo(v, 1f);
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<EnemyBullet>();
    }

    protected override void HandleCollision<T>(Collider2D coll)
    {
        base.HandleCollision<T>(coll);

        if (coll.TryGetComponent(out ScorpioBullet61 bullet))
        {
            Vector3 v = transform.position - Vector3.Scale(bullet.SpriteRenderer.size * 0.5f, bullet.moveDirection);

            if (coll.OverlapPoint(v))
            {
                bullet.Destroy();
            }
        }
    }
}