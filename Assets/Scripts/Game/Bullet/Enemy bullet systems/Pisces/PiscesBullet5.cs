using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBullet5 : EnemyBullet
{
    protected override float MaxLifetime => 12.5f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return WaitForSeconds(2f);

        float dist = Vector2.Distance(transform.position, ownerShip.transform.position);
        float endSpeed = Mathf.Lerp(1f, 2f, dist / 6f);

        StartCoroutine(this.LerpSpeed(0f, endSpeed, 1f));
        yield return this.RotateBy(Random.Range(90f, 180f), 3f, Random.value > 0.5f);
    }
}