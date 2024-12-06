using System.Collections;
using UnityEngine;

public class SpecialYellow : SpecialBullet
{
    protected override int MaxCollisions => 8;
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, SpriteRenderer.size, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override IEnumerator Move()
    {
        yield return this.LerpSize(Vector2.zero, 0.5f);
        Destroy();
    }
}