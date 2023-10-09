using System.Collections;
using UnityEngine;

public class CancerBullet32 : EnemyBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, spriteRenderer.size, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.RotateBy(60f, MaxLifetime));
        yield return this.LerpSpeed(-5f, 3f, 1f);
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.zero, spriteRenderer.size);
        }
    }
#endif
}