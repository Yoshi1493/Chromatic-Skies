using System.Collections;
using UnityEngine;

public class PiscesBullet52 : EnemyBullet
{
    protected override Collider2D CollisionCondition => Physics2D.OverlapBox(transform.position, spriteRenderer.size * 0.6f, transform.eulerAngles.z, CollisionMask);
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}