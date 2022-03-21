using System.Collections;
using UnityEngine;

public class PiscesBullet4 : EnemyBullet
{
    protected override Collider2D CollisionCondition => Physics2D.OverlapBox(transform.position, spriteRenderer.size * 0.8f, transform.eulerAngles.z, CollisionMask);
    protected override float MaxLifetime => 4f;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(6f, 3f, 2f));
        yield return this.RotateBy(-33f, 3f);
    }

    protected override void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.zero, spriteRenderer.size * 0.8f);
        }
    }
}