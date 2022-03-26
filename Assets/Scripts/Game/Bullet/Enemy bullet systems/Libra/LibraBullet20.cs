using System.Collections;
using UnityEngine;

public class LibraBullet20 : EnemyBullet
{
    protected override Collider2D CollisionCondition => Physics2D.OverlapBox(transform.position, spriteRenderer.size * 0.8f, transform.eulerAngles.z, CollisionMask);
    protected override float MaxLifetime => 3f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 1f, 0.5f);
        yield return this.LerpSpeed(1f, 6f, 1f);
    }
}