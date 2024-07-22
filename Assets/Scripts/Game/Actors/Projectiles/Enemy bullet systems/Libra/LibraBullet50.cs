using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBullet50 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);

        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 0f, 1f);
    }
}