using System.Collections;
using UnityEngine;

public class TaurusBullet20 : EnemyBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, spriteRenderer.size * 0.8f, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override float MaxLifetime => 7f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 1f);
        yield return this.LerpSpeed(0f, Random.Range(2f, 4f), 0.5f);
    }
}