using System.Collections;
using UnityEngine;

public class TaurusBullet61 : EnemyBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, SpriteRenderer.size, 0f, collisionResults, CollisionMask);

    protected override float MaxLifetime => 6f;

    protected override void Awake()
    {
        base.Awake();
        GetComponent<BoxCollider2D>().size = SpriteRenderer.size;
    }

    protected override IEnumerator Move()
    {
        yield return null;
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            Gizmos.DrawCube(transform.position, SpriteRenderer.size);
        }
    }
#endif
}