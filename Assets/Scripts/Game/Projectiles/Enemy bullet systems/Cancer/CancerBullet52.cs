using System.Collections;
using UnityEngine;

public class CancerBullet52 : EnemyBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, spriteRenderer.size, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(6f, 2f, 1f);
    }
}