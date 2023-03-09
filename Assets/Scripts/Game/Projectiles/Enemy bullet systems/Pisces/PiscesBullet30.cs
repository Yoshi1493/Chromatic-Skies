using System.Collections;
using UnityEngine;

public class PiscesBullet30 : EnemyBullet
{
    protected override Collider2D CollisionCondition => Physics2D.OverlapBox(transform.position, spriteRenderer.size * 0.8f, transform.eulerAngles.z, CollisionMask);
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(6f, 3f, 1f));
        yield return this.RotateBy(30f, 3f, false);
    }
}