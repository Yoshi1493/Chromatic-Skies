using System.Collections;
using UnityEngine;

public class PiscesBullet04 : EnemyBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, SpriteRenderer.size * 0.8f, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override IEnumerator Move()
    {
        Vector2 originalSize = SpriteRenderer.size;
        SpriteRenderer.size = Vector2.zero;

        yield return this.LerpSize(originalSize, 0.5f);
        yield return this.LerpSpeed(4f, 2.5f, 0.5f);
    }
}