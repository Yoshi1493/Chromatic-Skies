using System.Collections;
using UnityEngine;

public class CancerBullet51 : EnemyBullet
{
    protected override Collider2D CollisionCondition => Physics2D.OverlapBox(transform.position, spriteRenderer.size, transform.eulerAngles.z, CollisionMask);

    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(6f, 2f, 1f);
    }
}