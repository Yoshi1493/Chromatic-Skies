using System.Collections;
using UnityEngine;

public class PiscesBullet07 : MinibossBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, SpriteRenderer.size * 0.8f, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, Random.Range(3f, 5f), 1f);
    }
}