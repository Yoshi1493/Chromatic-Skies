using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBullet40 : EnemyBullet
{
    protected override float MaxLifetime => 12.5f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return WaitForSeconds(2f);

        StartCoroutine(this.LerpSpeed(0f, 1.5f, 1f));
        yield return this.RotateBy(Random.Range(90f, 180f), 3f, Random.value > 0.5f);
    }
}