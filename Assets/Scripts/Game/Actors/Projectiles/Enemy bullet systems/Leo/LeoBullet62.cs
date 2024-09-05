using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBullet62 : EnemyBullet
{
    [SerializeField] AnimationCurve sizeInterpolation;
    Vector2 originalSpriteSize;

    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 2f;

        yield return WaitUntil(() => (ownerShip.transform.position - transform.position).magnitude <= 2f);
        yield return this.LerpSpeed(2f, 0f, 2f);
    }

    protected override void Awake()
    {
        base.Awake();
        originalSpriteSize = SpriteRenderer.size;
    }

    protected override void Update()
    {
        base.Update();

        float t = currentLifetime / MaxLifetime;
        SpriteRenderer.size = sizeInterpolation.Evaluate(t) * originalSpriteSize;
        SpriteRenderer.color = projectileData.gradient.Evaluate(t);
    }
}