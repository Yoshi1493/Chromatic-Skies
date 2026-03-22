using System.Collections;
using UnityEngine;

public class LeoBullet00 : EnemyBullet
{
    Vector2 originalSize;

    protected override float MaxLifetime => 8f;

    protected override void Awake()
    {
        base.Awake();
        originalSize = SpriteRenderer.size;
    }

    protected override IEnumerator Move()
    {
        SpriteRenderer.size = originalSize * 0.25f;

        StartCoroutine(this.LerpSize(originalSize, 0.25f));
        yield return this.LerpSpeed(3f, 1.6f, 1.5f);
    }
}