using System.Collections;
using UnityEngine;

public class AriesBullet41 : EnemyBullet
{
    const float SpinSpeed = 180f;

    protected override float MaxLifetime => 5f;

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