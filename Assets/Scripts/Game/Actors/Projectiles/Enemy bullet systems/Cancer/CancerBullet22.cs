using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet22 : EnemyBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, SpriteRenderer.size, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, 1f);
        yield return WaitForSeconds(0.5f);

        yield return this.LerpSpeed(0f, 3f, 0.5f);
    }
}