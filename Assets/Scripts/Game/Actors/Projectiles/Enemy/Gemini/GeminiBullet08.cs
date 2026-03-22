using System.Collections;
using UnityEngine;

public class GeminiBullet08 : EnemyBullet
{
    [SerializeField] ReflectiveBullet reflectComponent;

    protected override int CollisionMask => base.CollisionMask | 1 << LayerMask.NameToLayer("Bullet bounds");

    protected override float MaxLifetime => 20f;

    protected override void OnEnable()
    {
        base.OnEnable();
        SpriteRenderer.color = projectileData.gradient.Evaluate(1f);
    }

    protected override IEnumerator Move()
    {
        yield return null;
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<EdgeCollider2D>();
    }

    protected override void HandleCollision(Collider2D coll)
    {
        base.HandleCollision(coll);

        if (coll.TryGetComponent(out EdgeCollider2D _))
        {
            reflectComponent.HandleReflection(coll);
            MoveSpeed = 1.5f;
        }
    }
}