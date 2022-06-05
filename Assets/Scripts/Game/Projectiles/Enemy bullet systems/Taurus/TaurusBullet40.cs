using System.Collections;
using UnityEngine;

public class TaurusBullet40 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        StartCoroutine(this.LerpSpeed(0f, endSpeed, 1f));
        yield return this.RotateBy(45f, 2f, rotatesClockwise);
    }
}