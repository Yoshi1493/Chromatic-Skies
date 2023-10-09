using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet12 : EnemyBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, spriteRenderer.size, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 1f);
        yield return WaitForSeconds(0.5f);

        moveDirection *= -1;
        StartCoroutine(this.LerpSpeed(0f, Random.Range(4f, 6f), 1f));
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