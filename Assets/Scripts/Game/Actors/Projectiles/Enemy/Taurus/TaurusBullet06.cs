using System.Collections;
using UnityEngine;

public class TaurusBullet06 : EnemyBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, SpriteRenderer.size * 0.8f, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(1f, 2f, 1f));
        yield return this.RotateBy(Random.Range(-3f, 3f), 2f);
    }
}