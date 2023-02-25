using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBullet61 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 0f, 1f);
        yield return this.GraduallyLookAt(playerShip.transform.position, 1f);

        yield return WaitForSeconds(3f);

        StartCoroutine(this.LerpSpeed(0f, 10f, 5f));
        yield return this.GraduallyLookAt(playerShip.transform.position, 1f);
    }
}