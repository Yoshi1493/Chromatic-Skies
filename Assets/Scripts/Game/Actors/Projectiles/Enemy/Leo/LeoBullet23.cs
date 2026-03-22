using System.Collections;
using UnityEngine;

public class LeoBullet23 : BossBullet
{
    protected override float MaxLifetime => 12f;
    Vector2 originalSize;

    protected override void Awake()
    {
        base.Awake();
        originalSize = SpriteRenderer.size;
    }

    protected override IEnumerator Move()
    {
        SpriteRenderer.size = originalSize;

        Vector2 endSize = originalSize * 0.75f;
        float endSpeed = MoveSpeed;

        StartCoroutine(this.LerpSize(endSize, MaxLifetime * 0.5f));
        StartCoroutine(this.LerpSpeed(0f, endSpeed, 2f));
        yield return this.RotateBy(60f, 10f, false);
    }
}