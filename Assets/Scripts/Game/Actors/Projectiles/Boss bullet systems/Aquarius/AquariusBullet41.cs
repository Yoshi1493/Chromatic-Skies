using System.Collections;
using UnityEngine;

public class AquariusBullet41 : BossBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, SpriteRenderer.size, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return null;
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, SpriteRenderer.size);
    }
#endif
}