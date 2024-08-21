using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet12 : EnemyBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, SpriteRenderer.size, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 1f);
        yield return WaitForSeconds(0.5f);

        moveDirection *= -1;
        StartCoroutine(this.LerpSpeed(0f, Random.Range(3f, 5f), 1f));
    }
}