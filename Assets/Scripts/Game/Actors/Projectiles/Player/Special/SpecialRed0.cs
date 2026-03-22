using System.Collections;
using UnityEngine;

public class SpecialRed0 : SpecialBullet
{
    Vector2 maxSize = 5f * Vector2.one;

    protected override float MaxLifetime => 4f;
    protected override int MaxCollisions => 64;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(2f, 7f, 2f));

        yield return this.LerpSize(maxSize, 1.5f);
        yield return this.LerpSize(Vector2.zero, 2.5f);

        Destroy();
    }

    protected override void Update()
    {
        base.Update();
        ((CircleCollider2D)collider).radius = HitboxSize;
    }
}