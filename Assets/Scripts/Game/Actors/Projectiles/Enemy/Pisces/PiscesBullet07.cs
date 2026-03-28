using System.Collections;
using UnityEngine;

public class PiscesBullet07 : MinibossBullet
{
    protected override int NumCollisions => Physics2D.OverlapBox(transform.position, SpriteRenderer.size * 0.8f, transform.eulerAngles.z, contactFilter, collisionResults);

    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        Vector2 originalSize = SpriteRenderer.size;
        SpriteRenderer.size = Vector2.zero;

        yield return this.LerpSize(originalSize, 1f);
        yield return this.LerpSpeed(0f, Random.Range(2.5f, 4f), 1f);
    }
}