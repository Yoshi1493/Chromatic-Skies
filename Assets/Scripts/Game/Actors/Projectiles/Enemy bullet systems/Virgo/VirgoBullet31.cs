using System.Collections;
using UnityEngine;

public class VirgoBullet31 : EnemyBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, spriteRenderer.size * 0.8f, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, 0.5f);
        StartCoroutine(this.LerpSpeed(0f, 2.5f, 1f));
    }
}