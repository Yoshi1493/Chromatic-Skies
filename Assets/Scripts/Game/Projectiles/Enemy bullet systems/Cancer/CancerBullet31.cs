using System.Collections;
using UnityEngine;

public class CancerBullet31 : EnemyBullet
{
    protected override Collider2D CollisionCondition => Physics2D.OverlapBox(transform.position, spriteRenderer.size, transform.eulerAngles.z, CollisionMask);

    protected override float MaxLifetime => 3f;

    protected override IEnumerator Move()
    {
        MoveSpeed = Random.Range(6f, 8f);
        yield break;
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