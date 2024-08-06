using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBullet63 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        Vector3 originalPosition = transform.position;

        StartCoroutine(this.LerpSpeed(4f, 0f, 1f));
        yield return this.RotateBy(108f, 1f);
        yield return WaitForSeconds(4f);

        yield return this.TransformRotateAround(originalPosition, 1f, 180f);
        MoveSpeed = 0f;
    }
}