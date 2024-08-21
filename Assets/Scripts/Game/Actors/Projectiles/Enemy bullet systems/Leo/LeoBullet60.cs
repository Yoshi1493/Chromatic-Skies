using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBullet60 : EnemyBullet
{
    [SerializeField] AnimationCurve sizeInterpolation;

    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 2f;

        yield return WaitUntil(() => (ownerShip.transform.position - transform.position).magnitude <= 2f);
        yield return this.LerpSpeed(2f, 0f, 2f);
    }

    protected override void Update()
    {
        base.Update();

        float t = currentLifetime / MaxLifetime;
        transform.localScale = sizeInterpolation.Evaluate(t) * Vector2.one;
        SpriteRenderer.color = projectileData.gradient.Evaluate(t);
    }
}