using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet22 : EnemyBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, spriteRenderer.size, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return WaitForSeconds(1f);
        yield return this.LerpSpeed(0f, 2.5f, 1f);
    }
}