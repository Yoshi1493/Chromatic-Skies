using System.Collections;
using UnityEngine;

public class SpecialRed : SpecialBullet
{
    new CircleCollider2D collider;

    protected override float MaxLifetime => 4f;
    protected override int MaxCollisions => 64;

    protected override void Awake()
    {
        base.Awake();
        collider = GetComponent<CircleCollider2D>();
    }

    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(2f, 7f, 2f));

        yield return this.LerpSize(5f * Vector2.one, 1.5f);
        yield return this.LerpSize(Vector2.zero, 2.5f);
    }

    protected override void Update()
    {
        base.Update();

        //[special bullet <-> enemy bullet] collision detection requires a collider in at least one party
        collider.radius = HitboxSize;
    }
}