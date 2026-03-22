using System.Collections;
using UnityEngine;

public class LeoBullet04 : EnemyBullet
{
    Vector2 originalSize;

    protected override void Awake()
    {
        base.Awake();
        originalSize = SpriteRenderer.size;
    }

    protected override IEnumerator Move()
    {
        SpriteRenderer.size = originalSize * 0.5f;

        yield return this.LerpSpeed(5f, 0f, 0.5f);
        yield return this.LerpSize(originalSize, 2f);
    }
}