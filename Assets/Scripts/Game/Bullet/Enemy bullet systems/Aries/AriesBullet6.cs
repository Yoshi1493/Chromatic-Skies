using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBullet6 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(5f, 3f, 2f));

        while (enabled)
        {
            spriteRenderer.transform.Rotate(Vector3.forward);
            yield return EndOfFrame;
        }
    }
}