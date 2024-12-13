using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialGreen1 : SpecialBullet
{
    [Space]

    [SerializeField] TrailRenderer trail;

    protected override float MaxLifetime => 4f;
    protected override int MaxCollisions => 8;
    
    Vector2 originalSize;

    protected override void Awake()
    {
        base.Awake();
        originalSize = SpriteRenderer.size;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        SpriteRenderer.size = originalSize;
        trail.time = 1f;
    }

    protected override IEnumerator Move()
    {
        MoveSpeed = 4f;

        yield return WaitForSeconds(MaxLifetime - 1f);
        yield return this.LerpSize(Vector2.zero, 1f);
    }

    protected override void Update()
    {
        base.Update();

        ((CircleCollider2D)collider).radius = HitboxSize;
        trail.time = 1 - (currentLifetime / MaxLifetime);
    }
}