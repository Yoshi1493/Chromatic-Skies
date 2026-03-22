using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialYellow1 : SpecialBullet
{
    protected override int MaxCollisions => 32;
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, SpriteRenderer.size, transform.eulerAngles.z, collisionResults, CollisionMask);

    Vector2 maxSize = new(0.32f, 20.48f);
    Vector2 minSize = new(0f, 20.48f);

    protected override void OnEnable()
    {
        base.OnEnable();
        SpriteRenderer.size = Vector2.zero;
    }

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;

        yield return this.LerpSize(maxSize, 0.2f);
        yield return WaitForSeconds(1f);

        yield return this.LerpSize(minSize, 0.5f);

        Destroy();
    }

    protected override void Update()
    {
        base.Update();
        ((BoxCollider2D)collider).size = SpriteRenderer.size;
    }
}