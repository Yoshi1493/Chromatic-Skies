using System.Collections;
using UnityEngine;

public class CancerBullet32 : EnemyBullet
{
    protected override Collider2D CollisionCondition => Physics2D.OverlapBox(transform.position, spriteRenderer.size, transform.eulerAngles.z, CollisionMask);

    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.RotateBy(60f, MaxLifetime));
        yield return this.LerpSpeed(-5f, 3f, 1f);
    }

    protected override void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.zero, spriteRenderer.size);
        }
    }
}