using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialYellow : SpecialBullet
{
    protected override int MaxCollisions => 8;
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, SpriteRenderer.size, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override IEnumerator Move()
    {
        moveDirection *= -1;

        yield return this.LerpSpeed(0f, 2.5f, 0.5f);
        yield return WaitForSeconds(0.75f);

        Destroy();
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        Gizmos.matrix = collider.transform.localToWorldMatrix;
        Gizmos.DrawCube(transform.position, SpriteRenderer.size);
    }
#endif
}