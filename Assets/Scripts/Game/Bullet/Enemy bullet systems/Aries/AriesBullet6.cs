using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBullet6 : EnemyBullet
{
    readonly float spinSpeed = 180f;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(5f, 3f, 2f));

        while (enabled)
        {
            spriteRenderer.transform.Rotate(spinSpeed * Time.deltaTime * Vector3.forward);
            yield return EndOfFrame;
        }
    }
}