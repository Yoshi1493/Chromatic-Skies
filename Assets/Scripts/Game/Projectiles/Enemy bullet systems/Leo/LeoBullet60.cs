using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBullet60 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;

        yield return WaitUntil(() => (ownerShip.transform.position - transform.position).magnitude <= 3f);
        yield return this.LerpSpeed(3f, 0f, 2f);
    }

    protected override void Update()
    {
        base.Update();

        float t = currentLifetime / MaxLifetime;
        transform.localScale = Mathf.Lerp(1f, 0.5f, t) * Vector2.one;
        spriteRenderer.color = projectileData.gradient.Evaluate(t);
    }
}