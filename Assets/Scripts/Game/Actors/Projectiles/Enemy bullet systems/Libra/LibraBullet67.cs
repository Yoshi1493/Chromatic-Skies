using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBullet67 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        Vector3 originalPosition = ownerShip.transform.position;

        StartCoroutine(this.LerpSpeed(4f, 0f, 1f));
        yield return WaitForSeconds(1f);

        yield return this.TransformRotateAround(originalPosition, 4f, 90f);
        yield return WaitForSeconds(1f);

        yield return this.TransformRotateAround(originalPosition, 4f, 90f, false);
    }
}