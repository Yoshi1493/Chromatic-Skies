using System.Collections;
using UnityEngine;

public class TaurusBullet05 : MinibossBullet
{
    protected override int NumCollisions => Physics2D.OverlapBox(transform.position, SpriteRenderer.size * 0.8f, transform.eulerAngles.z, contactFilter, collisionResults);

    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 2f, 1f);
    }
}