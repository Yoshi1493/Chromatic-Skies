using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBullet65 : EnemyBullet
{
    protected override float MaxLifetime => 18f;

    protected override IEnumerator Move()
    {
        Vector3 originalPosition = transform.position;

        yield return this.LerpSpeed(2f, 0f, 1f);
        yield return WaitForSeconds(5f);

        yield return this.TransformRotateAround(originalPosition, 1f, 180f + (LibraBulletSystem63.ParentBulletSpacing / 2f));
        MoveSpeed = 0f;
    }
}