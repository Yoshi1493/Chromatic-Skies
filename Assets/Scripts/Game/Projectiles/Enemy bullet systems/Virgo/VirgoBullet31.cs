using System.Collections;
using UnityEngine;

public class VirgoBullet31 : EnemyBullet
{
    [SerializeField] int multiplier;
    protected override Collider2D CollisionCondition => Physics2D.OverlapBox(transform.position, spriteRenderer.size * 0.8f, transform.eulerAngles.z, CollisionMask);

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 1f);
        StartCoroutine(this.LerpSpeed(0f, Random.Range(2.5f, 3.5f) * multiplier, 1f));
        yield return this.RotateBy(30f * multiplier, 3f, delay: 0.5f);
    }
}