using System.Collections;
using UnityEngine;

public class GeminiBullet52 : ReflectiveEnemyBullet
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

        MoveSpeed = 2f;
        SpriteRenderer.color = projectileData.gradient.Evaluate(currentReflectCount / (float)MaxReflectCount);
    }
}