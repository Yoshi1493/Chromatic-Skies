using System.Collections;
using UnityEngine;

public class VirgoBullet32 : EnemyBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, SpriteRenderer.size * 0.8f, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 3f, 1f);
    }

    protected override void Update()
    {
        base.Update();
        SpriteRenderer.color = projectileData.gradient.Evaluate(Mathf.Clamp01(currentLifetime));
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