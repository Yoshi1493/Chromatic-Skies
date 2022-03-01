using System.Collections;
using UnityEngine;

public class AriesBullet6 : EnemyBullet
{
    readonly float SpinSpeed = 180f;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(5f, 3f, 2f));

        while (enabled)
        {
            spriteRenderer.transform.Rotate(SpinSpeed * Time.deltaTime * Vector3.forward);
            yield return null;
        }
    }
}