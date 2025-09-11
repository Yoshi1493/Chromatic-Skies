using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialBlue0 : SpecialBullet
{
    Vector2 maxSize = 2.56f * Vector2.one;
    Vector2 minSize = 0.32f * Vector2.one;

    public int HitCount { get; private set; }
    public void RegisterHit() => HitCount++;

    protected override void OnEnable()
    {
        base.OnEnable();

        SpriteRenderer.size = minSize;
        HitCount = 0;
    }

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;

        yield return this.LerpSize(maxSize, 1f);
        yield return WaitForSeconds(5f);

        yield return this.LerpSize(Vector2.zero, 1f);
    }

    protected override void Update()
    {
        base.Update();

        transform.position = playerShip.transform.position;
        ((CircleCollider2D)collider).radius = HitboxSize;
    }
}