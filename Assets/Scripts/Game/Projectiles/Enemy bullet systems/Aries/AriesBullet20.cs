using System.Collections;
using static CoroutineHelper;

public class AriesBullet20 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;

        yield return WaitForSeconds(2f);
        yield return this.LerpSpeed(startSpeed, 2.5f, 0.5f);
    }
}