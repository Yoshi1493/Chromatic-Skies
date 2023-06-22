using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet22 : EnemyBullet
{
    protected override Collider2D CollisionCondition => Physics2D.OverlapBox(transform.position, spriteRenderer.size, transform.eulerAngles.z, CollisionMask);

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return WaitForSeconds(1f);
        yield return this.LerpSpeed(0f, 2.5f, 1f);
    }
}