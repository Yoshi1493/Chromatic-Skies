using System.Collections;
using UnityEngine;

public class VirgoBullet32 : EnemyBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, spriteRenderer.size * 0.8f, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(6f, 0f, 0.5f);
        moveDirection *= -1;
        StartCoroutine(this.LerpSpeed(0f, Random.Range(2f, 3f), 1f));
        StartCoroutine(this.RotateBy(60f, 5f));
    }
}