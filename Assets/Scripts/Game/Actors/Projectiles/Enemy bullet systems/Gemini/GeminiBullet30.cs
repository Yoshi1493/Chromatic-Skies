using System.Collections;
using UnityEngine;

public class GeminiBullet30 : ReflectiveEnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield return null;
    }

    protected override void HandleReflection(Collider2D coll)
    {
        base.HandleReflection(coll);

        MoveSpeed *= 0.5f;
        SpriteRenderer.color = projectileData.gradient.Evaluate(1f);
    }
}