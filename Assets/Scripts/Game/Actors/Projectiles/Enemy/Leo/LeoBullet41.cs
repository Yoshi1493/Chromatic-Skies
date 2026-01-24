using System.Collections;
using UnityEngine;

public class LeoBullet41 : BossBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        Vector2 originalSize = SpriteRenderer.size;
        Vector2 endSize = 0.75f * originalSize;

        StartCoroutine(this.LerpSpeed(2.4f, 1.2f, 2f));
        yield return this.LerpSize(endSize, 5f);
    }
}