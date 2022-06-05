using System.Collections;
using UnityEngine;

public class TaurusBullet40 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override Collider2D CollisionCondition => Physics2D.OverlapBox(transform.position, spriteRenderer.size * 0.8f, transform.eulerAngles.z, CollisionMask);
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        StartCoroutine(this.LerpSpeed(0f, endSpeed, 1f));
        yield return this.RotateBy(45f, 2f, rotatesClockwise);
    }
}