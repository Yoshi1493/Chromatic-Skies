using System.Collections;
using static CoroutineHelper;

public class AriesBullet20 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;

        yield return WaitForSeconds(1f);
        yield return this.LerpSpeed(startSpeed, 3f, 1f);
    }
}