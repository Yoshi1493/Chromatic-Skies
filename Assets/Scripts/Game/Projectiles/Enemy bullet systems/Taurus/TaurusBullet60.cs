using System.Collections;
using UnityEngine;

public class TaurusBullet60 : EnemyBullet
{
    protected override Collider2D CollisionCondition => Physics2D.OverlapBox(transform.position, spriteRenderer.size, 0f, CollisionMask);

    protected override float MaxLifetime => 5f;

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