using System.Collections;
using UnityEngine;

public class TaurusBullet20 : EnemyBullet
{
    protected override float MaxLifetime => 6f;
    protected override Collider2D CollisionCondition => Physics2D.OverlapBox(transform.position, spriteRenderer.size * 0.8f, transform.eulerAngles.z, CollisionMask);

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 2f, 0.5f);
        yield return this.LerpSpeed(2f, -4f, 1f);
    }
}