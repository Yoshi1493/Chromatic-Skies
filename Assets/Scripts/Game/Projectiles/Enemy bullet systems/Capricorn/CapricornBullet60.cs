using System.Collections;
using UnityEngine;

public class CapricornBullet60 : EnemyBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, spriteRenderer.size * 0.8f, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 3f, 1f);
    }
}