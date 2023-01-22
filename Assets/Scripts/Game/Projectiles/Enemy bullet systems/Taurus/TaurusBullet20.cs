using System.Collections;
using UnityEngine;

public class TaurusBullet20 : EnemyBullet
{
    protected override Collider2D CollisionCondition => Physics2D.OverlapBox(transform.position, spriteRenderer.size * 0.8f, transform.eulerAngles.z, CollisionMask);
    protected override float MaxLifetime => 7f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 0.5f);
        yield return this.LerpSpeed(0f, Random.Range(2f, 5f), 0.5f);
    }
}