using System.Collections;
using UnityEngine;

public class GeminiBullet10 : ReflectiveEnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        yield return null;
    }

    protected override void HandleReflection(Collider2D coll)
    {
        base.HandleReflection(coll);

        MoveSpeed = 1.5f;
        spriteRenderer.color = projectileData.gradient.Evaluate(1f);
    }
}