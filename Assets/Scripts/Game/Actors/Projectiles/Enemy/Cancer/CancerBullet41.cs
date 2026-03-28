using System.Collections;
using UnityEngine;

public class CancerBullet41 : BossBullet
{
    protected override int NumCollisions => Physics2D.OverlapBox(transform.position, SpriteRenderer.size, transform.eulerAngles.z, contactFilter, collisionResults);

    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(6f, 3f, 1f);
    }
}