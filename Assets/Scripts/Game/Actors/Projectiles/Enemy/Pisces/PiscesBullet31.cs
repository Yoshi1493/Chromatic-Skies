using System.Collections;
using UnityEngine;

public class PiscesBullet31 : BossBullet
{
    protected override int NumCollisions => Physics2D.OverlapBox(transform.position, SpriteRenderer.size * 0.8f, transform.eulerAngles.z, contactFilter, collisionResults);

    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(6f, 0f, 0.5f);
        yield return this.LerpSpeed(0f, 2.5f, 1f);
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.zero, SpriteRenderer.size * 0.8f);
        }
    }
#endif
}