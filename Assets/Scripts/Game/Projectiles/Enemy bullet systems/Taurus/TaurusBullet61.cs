using System.Collections;
using UnityEngine;

public class TaurusBullet61 : EnemyBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, spriteRenderer.size, 0f, collisionResults, CollisionMask);

    protected override float MaxLifetime => 6f;

    protected override void Awake()
    {
        base.Awake();
        GetComponent<BoxCollider2D>().size = spriteRenderer.size;
    }

    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        yield return null;
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            Gizmos.DrawCube(transform.position, spriteRenderer.size);
        }
    }
#endif
}